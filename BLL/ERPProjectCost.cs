using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用
using ZWL.Common;//请先添加引用

namespace ZWL.BLL
{
    public class ERPProjectCost
    {
        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// XMName
        /// </summary>		
        private string _xmname;
        public string XMName
        {
            get { return _xmname; }
            set { _xmname = value; }
        }
        /// <summary>
        /// XMBH
        /// </summary>		
        private string _xmbh;
        public string XMBH
        {
            get { return _xmbh; }
            set { _xmbh = value; }
        }
        /// <summary>
        /// CostBH
        /// </summary>		
        private DateTime _djtime;
        public DateTime DJTime
        {
            get { return _djtime; }
            set { _djtime = value; }
        }
        /// <summary>
        /// HTBH
        /// </summary>		
        private string _htbh;
        public string HTBH
        {
            get { return _htbh; }
            set { _htbh = value; }
        }
        /// <summary>
        /// XMState
        /// </summary>		
        private string _xmstate;
        public string XMState
        {
            get { return _xmstate; }
            set { _xmstate = value; }
        }
        /// <summary>
        /// ZYLB
        /// </summary>		
        private string _zylb;
        public string ZYLB
        {
            get { return _zylb; }
            set { _zylb = value; }
        }
        /// <summary>
        /// XMBM
        /// </summary>		
        private string _xmbm;
        public string XMBM
        {
            get { return _xmbm; }
            set { _xmbm = value; }
        }
        /// <summary>
        /// XMFZR
        /// </summary>		
        private string _xmfzr;
        public string XMFZR
        {
            get { return _xmfzr; }
            set { _xmfzr = value; }
        }
        /// <summary>
        /// XMBeginTime
        /// </summary>		
        private DateTime _xmbegintime;
        public DateTime XMBeginTime
        {
            get { return _xmbegintime; }
            set { _xmbegintime = value; }
        }
        /// <summary>
        /// XMEndTime
        /// </summary>		
        private DateTime _xmendtime;
        public DateTime XMEndTime
        {
            get { return _xmendtime; }
            set { _xmendtime = value; }
        }
        /// <summary>
        /// HTJE
        /// </summary>		
        private decimal _htje;
        public decimal HTJE
        {
            get { return _htje; }
            set { _htje = value; }
        }
        /// <summary>
        /// 结算金额
        /// </summary>		
        private decimal _jsje;
        public decimal JSJE
        {
            get { return _jsje; }
            set { _jsje = value; }
        }
        /// <summary>
        /// 成本支出合计
        /// </summary>		
        private decimal _costmoneysum;
        public decimal CostMoneySUM
        {
            get { return _costmoneysum; }
            set { _costmoneysum = value; }
        }
        /// <summary>
        /// 项目经费
        /// </summary>		
        private decimal _xmjf;
        public decimal XMJF
        {
            get { return _xmjf; }
            set { _xmjf = value; }
        }


