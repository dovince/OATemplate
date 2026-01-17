using FSDZ.Logger;
using System;
using System.IO;
using System.Linq;
using ZWL.Common;
using ZWL.DBUtility;

namespace SchedulerJob.CustomJobs
{
    public class ChuChaiQiJianYongCan : ISchedulerJob
    {
        #region MyRegion
        private Definition _definition;
        public Definition Delimit
        {
            get
            {
                if (_definition == null)
                {
                    _definition = new Definition
                    {
                        IntervalType = IntervalType.Second,
                        RunType = RunType.Trigger
                    };
                }
                return _definition;
            }
            set
            {
                _definition = value;
            }
        }
        #endregion

        public void Run()
        {
            try
            {
                var date = DateTime.Now;
                var lastSql = @"SELECT TOP 1 * FROM Flow WHERE DataTable='FanTangJiuCanRecord' and Operation=1 and DATEDIFF(MINUTE, CreatedTime, GETDATE())<20 ORDER BY ID DESC";
                var flow = Conv<ZWL.BLL.Flow>.GetModel(lastSql);
                if (flow != null)
                {
                    var filepath = Path.Combine(PublicMethod.UploadFileFolderTruePath, flow.NewValue);
                    ExcelUtility.SetActiveSheet(filepath, 0);
                    var dt = ExcelUtility.FileToDataTable(filepath, 2);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var syscols = Util.GetSysColumnInfos("FanTangJiuCanRecord");
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            var item = dt.Columns[i];
                            var citem = syscols.FirstOrDefault(r => r.Desc == item.ColumnName);
                            if (citem != null)
                            {
                                dt.Columns[i].ColumnName = citem.Name;
                            }
                        }
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var flist = DataTableHelper.ConvertTo<ZWL.BLL.FanTangJiuCanRecord>(dt);
                        var glist = flist.Where(x => x.RecordDate != DateTime.MinValue);
                        var minDate = glist.Min(x => x.RecordDate);
                        var maxDate = glist.Max(x => x.RecordDate);
                        var csqlWhere = @"select c.* from ERPChuChai c JOIN ERPNWorkToDo d on c.NWorkID=d.ID
                                        where d.StateNow not in ('已被驳回','不通过') and ( 
				                                        (ChuChaiStart BETWEEN '{0}' AND '{1}')
                                                        OR (ChuChaiEnd BETWEEN '{0}' AND '{1}')
                                                        OR (ChuChaiStart <= '{0}' AND ChuChaiEnd >= '{1}')
		                                        )".FormatWith(TimeParser.GetFormatDateString(minDate), TimeParser.GetFormatDateString(maxDate));
                        var clist = Conv<ZWL.BLL.ERPChuChai>.GetList(csqlWhere);
                        foreach (var item in glist)
                        {
                            if (item.Name.IsNullOrEmpty()) continue;

                            var isChuChai = "否";
                            var chuChaiDi = string.Empty;
                            var chuChaiNWorkID = string.Empty;

                            item.LotID = flow.LotID;
                            item.CanShi = item.ShiJianDuan.Substring(0, item.ShiJianDuan.IndexOf("餐") + 1);

                            var checkList = clist.Where(x => (x.SQR == item.Name || !x.TongXingRenYuan.IsNullOrEmpty() && ("," + Util.ReplaceSymbolsWithComma(x.TongXingRenYuan) + ",").Contains("," + item.Name + ","))
                            && ((x.ChuChaiStart >= item.RecordDate && x.ChuChaiStart <= item.RecordDate) || (x.ChuChaiEnd >= item.RecordDate && x.ChuChaiEnd <= item.RecordDate)
                            || (x.ChuChaiStart <= item.RecordDate && x.ChuChaiEnd >= item.RecordDate)));
                            if (checkList.Any())
                            {
                                isChuChai = "是";
                                chuChaiDi = string.Join(",", checkList.Select(x => x.ChuChaiDiDian));
                                chuChaiNWorkID = string.Join(",", checkList.Select(x => x.NWorkID));
                            }
                            item.IsChuChai = isChuChai;
                            item.ChuChaiDiDian = chuChaiDi;
                            item.ChuChaiNWorkID = chuChaiNWorkID;
                            item.ID = item.Add();
                        }
                        var insertSumSql = @"INSERT into FanTangJiuCanSummary(LotID,Number,Dept,Name,ZaoCan,WuCan,ZaoCan1,WuCan1)
						                        select '{0}' LotID,t.*,
                                            (select count(0) c from FanTangJiuCanRecord d where LotID='{0}' and d.Name=t.Name and CanShi='早餐' and (KaoQinRecord is not null and KaoQinRecord<>'-' and LEN(KaoQinRecord)>1) ) ZaoCan,
                                            (select count(0) c from FanTangJiuCanRecord d where LotID='{0}' and d.Name=t.Name and CanShi='午餐' and (KaoQinRecord is not null and KaoQinRecord<>'-' and LEN(KaoQinRecord)>1) ) WuCan,
                                            (select count(0) c from FanTangJiuCanRecord d where LotID='{0}' and d.Name=t.Name and CanShi='早餐' and (KaoQinRecord is not null and KaoQinRecord<>'-' and LEN(KaoQinRecord)>1) and IsChuChai='是' ) ZaoCan1,
                                            (select count(0) c from FanTangJiuCanRecord d where LotID='{0}' and d.Name=t.Name and CanShi='午餐' and (KaoQinRecord is not null and KaoQinRecord<>'-' and LEN(KaoQinRecord)>1) and IsChuChai='是' ) WuCan1  from (
                                            select Number,Dept,Name
                                            from FanTangJiuCanRecord where LotID='{0}'
                                            and IsChuChai='是' and (KaoQinRecord is not null and KaoQinRecord<>'-' and LEN(KaoQinRecord)>1) GROUP BY Number,Dept,Name
                                            ) t ".FormatWith(flow.LotID);
                        DbHelperSQL.ExecuteSQL(insertSumSql);


                        var insertReportSql = ReportSQLFormat.FormatWith(flow.LotID);
                        DbHelperSQL.ExecuteSQL(insertReportSql);


                        Logger.Log(LogType.Info, PublicMethod.ToJson(flow));
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
        }
        private string ReportSQLFormat
        {
            get
            {
                var sql = @"-- 1️⃣ 提取查询月份，避免每次子查询计算
                        DECLARE @LotID UNIQUEIDENTIFIER;
                        SET @LotID = '{0}';

                        DECLARE @Month CHAR(6);
                        SELECT TOP 1 
                            @Month = CONVERT(CHAR(6), RecordDate, 112)
                        FROM FanTangJiuCanRecord
                        WHERE LotID = @LotID;

                        -----------------------------------------------------
                        -- 2️⃣ 预计算主数据（#A）
                        -----------------------------------------------------
                        IF OBJECT_ID('tempdb..#A') IS NOT NULL DROP TABLE #A;

                        SELECT 
                            LotID,
                            Name,
                            ( ISNULL(u.Department, REPLACE(d.Dept, '地质局/', '')) ) AS Dept,
                            RecordDate,
                            XingQi,
                            IsChuChai,
                            ChuChaiDiDian,
                            '' AS ChuChaiShiYou,
                            ChuChaiNWorkID,
                            MAX(CASE WHEN CanShi = '早餐' THEN KaoQinRecord END) AS ZaoCan,
                            MAX(CASE WHEN CanShi = '午餐' THEN KaoQinRecord END) AS WuCan,
                            '-' AS WanCan,
                            CASE 
                                WHEN IsChuChai = '是' AND 
                                     (MAX(CASE WHEN CanShi = '早餐' THEN KaoQinRecord END) <> '-' 
                                      OR MAX(CASE WHEN CanShi = '午餐' THEN KaoQinRecord END) <> '-') 
                                THEN '是' ELSE '' 
                            END AS Received,
                            NULL AS Comment
                        INTO #A
                        FROM FanTangJiuCanRecord d LEFT JOIN ERPUser u on u.UserName=d.Name
                        WHERE LotID = @LotID
                        GROUP BY LotID, RecordDate, ( ISNULL(u.Department, REPLACE(d.Dept, '地质局/', '')) ), Name, XingQi, IsChuChai, ChuChaiDiDian, ChuChaiNWorkID;

                        -----------------------------------------------------
                        -- 3️⃣ 预计算报表数据（#B）
                        -----------------------------------------------------
                        IF OBJECT_ID('tempdb..#B') IS NOT NULL DROP TABLE #B;

                        SELECT *
                        INTO #B
                        FROM FanTangJiuCanRecordReport
                        WHERE ID IN (
                            SELECT MAX(ID)
                            FROM FanTangJiuCanRecordReport d
                            WHERE CONVERT(CHAR(6), RecordDate, 112) = @Month
                              AND (
                                    (Received IS NOT NULL AND Received <> '' AND Received = '否')
                                    OR (Comment IS NOT NULL AND Comment <> '')
                                  )
                            GROUP BY RecordDate, Name
                        );

                        -----------------------------------------------------
                        -- 4️⃣ 创建连接索引（临时表上也生效）
                        -----------------------------------------------------
                        CREATE INDEX IX_A_Name_RecordDate ON #A(Name, RecordDate);
                        CREATE INDEX IX_B_Name_RecordDate ON #B(Name, RecordDate);

                        -----------------------------------------------------
                        -- 5️⃣ 执行主查询
                        -----------------------------------------------------
                        INSERT INTO FanTangJiuCanRecordReport (LotID,Name,Dept,RecordDate,XingQi,IsChuChai,ChuChaiDiDian,ChuChaiShiYou,ChuChaiNWorkID,ZaoCan,WuCan,WanCan,Received,Comment)

                        SELECT 
                            a.LotID,
                            a.Name,
                            a.Dept,
                            a.RecordDate,
                            a.XingQi,
                            a.IsChuChai,
                            a.ChuChaiDiDian,
                            a.ChuChaiShiYou,
                            a.ChuChaiNWorkID,
                            a.ZaoCan,
                            a.WuCan,
                            a.WanCan,
                            ISNULL(b.Received, a.Received) AS Received,
                            ISNULL(b.Comment, a.Comment) AS Comment
                        FROM #A a
                        LEFT JOIN #B b
                            ON a.Name = b.Name
                           AND a.RecordDate = b.RecordDate
                        OPTION (HASH JOIN);   -- ✅ 强制使用 HASH JOIN（适合中大数据量）

                        -----------------------------------------------------
                        -- 6️⃣ 清理（可选）
                        -----------------------------------------------------
                        DROP TABLE #A;
                        DROP TABLE #B;";
                return sql;
            }
        }
    }
}