        public bool Exists()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPProjectCost");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID and  ");
            strSql.Append(" XMName = @XMName and  ");
            strSql.Append(" XMBH = @XMBH and  ");
            strSql.Append(" DJTime = @DJTime and  ");
            strSql.Append(" HTBH = @HTBH and  ");
            strSql.Append(" XMState = @XMState and  ");
            strSql.Append(" ZYLB = @ZYLB and  ");
            strSql.Append(" XMBM = @XMBM and  ");
            strSql.Append(" XMFZR = @XMFZR and  ");
            strSql.Append(" XMBeginTime = @XMBeginTime and  ");
            strSql.Append(" XMEndTime = @XMEndTime and  ");
            strSql.Append(" HTJE = @HTJE and  ");
            strSql.Append(" JSJE = @JSJE and  ");
            strSql.Append(" CostMoneySUM = @CostMoneySUM  ");
            strSql.Append(" XMJF = @XMJF  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@XMName", SqlDbType.NVarChar,500),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@DJTime",  SqlDbType.DateTime),
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMState", SqlDbType.NVarChar,50),
                    new SqlParameter("@ZYLB", SqlDbType.NVarChar,50),
                    new SqlParameter("@XMBM", SqlDbType.NVarChar,200),
                    new SqlParameter("@XMFZR", SqlDbType.NVarChar,100),
                    new SqlParameter("@XMBeginTime", SqlDbType.DateTime),
                    new SqlParameter("@XMEndTime", SqlDbType.DateTime),
                    new SqlParameter("@HTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@JSJE", SqlDbType.Decimal,9),
                    new SqlParameter("@CostMoneySUM", SqlDbType.Decimal,9),
                    new SqlParameter("@XMJF", SqlDbType.Decimal,9)
                                        };
            parameters[0].Value = ID;
            parameters[1].Value = XMName;
            parameters[2].Value = XMBH;
            parameters[3].Value = DJTime;
            parameters[4].Value = HTBH;
            parameters[5].Value = XMState;
            parameters[6].Value = ZYLB;
            parameters[7].Value = XMBM;
            parameters[8].Value = XMFZR;
            parameters[9].Value = XMBeginTime;
            parameters[10].Value = XMEndTime;
            parameters[11].Value = HTJE;
            parameters[12].Value = JSJE;
            parameters[13].Value = CostMoneySUM;
            parameters[14].Value = XMJF;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPProjectCost(");
            strSql.Append("XMBeginTime,XMEndTime,HTJE,JSJE,CostMoneySUM,XMName,XMBH,DJTime,HTBH,XMState,ZYLB,XMBM,XMFZR,XMJF");
            strSql.Append(") values (");
            strSql.Append("@XMBeginTime,@XMEndTime,@HTJE,@JSJE,@CostMoneySUM,@XMName,@XMBH,@DJTime,@HTBH,@XMState,@ZYLB,@XMBM,@XMFZR,@XMJF");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");

            SqlParameter[] parameters = {
                        new SqlParameter("@XMBeginTime", SqlDbType.DateTime) ,
                        new SqlParameter("@XMEndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@HTJE", SqlDbType.Decimal,18) ,
                        new SqlParameter("@JSJE", SqlDbType.Decimal,18) ,
                        new SqlParameter("@CostMoneySUM", SqlDbType.Decimal,18) ,
                        new SqlParameter("@XMName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@XMBH", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@DJTime", SqlDbType.DateTime) ,
                        new SqlParameter("@HTBH", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@XMState", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ZYLB", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@XMBM", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@XMFZR", SqlDbType.NVarChar,100),
                        new SqlParameter("@XMJF", SqlDbType.Decimal,9)

            };

            parameters[0].Value = XMBeginTime;
            parameters[1].Value = XMEndTime;
            parameters[2].Value = HTJE;
            parameters[3].Value = JSJE;
            parameters[4].Value = CostMoneySUM;
            parameters[5].Value = XMName;
            parameters[6].Value = XMBH;
            parameters[7].Value = DJTime;
            parameters[8].Value = HTBH;
            parameters[9].Value = XMState;
            parameters[10].Value = ZYLB;
            parameters[11].Value = XMBM;
            parameters[12].Value = XMFZR;
            parameters[13].Value = XMJF;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPProjectCost set ");
            strSql.Append(" XMBeginTime = @XMBeginTime , ");
            strSql.Append(" XMEndTime = @XMEndTime , ");
            strSql.Append(" HTJE = @HTJE , ");
            strSql.Append(" JSJE = @JSJE , ");
            strSql.Append(" CostMoneySUM = @CostMoneySUM , ");
            strSql.Append(" XMName = @XMName , ");
            strSql.Append(" XMBH = @XMBH , ");
            strSql.Append(" DJTime = @DJTime , ");
            strSql.Append(" HTBH = @HTBH , ");
            strSql.Append(" XMState = @XMState , ");
            strSql.Append(" ZYLB = @ZYLB , ");
            strSql.Append(" XMBM = @XMBM , ");
            strSql.Append(" XMFZR = @XMFZR , ");
            strSql.Append(" XMJF = @XMJF  ");
            strSql.Append(" where ID=@ID  ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4) ,
                        new SqlParameter("@XMBeginTime", SqlDbType.DateTime) ,
                        new SqlParameter("@XMEndTime", SqlDbType.DateTime) ,
                        new SqlParameter("@HTJE", SqlDbType.Decimal,18) ,
                        new SqlParameter("@JSJE", SqlDbType.Decimal,18) ,
                        new SqlParameter("@CostMoneySUM", SqlDbType.Decimal,18) ,
                        new SqlParameter("@XMName", SqlDbType.NVarChar,500) ,
                        new SqlParameter("@XMBH", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@DJTime", SqlDbType.DateTime) ,
                        new SqlParameter("@HTBH", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@XMState", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@ZYLB", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@XMBM", SqlDbType.NVarChar,200) ,
                        new SqlParameter("@XMFZR", SqlDbType.NVarChar,100) ,
                        new SqlParameter("@XMJF", SqlDbType.Decimal,9)


            };

            parameters[0].Value = ID;
            parameters[1].Value = XMBeginTime;
            parameters[2].Value = XMEndTime;
            parameters[3].Value = HTJE;
            parameters[4].Value = JSJE;
            parameters[5].Value = CostMoneySUM;
            parameters[6].Value = XMName;
            parameters[7].Value = XMBH;
            parameters[8].Value = DJTime;
            parameters[9].Value = HTBH;
            parameters[10].Value = XMState;
            parameters[11].Value = ZYLB;
            parameters[12].Value = XMBM;
            parameters[13].Value = XMFZR;
            parameters[14].Value = XMJF;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPProjectCost ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = nID;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int nID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, XMBeginTime, XMEndTime, HTJE, JSJE, CostMoneySUM, XMName, XMBH, DJTime, HTBH, XMState, ZYLB, XMBM, XMFZR ,XMJF  ");
            strSql.Append("  from ERPProjectCost ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = nID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBeginTime"].ToString() != "")
                {
                    XMBeginTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMBeginTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMEndTime"].ToString() != "")
                {
                    XMEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["XMEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTJE"].ToString() != "")
                {
                    HTJE = decimal.Parse(ds.Tables[0].Rows[0]["HTJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JSJE"].ToString() != "")
                {
                    JSJE = decimal.Parse(ds.Tables[0].Rows[0]["JSJE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostMoneySUM"].ToString() != "")
                {
                    CostMoneySUM = decimal.Parse(ds.Tables[0].Rows[0]["CostMoneySUM"].ToString());
                }
                XMName = ds.Tables[0].Rows[0]["XMName"].ToString();
                XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();

                if (ds.Tables[0].Rows[0]["DJTime"].ToString() != "")
                {
                    DJTime = DateTime.Parse(ds.Tables[0].Rows[0]["DJTime"].ToString());
                }
                HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                XMState = ds.Tables[0].Rows[0]["XMState"].ToString();
                ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                XMBM = ds.Tables[0].Rows[0]["XMBM"].ToString();
                XMFZR = ds.Tables[0].Rows[0]["XMFZR"].ToString();
                if (ds.Tables[0].Rows[0]["XMJF"].ToString() != "")
                {
                    XMJF = decimal.Parse(ds.Tables[0].Rows[0]["XMJF"].ToString());
                }

            }
            else
            {

            }
        }

        public ZWL.BLL.ERPProjectCost GetModel(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.CreateItem<ZWL.BLL.ERPProjectCost>(ds.Tables[0].Rows[0]);
            }
            return null;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPProjectCost ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM ERPProjectCost ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetListAndPaging(string strWhere, int currPage, int pageSize)
        {
            var strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPProjectCost ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), currPage, pageSize, "[ID] desc");
        }
        public StringBuilder GetFinancialSql(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select pc.*,(case when js is null then pc.JSJE else isnull(js,0.00) end) 结算金额,isnull(s,0.00) 收款金额,isnull(k,0.00) 开票金额,isnull(CostSum,0.00) CostSums,hetong.HTJEs AS HTJE2,x.XMJF XMJF2,isnull(BudgetSum,0.00) BudgetSum FROM ERPProjectCost pc left outer join (select HTBH,sum(KaiPiaoJE) k,COUNT(HTBH) c from ERPHeTongShouKuan h 
																			join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过') group by htbh) HTSK on pc.HTBH=HTSK.HTBH left outer join (select HTBH,sum(DaoZhangJE) s,COUNT(HTBH) c
                            from ERPHeTongDaoZhang h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过') group by htbh)HTDZ on pc.HTBH=HTDZ.HTBH left outer join (select  ParentId,sum(工资及津贴 +节日补贴+养老统筹 +福利费 +劳动保护费 +住房公积金 +住房补贴 +材料费 +工程出包费 +固定资产+办公费 +差旅费 +水电费 +物业管理费 +交通运输费用 +邮电费用 +维修费用 +会议费 +培训费 +业务招待费 +劳务费 +租赁费 +税金及附加 +安全生产费用 +工会经费+ 印刷费 +其它费用) CostSum from ERPCostDetail group by ParentId) cd on pc.ID=cd.ParentId 
                            left outer join (select beiyong1,HTBH,JSJE js from ERPHTJieSuan where ID in (select max(id) from ERPHTJieSuan group by beiyong1,HTBH)) js on pc.XMBH=js.beiyong1 and pc.HTBH=js.HTBH 
                            left outer join (select sum(HTJE) HTJEs,HTLB,XMID,HTID from ERPHeTong h join ERPNWorkToDo d  on h.NworkToDoID = d.ID  where  (d.StateNow = '正常结束' or d.JieDianName='合同归档') group by XMID, htlb,HTID) hetong on pc.XMBH=hetong.XMID and pc.HTBH=hetong.HTID and hetong.HTLB='收款' 
                            LEFT JOIN ERPXMJBXX x on pc.XMBH=x.XMBH
                            left outer join (select d.ParentId,([工资及津贴]+[工程出包费]+[材料费]+[租赁费]+[劳务费]+[安全生产费用]+[办公费]+[维修费用]+[交通运输费用]+[差旅费]+[邮电费用]+[其它费用]) BudgetSum from [ERPBudgetDetail] d join (
				                  select ParentId,max(Version) Vsersion from [ERPBudgetDetail] group by ParentId) f
				                  on d.ParentId=f.ParentId and d.Version=f.Vsersion) bud on pc.ID = bud.ParentId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return strSql;
        }
        public Pager GetListAndPagingFinancial(string strWhere, int currPage, int pageSize, string orderBy = "[ID] desc")
        {
            var strSql = GetFinancialSql(strWhere);
            return new Pager(strSql.ToString(), currPage, pageSize, orderBy);
        }
        /// <summary>
        /// 重载该函数，返回查询语句，用于导出查询结果到EXCEL中
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="currPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public string GetListAndPagingFinancial(string strWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select pc.*,(case when js is null then pc.JSJE else isnull(js,0.00) end) 结算金额,isnull(s,0.00) 收款金额,isnull(k,0.00) 开票金额,isnull(CostSum,0.00) CostSums,hetong.HTJEs AS HTJE2,x.XMJF XMJF2,isnull(BudgetSum,0.00) BudgetSum FROM ERPProjectCost pc left outer join (select HTBH,sum(KaiPiaoJE) k,COUNT(HTBH) c from ERPHeTongShouKuan h 
																			join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过') group by htbh) HTSK on pc.HTBH=HTSK.HTBH left outer join (select HTBH,sum(DaoZhangJE) s,COUNT(HTBH) c
                            from ERPHeTongDaoZhang h join ERPNWorkToDo d on h.NWorkToDoID=d.ID where StateNow not in ('已被驳回','不通过') group by htbh)HTDZ on pc.HTBH=HTDZ.HTBH left outer join (select  ParentId,sum(工资及津贴 +节日补贴+养老统筹 +福利费 +劳动保护费 +住房公积金 +住房补贴 +材料费 +工程出包费 +固定资产+办公费 +差旅费 +水电费 +物业管理费 +交通运输费用 +邮电费用 +维修费用 +会议费 +培训费 +业务招待费 +劳务费 +租赁费 +税金及附加 +安全生产费用 +工会经费+ 印刷费 +其它费用) CostSum from ERPCostDetail group by ParentId) cd on pc.ID=cd.ParentId 
                            left outer join (select beiyong1,HTBH,JSJE js from ERPHTJieSuan where ID in (select max(id) from ERPHTJieSuan group by beiyong1,HTBH)) js on pc.XMBH=js.beiyong1 and pc.HTBH=js.HTBH 
                            left outer join (select sum(HTJE) HTJEs,HTLB,XMID,HTID from ERPHeTong h join ERPNWorkToDo d  on h.NworkToDoID = d.ID  where  (d.StateNow = '正常结束' or d.JieDianName='合同归档') group by XMID, htlb,HTID) hetong on pc.XMBH=hetong.XMID and pc.HTBH=hetong.HTID and hetong.HTLB='收款' 
                            LEFT JOIN ERPXMJBXX x on pc.XMBH=x.XMBH
                            left outer join (select d.ParentId,([工资及津贴]+[工程出包费]+[材料费]+[租赁费]+[劳务费]+[安全生产费用]+[办公费]+[维修费用]+[交通运输费用]+[差旅费]+[邮电费用]+[其它费用]) BudgetSum from [ERPBudgetDetail] d join (
				                  select ParentId,max(Version) Vsersion from [ERPBudgetDetail] group by ParentId) f
				                  on d.ParentId=f.ParentId and d.Version=f.Vsersion) bud on pc.ID = bud.ParentId ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return strSql.ToString();
        }
        public Pager GetListAndPagingCostassign(string strWhere, int currPage, int pageSize, string condition)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ROW_NUMBER() over(order by xm.ID asc) ID,xm.XMBH 项目编号,xm.XMName 项目名称,xm.HTJE 合同金额,(case when xm.HTJE=0 or xm.HTJE is null then 1 else Convert(decimal(18,2),sum(cd." + condition + ")/xm.HTJE) end) 费用分配率,sum(cd." + condition + ") 费用分配额");
            strSql.Append(" from ERPProjectCost xm,ERPCostDetail cd  where cd.ParentId=xm.ID and " + condition + ">0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" group by xm.XMBH,xm.ID,xm.XMName,xm.HTJE ");
            return new Pager(strSql.ToString(), currPage, pageSize, "[ID] desc");
        }
    }
}
