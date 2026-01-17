using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;//请先添加引用

using ZWL.Common;//请先添加引用
using System.Collections.Generic;

namespace ZWL.BLL
{
    public class ERPCostDetail : ModelBase
    {
        public ERPCostDetail()
        { }
        #region 对象属性
        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public override int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// ID
        /// </summary>		
        private int? _parentid;
        public int? ParentId
        {
            get { return _parentid; }
            set { _parentid = value; }
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
        /// HTBH
        /// </summary>		
        private string _htbh;
        public string HTBH
        {
            get { return _htbh; }
            set { _htbh = value; }
        }
        /// <summary>
        /// 工资及津贴
        /// </summary>		
        private decimal _工资及津贴;
        public decimal 工资及津贴
        {
            get { return _工资及津贴; }
            set { _工资及津贴 = value; }
        }
        /// <summary>
        /// 节日补贴
        /// </summary>		
        private decimal _节日补贴;
        public decimal 节日补贴
        {
            get { return _节日补贴; }
            set { _节日补贴 = value; }
        }
        /// <summary>
        /// 养老统筹
        /// </summary>		
        private decimal _养老统筹;
        public decimal 养老统筹
        {
            get { return _养老统筹; }
            set { _养老统筹 = value; }
        }
        /// <summary>
        /// 福利费
        /// </summary>		
        private decimal _福利费;
        public decimal 福利费
        {
            get { return _福利费; }
            set { _福利费 = value; }
        }
        /// <summary>
        /// 劳动保护费
        /// </summary>		
        private decimal _劳动保护费;
        public decimal 劳动保护费
        {
            get { return _劳动保护费; }
            set { _劳动保护费 = value; }
        }
        /// <summary>
        /// 住房公积金
        /// </summary>		
        private decimal _住房公积金;
        public decimal 住房公积金
        {
            get { return _住房公积金; }
            set { _住房公积金 = value; }
        }
        /// <summary>
        /// 住房补贴
        /// </summary>		
        private decimal _住房补贴;
        public decimal 住房补贴
        {
            get { return _住房补贴; }
            set { _住房补贴 = value; }
        }
        /// <summary>
        /// 材料费
        /// </summary>		
        private decimal _材料费;
        public decimal 材料费
        {
            get { return _材料费; }
            set { _材料费 = value; }
        }
        /// <summary>
        /// 工程出包费
        /// </summary>		
        private decimal _工程出包费;
        public decimal 工程出包费
        {
            get { return _工程出包费; }
            set { _工程出包费 = value; }
        }
        /// <summary>
        /// 钻探出包费
        /// </summary>		
        private decimal _钻探出包费;
        public decimal 钻探出包费
        {
            get { return _钻探出包费; }
            set { _钻探出包费 = value; }
        }
        /// <summary>
        /// 钻孔进尺
        /// </summary>		
        private decimal _钻孔进尺;
        public decimal 钻孔进尺
        {
            get { return _钻孔进尺; }
            set { _钻孔进尺 = value; }
        }
        /// <summary>
        /// 固定资产
        /// </summary>		
        private decimal _固定资产;
        public decimal 固定资产
        {
            get { return _固定资产; }
            set { _固定资产 = value; }
        }
        /// <summary>
        /// 办公费
        /// </summary>		
        private decimal _办公费;
        public decimal 办公费
        {
            get { return _办公费; }
            set { _办公费 = value; }
        }
        /// <summary>
        /// 差旅费
        /// </summary>		
        private decimal _差旅费;
        public decimal 差旅费
        {
            get { return _差旅费; }
            set { _差旅费 = value; }
        }
        /// <summary>
        /// 水电费
        /// </summary>		
        private decimal _水电费;
        public decimal 水电费
        {
            get { return _水电费; }
            set { _水电费 = value; }
        }
        /// <summary>
        /// 物业管理费
        /// </summary>		
        private decimal _物业管理费;
        public decimal 物业管理费
        {
            get { return _物业管理费; }
            set { _物业管理费 = value; }
        }
        /// <summary>
        /// 交通运输费用
        /// </summary>		
        private decimal _交通运输费用;
        public decimal 交通运输费用
        {
            get { return _交通运输费用; }
            set { _交通运输费用 = value; }
        }
        /// <summary>
        /// 邮电费用
        /// </summary>		
        private decimal _邮电费用;
        public decimal 邮电费用
        {
            get { return _邮电费用; }
            set { _邮电费用 = value; }
        }
        /// <summary>
        /// 维修费用
        /// </summary>		
        private decimal _维修费用;
        public decimal 维修费用
        {
            get { return _维修费用; }
            set { _维修费用 = value; }
        }
        /// <summary>
        /// 会议费
        /// </summary>		
        private decimal _会议费;
        public decimal 会议费
        {
            get { return _会议费; }
            set { _会议费 = value; }
        }
        /// <summary>
        /// 培训费
        /// </summary>		
        private decimal _培训费;
        public decimal 培训费
        {
            get { return _培训费; }
            set { _培训费 = value; }
        }
        /// <summary>
        /// 业务招待费
        /// </summary>		
        private decimal _业务招待费;
        public decimal 业务招待费
        {
            get { return _业务招待费; }
            set { _业务招待费 = value; }
        }
        /// <summary>
        /// 劳务费
        /// </summary>		
        private decimal _劳务费;
        public decimal 劳务费
        {
            get { return _劳务费; }
            set { _劳务费 = value; }
        }
        /// <summary>
        /// 租赁费
        /// </summary>		
        private decimal _租赁费;
        public decimal 租赁费
        {
            get { return _租赁费; }
            set { _租赁费 = value; }
        }
        /// <summary>
        /// 税金及附加
        /// </summary>		
        private decimal _税金及附加;
        public decimal 税金及附加
        {
            get { return _税金及附加; }
            set { _税金及附加 = value; }
        }
        /// <summary>
        /// 安全生产费用
        /// </summary>		
        private decimal _安全生产费用;
        public decimal 安全生产费用
        {
            get { return _安全生产费用; }
            set { _安全生产费用 = value; }
        }
        /// <summary>
        /// 工会经费
        /// </summary>		
        private decimal _工会经费;
        public decimal 工会经费
        {
            get { return _工会经费; }
            set { _工会经费 = value; }
        }
        /// <summary>
        /// 其它费用
        /// </summary>		
        private decimal _其它费用;
        public decimal 其它费用
        {
            get { return _其它费用; }
            set { _其它费用 = value; }
        }
        /// <summary>
        /// 期间
        /// </summary>		
        private string _期间;
        public string 期间
        {
            get { return _期间; }
            set { _期间 = value; }
        }
        /// <summary>
        /// beiyong1
        /// </summary>		
        private string _beiyong1;
        public string beiyong1
        {
            get { return _beiyong1; }
            set { _beiyong1 = value; }
        }
        /// <summary>
        /// beiyong2
        /// </summary>		
        private string _beiyong2;
        public string beiyong2
        {
            get { return _beiyong2; }
            set { _beiyong2 = value; }
        }
        /// <summary>
        /// 工资及津贴_备注
        /// </summary>		
        private string _工资及津贴_备注;
        public string 工资及津贴_备注
        {
            get { return _工资及津贴_备注; }
            set { _工资及津贴_备注 = value; }
        }
        /// <summary>
        /// 节日补贴_备注
        /// </summary>		
        private string _节日补贴_备注;
        public string 节日补贴_备注
        {
            get { return _节日补贴_备注; }
            set { _节日补贴_备注 = value; }
        }
        /// <summary>
        /// 养老统筹_备注
        /// </summary>		
        private string _养老统筹_备注;
        public string 养老统筹_备注
        {
            get { return _养老统筹_备注; }
            set { _养老统筹_备注 = value; }
        }
        /// <summary>
        /// 福利费_备注
        /// </summary>		
        private string _福利费_备注;
        public string 福利费_备注
        {
            get { return _福利费_备注; }
            set { _福利费_备注 = value; }
        }
        /// <summary>
        /// 劳动保护费_备注
        /// </summary>		
        private string _劳动保护费_备注;
        public string 劳动保护费_备注
        {
            get { return _劳动保护费_备注; }
            set { _劳动保护费_备注 = value; }
        }
        /// <summary>
        /// 住房公积金_备注
        /// </summary>		
        private string _住房公积金_备注;
        public string 住房公积金_备注
        {
            get { return _住房公积金_备注; }
            set { _住房公积金_备注 = value; }
        }
        /// <summary>
        /// 住房补贴_备注
        /// </summary>		
        private string _住房补贴_备注;
        public string 住房补贴_备注
        {
            get { return _住房补贴_备注; }
            set { _住房补贴_备注 = value; }
        }
        /// <summary>
        /// 材料费_备注
        /// </summary>		
        private string _材料费_备注;
        public string 材料费_备注
        {
            get { return _材料费_备注; }
            set { _材料费_备注 = value; }
        }
        /// <summary>
        /// 工程出包费_备注
        /// </summary>		
        private string _工程出包费_备注;
        public string 工程出包费_备注
        {
            get { return _工程出包费_备注; }
            set { _工程出包费_备注 = value; }
        }
        /// <summary>
        /// 固定资产_备注
        /// </summary>		
        private string _固定资产_备注;
        public string 固定资产_备注
        {
            get { return _固定资产_备注; }
            set { _固定资产_备注 = value; }
        }
        /// <summary>
        /// 办公费_备注
        /// </summary>		
        private string _办公费_备注;
        public string 办公费_备注
        {
            get { return _办公费_备注; }
            set { _办公费_备注 = value; }
        }
        /// <summary>
        /// 差旅费_备注
        /// </summary>		
        private string _差旅费_备注;
        public string 差旅费_备注
        {
            get { return _差旅费_备注; }
            set { _差旅费_备注 = value; }
        }
        /// <summary>
        /// 水电费_备注
        /// </summary>		
        private string _水电费_备注;
        public string 水电费_备注
        {
            get { return _水电费_备注; }
            set { _水电费_备注 = value; }
        }
        /// <summary>
        /// 物业管理费_备注
        /// </summary>		
        private string _物业管理费_备注;
        public string 物业管理费_备注
        {
            get { return _物业管理费_备注; }
            set { _物业管理费_备注 = value; }
        }
        /// <summary>
        /// 交通运输费用_备注
        /// </summary>		
        private string _交通运输费用_备注;
        public string 交通运输费用_备注
        {
            get { return _交通运输费用_备注; }
            set { _交通运输费用_备注 = value; }
        }
        /// <summary>
        /// 邮电费用_备注
        /// </summary>		
        private string _邮电费用_备注;
        public string 邮电费用_备注
        {
            get { return _邮电费用_备注; }
            set { _邮电费用_备注 = value; }
        }
        /// <summary>
        /// 维修费用_备注
        /// </summary>		
        private string _维修费用_备注;
        public string 维修费用_备注
        {
            get { return _维修费用_备注; }
            set { _维修费用_备注 = value; }
        }
        /// <summary>
        /// 会议费_备注
        /// </summary>		
        private string _会议费_备注;
        public string 会议费_备注
        {
            get { return _会议费_备注; }
            set { _会议费_备注 = value; }
        }
        /// <summary>
        /// 培训费_备注
        /// </summary>		
        private string _培训费_备注;
        public string 培训费_备注
        {
            get { return _培训费_备注; }
            set { _培训费_备注 = value; }
        }
        /// <summary>
        /// 业务招待费_备注
        /// </summary>		
        private string _业务招待费_备注;
        public string 业务招待费_备注
        {
            get { return _业务招待费_备注; }
            set { _业务招待费_备注 = value; }
        }
        /// <summary>
        /// 劳务费_备注
        /// </summary>		
        private string _劳务费_备注;
        public string 劳务费_备注
        {
            get { return _劳务费_备注; }
            set { _劳务费_备注 = value; }
        }
        /// <summary>
        /// 租赁费_备注
        /// </summary>		
        private string _租赁费_备注;
        public string 租赁费_备注
        {
            get { return _租赁费_备注; }
            set { _租赁费_备注 = value; }
        }
        /// <summary>
        /// 税金及附加_备注
        /// </summary>		
        private string _税金及附加_备注;
        public string 税金及附加_备注
        {
            get { return _税金及附加_备注; }
            set { _税金及附加_备注 = value; }
        }
        /// <summary>
        /// 安全生产费用_备注
        /// </summary>		
        private string _安全生产费用_备注;
        public string 安全生产费用_备注
        {
            get { return _安全生产费用_备注; }
            set { _安全生产费用_备注 = value; }
        }
        /// <summary>
        /// 工会经费_备注
        /// </summary>		
        private string _工会经费_备注;
        public string 工会经费_备注
        {
            get { return _工会经费_备注; }
            set { _工会经费_备注 = value; }
        }
        /// <summary>
        /// 其它费用_备注
        /// </summary>		
        private string _其它费用_备注;
        public string 其它费用_备注
        {
            get { return _其它费用_备注; }
            set { _其它费用_备注 = value; }
        }
        private decimal _印刷费 = 0M;
        private string _印刷费_备注;
        /// <summary>
        /// 
        /// </summary>
        public decimal 印刷费
        {
            set { _印刷费 = value; }
            get { return _印刷费; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string 印刷费_备注
        {
            set { _印刷费_备注 = value; }
            get { return _印刷费_备注; }
        }
        #endregion
        #region 成员方法
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPCostDetail");
            strSql.Append(" where ");
            strSql.Append(" ID = @ID  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPCostDetail] (");
            strSql.Append("XMBH,HTBH,工资及津贴,节日补贴,养老统筹,福利费,劳动保护费,住房公积金,住房补贴,材料费,工程出包费,固定资产,办公费,差旅费,水电费,物业管理费,交通运输费用,邮电费用,维修费用,会议费,培训费,业务招待费,劳务费,租赁费,税金及附加,安全生产费用,工会经费,其它费用,期间,beiyong1,beiyong2,工资及津贴_备注,节日补贴_备注,养老统筹_备注,福利费_备注,劳动保护费_备注,住房公积金_备注,住房补贴_备注,材料费_备注,工程出包费_备注,固定资产_备注,办公费_备注,差旅费_备注,水电费_备注,物业管理费_备注,交通运输费用_备注,邮电费用_备注,维修费用_备注,会议费_备注,培训费_备注,业务招待费_备注,劳务费_备注,租赁费_备注,税金及附加_备注,安全生产费用_备注,工会经费_备注,其它费用_备注,钻探出包费,钻孔进尺,印刷费,印刷费_备注,ParentId)");
            strSql.Append(" values (");
            strSql.Append("@XMBH,@HTBH,@工资及津贴,@节日补贴,@养老统筹,@福利费,@劳动保护费,@住房公积金,@住房补贴,@材料费,@工程出包费,@固定资产,@办公费,@差旅费,@水电费,@物业管理费,@交通运输费用,@邮电费用,@维修费用,@会议费,@培训费,@业务招待费,@劳务费,@租赁费,@税金及附加,@安全生产费用,@工会经费,@其它费用,@期间,@beiyong1,@beiyong2,@工资及津贴_备注,@节日补贴_备注,@养老统筹_备注,@福利费_备注,@劳动保护费_备注,@住房公积金_备注,@住房补贴_备注,@材料费_备注,@工程出包费_备注,@固定资产_备注,@办公费_备注,@差旅费_备注,@水电费_备注,@物业管理费_备注,@交通运输费用_备注,@邮电费用_备注,@维修费用_备注,@会议费_备注,@培训费_备注,@业务招待费_备注,@劳务费_备注,@租赁费_备注,@税金及附加_备注,@安全生产费用_备注,@工会经费_备注,@其它费用_备注,@钻探出包费,@钻孔进尺,@印刷费,@印刷费_备注,@ParentId)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@工资及津贴", SqlDbType.Decimal,9),
                    new SqlParameter("@节日补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@养老统筹", SqlDbType.Decimal,9),
                    new SqlParameter("@福利费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳动保护费", SqlDbType.Decimal,9),
                    new SqlParameter("@住房公积金", SqlDbType.Decimal,9),
                    new SqlParameter("@住房补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@材料费", SqlDbType.Decimal,9),
                    new SqlParameter("@工程出包费", SqlDbType.Decimal,9),
                    new SqlParameter("@固定资产", SqlDbType.Decimal,9),
                    new SqlParameter("@办公费", SqlDbType.Decimal,9),
                    new SqlParameter("@差旅费", SqlDbType.Decimal,9),
                    new SqlParameter("@水电费", SqlDbType.Decimal,9),
                    new SqlParameter("@物业管理费", SqlDbType.Decimal,9),
                    new SqlParameter("@交通运输费用", SqlDbType.Decimal,9),
                    new SqlParameter("@邮电费用", SqlDbType.Decimal,9),
                    new SqlParameter("@维修费用", SqlDbType.Decimal,9),
                    new SqlParameter("@会议费", SqlDbType.Decimal,9),
                    new SqlParameter("@培训费", SqlDbType.Decimal,9),
                    new SqlParameter("@业务招待费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳务费", SqlDbType.Decimal,9),
                    new SqlParameter("@租赁费", SqlDbType.Decimal,9),
                    new SqlParameter("@税金及附加", SqlDbType.Decimal,9),
                    new SqlParameter("@安全生产费用", SqlDbType.Decimal,9),
                    new SqlParameter("@工会经费", SqlDbType.Decimal,9),
                    new SqlParameter("@其它费用", SqlDbType.Decimal,9),
                    new SqlParameter("@期间", SqlDbType.NVarChar,50),
                    new SqlParameter("@beiyong1", SqlDbType.NVarChar,50),
                    new SqlParameter("@beiyong2", SqlDbType.NVarChar,50),
                    new SqlParameter("@工资及津贴_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@节日补贴_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@养老统筹_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@福利费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@劳动保护费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@住房公积金_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@住房补贴_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@材料费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@工程出包费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@固定资产_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@办公费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@差旅费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@水电费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@物业管理费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@交通运输费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@邮电费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@维修费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@会议费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@培训费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@业务招待费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@劳务费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@租赁费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@税金及附加_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@安全生产费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@工会经费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@其它费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@钻探出包费", SqlDbType.Decimal,9),
                    new SqlParameter("@钻孔进尺", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@ParentId", SqlDbType.Int,4)
            };
            parameters[0].Value = XMBH;
            parameters[1].Value = HTBH;
            parameters[2].Value = 工资及津贴;
            parameters[3].Value = 节日补贴;
            parameters[4].Value = 养老统筹;
            parameters[5].Value = 福利费;
            parameters[6].Value = 劳动保护费;
            parameters[7].Value = 住房公积金;
            parameters[8].Value = 住房补贴;
            parameters[9].Value = 材料费;
            parameters[10].Value = 工程出包费;
            parameters[11].Value = 固定资产;
            parameters[12].Value = 办公费;
            parameters[13].Value = 差旅费;
            parameters[14].Value = 水电费;
            parameters[15].Value = 物业管理费;
            parameters[16].Value = 交通运输费用;
            parameters[17].Value = 邮电费用;
            parameters[18].Value = 维修费用;
            parameters[19].Value = 会议费;
            parameters[20].Value = 培训费;
            parameters[21].Value = 业务招待费;
            parameters[22].Value = 劳务费;
            parameters[23].Value = 租赁费;
            parameters[24].Value = 税金及附加;
            parameters[25].Value = 安全生产费用;
            parameters[26].Value = 工会经费;
            parameters[27].Value = 其它费用;
            parameters[28].Value = 期间;
            parameters[29].Value = beiyong1;
            parameters[30].Value = beiyong2;
            parameters[31].Value = 工资及津贴_备注;
            parameters[32].Value = 节日补贴_备注;
            parameters[33].Value = 养老统筹_备注;
            parameters[34].Value = 福利费_备注;
            parameters[35].Value = 劳动保护费_备注;
            parameters[36].Value = 住房公积金_备注;
            parameters[37].Value = 住房补贴_备注;
            parameters[38].Value = 材料费_备注;
            parameters[39].Value = 工程出包费_备注;
            parameters[40].Value = 固定资产_备注;
            parameters[41].Value = 办公费_备注;
            parameters[42].Value = 差旅费_备注;
            parameters[43].Value = 水电费_备注;
            parameters[44].Value = 物业管理费_备注;
            parameters[45].Value = 交通运输费用_备注;
            parameters[46].Value = 邮电费用_备注;
            parameters[47].Value = 维修费用_备注;
            parameters[48].Value = 会议费_备注;
            parameters[49].Value = 培训费_备注;
            parameters[50].Value = 业务招待费_备注;
            parameters[51].Value = 劳务费_备注;
            parameters[52].Value = 租赁费_备注;
            parameters[53].Value = 税金及附加_备注;
            parameters[54].Value = 安全生产费用_备注;
            parameters[55].Value = 工会经费_备注;
            parameters[56].Value = 其它费用_备注;
            parameters[57].Value = 钻探出包费;
            parameters[58].Value = 钻孔进尺;
            parameters[59].Value = 印刷费;
            parameters[60].Value = 印刷费_备注;
            parameters[61].Value = ParentId;

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
            strSql.Append("update [ERPCostDetail] set ");
            strSql.Append("工资及津贴=@工资及津贴,");
            strSql.Append("节日补贴=@节日补贴,");
            strSql.Append("养老统筹=@养老统筹,");
            strSql.Append("福利费=@福利费,");
            strSql.Append("劳动保护费=@劳动保护费,");
            strSql.Append("住房公积金=@住房公积金,");
            strSql.Append("住房补贴=@住房补贴,");
            strSql.Append("材料费=@材料费,");
            strSql.Append("工程出包费=@工程出包费,");
            strSql.Append("固定资产=@固定资产,");
            strSql.Append("办公费=@办公费,");
            strSql.Append("差旅费=@差旅费,");
            strSql.Append("水电费=@水电费,");
            strSql.Append("物业管理费=@物业管理费,");
            strSql.Append("交通运输费用=@交通运输费用,");
            strSql.Append("邮电费用=@邮电费用,");
            strSql.Append("维修费用=@维修费用,");
            strSql.Append("会议费=@会议费,");
            strSql.Append("培训费=@培训费,");
            strSql.Append("业务招待费=@业务招待费,");
            strSql.Append("劳务费=@劳务费,");
            strSql.Append("租赁费=@租赁费,");
            strSql.Append("税金及附加=@税金及附加,");
            strSql.Append("安全生产费用=@安全生产费用,");
            strSql.Append("工会经费=@工会经费,");
            strSql.Append("其它费用=@其它费用,");
            strSql.Append("期间=@期间,");
            strSql.Append("beiyong1=@beiyong1,");
            strSql.Append("beiyong2=@beiyong2,");
            strSql.Append("工资及津贴_备注=@工资及津贴_备注,");
            strSql.Append("节日补贴_备注=@节日补贴_备注,");
            strSql.Append("养老统筹_备注=@养老统筹_备注,");
            strSql.Append("福利费_备注=@福利费_备注,");
            strSql.Append("劳动保护费_备注=@劳动保护费_备注,");
            strSql.Append("住房公积金_备注=@住房公积金_备注,");
            strSql.Append("住房补贴_备注=@住房补贴_备注,");
            strSql.Append("材料费_备注=@材料费_备注,");
            strSql.Append("工程出包费_备注=@工程出包费_备注,");
            strSql.Append("固定资产_备注=@固定资产_备注,");
            strSql.Append("办公费_备注=@办公费_备注,");
            strSql.Append("差旅费_备注=@差旅费_备注,");
            strSql.Append("水电费_备注=@水电费_备注,");
            strSql.Append("物业管理费_备注=@物业管理费_备注,");
            strSql.Append("交通运输费用_备注=@交通运输费用_备注,");
            strSql.Append("邮电费用_备注=@邮电费用_备注,");
            strSql.Append("维修费用_备注=@维修费用_备注,");
            strSql.Append("会议费_备注=@会议费_备注,");
            strSql.Append("培训费_备注=@培训费_备注,");
            strSql.Append("业务招待费_备注=@业务招待费_备注,");
            strSql.Append("劳务费_备注=@劳务费_备注,");
            strSql.Append("租赁费_备注=@租赁费_备注,");
            strSql.Append("税金及附加_备注=@税金及附加_备注,");
            strSql.Append("安全生产费用_备注=@安全生产费用_备注,");
            strSql.Append("工会经费_备注=@工会经费_备注,");
            strSql.Append("其它费用_备注=@其它费用_备注,");
            strSql.Append("钻探出包费=@钻探出包费,");
            strSql.Append("钻孔进尺=@钻孔进尺,");
            strSql.Append("印刷费=@印刷费,");
            strSql.Append("印刷费_备注=@印刷费_备注,");
            strSql.Append("ParentId=@ParentId");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@工资及津贴", SqlDbType.Decimal,9),
                    new SqlParameter("@节日补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@养老统筹", SqlDbType.Decimal,9),
                    new SqlParameter("@福利费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳动保护费", SqlDbType.Decimal,9),
                    new SqlParameter("@住房公积金", SqlDbType.Decimal,9),
                    new SqlParameter("@住房补贴", SqlDbType.Decimal,9),
                    new SqlParameter("@材料费", SqlDbType.Decimal,9),
                    new SqlParameter("@工程出包费", SqlDbType.Decimal,9),
                    new SqlParameter("@固定资产", SqlDbType.Decimal,9),
                    new SqlParameter("@办公费", SqlDbType.Decimal,9),
                    new SqlParameter("@差旅费", SqlDbType.Decimal,9),
                    new SqlParameter("@水电费", SqlDbType.Decimal,9),
                    new SqlParameter("@物业管理费", SqlDbType.Decimal,9),
                    new SqlParameter("@交通运输费用", SqlDbType.Decimal,9),
                    new SqlParameter("@邮电费用", SqlDbType.Decimal,9),
                    new SqlParameter("@维修费用", SqlDbType.Decimal,9),
                    new SqlParameter("@会议费", SqlDbType.Decimal,9),
                    new SqlParameter("@培训费", SqlDbType.Decimal,9),
                    new SqlParameter("@业务招待费", SqlDbType.Decimal,9),
                    new SqlParameter("@劳务费", SqlDbType.Decimal,9),
                    new SqlParameter("@租赁费", SqlDbType.Decimal,9),
                    new SqlParameter("@税金及附加", SqlDbType.Decimal,9),
                    new SqlParameter("@安全生产费用", SqlDbType.Decimal,9),
                    new SqlParameter("@工会经费", SqlDbType.Decimal,9),
                    new SqlParameter("@其它费用", SqlDbType.Decimal,9),
                    new SqlParameter("@期间", SqlDbType.NVarChar,50),
                    new SqlParameter("@beiyong1", SqlDbType.NVarChar,50),
                    new SqlParameter("@beiyong2", SqlDbType.NVarChar,50),
                    new SqlParameter("@工资及津贴_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@节日补贴_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@养老统筹_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@福利费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@劳动保护费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@住房公积金_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@住房补贴_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@材料费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@工程出包费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@固定资产_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@办公费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@差旅费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@水电费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@物业管理费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@交通运输费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@邮电费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@维修费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@会议费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@培训费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@业务招待费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@劳务费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@租赁费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@税金及附加_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@安全生产费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@工会经费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@其它费用_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@钻探出包费", SqlDbType.Decimal,9),
                    new SqlParameter("@钻孔进尺", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费", SqlDbType.Decimal,9),
                    new SqlParameter("@印刷费_备注", SqlDbType.NVarChar,100),
                    new SqlParameter("@ParentId", SqlDbType.Int,4),
                    new SqlParameter("@ID", SqlDbType.Int,4),
                    new SqlParameter("@XMBH", SqlDbType.NVarChar,50),
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = 工资及津贴;
            parameters[1].Value = 节日补贴;
            parameters[2].Value = 养老统筹;
            parameters[3].Value = 福利费;
            parameters[4].Value = 劳动保护费;
            parameters[5].Value = 住房公积金;
            parameters[6].Value = 住房补贴;
            parameters[7].Value = 材料费;
            parameters[8].Value = 工程出包费;
            parameters[9].Value = 固定资产;
            parameters[10].Value = 办公费;
            parameters[11].Value = 差旅费;
            parameters[12].Value = 水电费;
            parameters[13].Value = 物业管理费;
            parameters[14].Value = 交通运输费用;
            parameters[15].Value = 邮电费用;
            parameters[16].Value = 维修费用;
            parameters[17].Value = 会议费;
            parameters[18].Value = 培训费;
            parameters[19].Value = 业务招待费;
            parameters[20].Value = 劳务费;
            parameters[21].Value = 租赁费;
            parameters[22].Value = 税金及附加;
            parameters[23].Value = 安全生产费用;
            parameters[24].Value = 工会经费;
            parameters[25].Value = 其它费用;
            parameters[26].Value = 期间;
            parameters[27].Value = beiyong1;
            parameters[28].Value = beiyong2;
            parameters[29].Value = 工资及津贴_备注;
            parameters[30].Value = 节日补贴_备注;
            parameters[31].Value = 养老统筹_备注;
            parameters[32].Value = 福利费_备注;
            parameters[33].Value = 劳动保护费_备注;
            parameters[34].Value = 住房公积金_备注;
            parameters[35].Value = 住房补贴_备注;
            parameters[36].Value = 材料费_备注;
            parameters[37].Value = 工程出包费_备注;
            parameters[38].Value = 固定资产_备注;
            parameters[39].Value = 办公费_备注;
            parameters[40].Value = 差旅费_备注;
            parameters[41].Value = 水电费_备注;
            parameters[42].Value = 物业管理费_备注;
            parameters[43].Value = 交通运输费用_备注;
            parameters[44].Value = 邮电费用_备注;
            parameters[45].Value = 维修费用_备注;
            parameters[46].Value = 会议费_备注;
            parameters[47].Value = 培训费_备注;
            parameters[48].Value = 业务招待费_备注;
            parameters[49].Value = 劳务费_备注;
            parameters[50].Value = 租赁费_备注;
            parameters[51].Value = 税金及附加_备注;
            parameters[52].Value = 安全生产费用_备注;
            parameters[53].Value = 工会经费_备注;
            parameters[54].Value = 其它费用_备注;
            parameters[55].Value = 钻探出包费;
            parameters[56].Value = 钻孔进尺;
            parameters[57].Value = 印刷费;
            parameters[58].Value = 印刷费_备注;
            parameters[59].Value = ParentId;
            parameters[60].Value = ID;
            parameters[61].Value = XMBH;
            parameters[62].Value = HTBH;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPCostDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = ID;


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
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPCostDetail ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
            strSql.Append("select *  ");
            strSql.Append("  from ERPCostDetail ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)
            };
            parameters[0].Value = nID;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(string HTBH)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *  ");
            strSql.Append("  from ERPCostDetail ");
            strSql.Append(" where HTBH=@HTBH");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTBH", SqlDbType.NVarChar,50)
            };
            parameters[0].Value = ID;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }

        public List<ZWL.BLL.ERPCostDetail> GetListModel(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return DataTableHelper.ConvertTo<ZWL.BLL.ERPCostDetail>(ds.Tables[0]);
            }
            return null;
        }
        public List<ZWL.BLL.ERPCostDetail> GetListModelByParentId(int parentId)
        {
            return GetListModel("ParentId=" + parentId);
        }
        private void SetPropertyValue(DataSet ds)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    this.ParentId = int.Parse(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["XMBH"] != null)
                {
                    this.XMBH = ds.Tables[0].Rows[0]["XMBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HTBH"] != null)
                {
                    this.HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["工资及津贴"] != null && ds.Tables[0].Rows[0]["工资及津贴"].ToString() != "")
                {
                    this.工资及津贴 = decimal.Parse(ds.Tables[0].Rows[0]["工资及津贴"].ToString());
                }
                if (ds.Tables[0].Rows[0]["节日补贴"] != null && ds.Tables[0].Rows[0]["节日补贴"].ToString() != "")
                {
                    this.节日补贴 = decimal.Parse(ds.Tables[0].Rows[0]["节日补贴"].ToString());
                }
                if (ds.Tables[0].Rows[0]["养老统筹"] != null && ds.Tables[0].Rows[0]["养老统筹"].ToString() != "")
                {
                    this.养老统筹 = decimal.Parse(ds.Tables[0].Rows[0]["养老统筹"].ToString());
                }
                if (ds.Tables[0].Rows[0]["福利费"] != null && ds.Tables[0].Rows[0]["福利费"].ToString() != "")
                {
                    this.福利费 = decimal.Parse(ds.Tables[0].Rows[0]["福利费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["劳动保护费"] != null && ds.Tables[0].Rows[0]["劳动保护费"].ToString() != "")
                {
                    this.劳动保护费 = decimal.Parse(ds.Tables[0].Rows[0]["劳动保护费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["住房公积金"] != null && ds.Tables[0].Rows[0]["住房公积金"].ToString() != "")
                {
                    this.住房公积金 = decimal.Parse(ds.Tables[0].Rows[0]["住房公积金"].ToString());
                }
                if (ds.Tables[0].Rows[0]["住房补贴"] != null && ds.Tables[0].Rows[0]["住房补贴"].ToString() != "")
                {
                    this.住房补贴 = decimal.Parse(ds.Tables[0].Rows[0]["住房补贴"].ToString());
                }
                if (ds.Tables[0].Rows[0]["材料费"] != null && ds.Tables[0].Rows[0]["材料费"].ToString() != "")
                {
                    this.材料费 = decimal.Parse(ds.Tables[0].Rows[0]["材料费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["工程出包费"] != null && ds.Tables[0].Rows[0]["工程出包费"].ToString() != "")
                {
                    this.工程出包费 = decimal.Parse(ds.Tables[0].Rows[0]["工程出包费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["固定资产"] != null && ds.Tables[0].Rows[0]["固定资产"].ToString() != "")
                {
                    this.固定资产 = decimal.Parse(ds.Tables[0].Rows[0]["固定资产"].ToString());
                }
                if (ds.Tables[0].Rows[0]["办公费"] != null && ds.Tables[0].Rows[0]["办公费"].ToString() != "")
                {
                    this.办公费 = decimal.Parse(ds.Tables[0].Rows[0]["办公费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["差旅费"] != null && ds.Tables[0].Rows[0]["差旅费"].ToString() != "")
                {
                    this.差旅费 = decimal.Parse(ds.Tables[0].Rows[0]["差旅费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["水电费"] != null && ds.Tables[0].Rows[0]["水电费"].ToString() != "")
                {
                    this.水电费 = decimal.Parse(ds.Tables[0].Rows[0]["水电费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["物业管理费"] != null && ds.Tables[0].Rows[0]["物业管理费"].ToString() != "")
                {
                    this.物业管理费 = decimal.Parse(ds.Tables[0].Rows[0]["物业管理费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["交通运输费用"] != null && ds.Tables[0].Rows[0]["交通运输费用"].ToString() != "")
                {
                    this.交通运输费用 = decimal.Parse(ds.Tables[0].Rows[0]["交通运输费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["邮电费用"] != null && ds.Tables[0].Rows[0]["邮电费用"].ToString() != "")
                {
                    this.邮电费用 = decimal.Parse(ds.Tables[0].Rows[0]["邮电费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["维修费用"] != null && ds.Tables[0].Rows[0]["维修费用"].ToString() != "")
                {
                    this.维修费用 = decimal.Parse(ds.Tables[0].Rows[0]["维修费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["会议费"] != null && ds.Tables[0].Rows[0]["会议费"].ToString() != "")
                {
                    this.会议费 = decimal.Parse(ds.Tables[0].Rows[0]["会议费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["培训费"] != null && ds.Tables[0].Rows[0]["培训费"].ToString() != "")
                {
                    this.培训费 = decimal.Parse(ds.Tables[0].Rows[0]["培训费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["业务招待费"] != null && ds.Tables[0].Rows[0]["业务招待费"].ToString() != "")
                {
                    this.业务招待费 = decimal.Parse(ds.Tables[0].Rows[0]["业务招待费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["劳务费"] != null && ds.Tables[0].Rows[0]["劳务费"].ToString() != "")
                {
                    this.劳务费 = decimal.Parse(ds.Tables[0].Rows[0]["劳务费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["租赁费"] != null && ds.Tables[0].Rows[0]["租赁费"].ToString() != "")
                {
                    this.租赁费 = decimal.Parse(ds.Tables[0].Rows[0]["租赁费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["税金及附加"] != null && ds.Tables[0].Rows[0]["税金及附加"].ToString() != "")
                {
                    this.税金及附加 = decimal.Parse(ds.Tables[0].Rows[0]["税金及附加"].ToString());
                }
                if (ds.Tables[0].Rows[0]["安全生产费用"] != null && ds.Tables[0].Rows[0]["安全生产费用"].ToString() != "")
                {
                    this.安全生产费用 = decimal.Parse(ds.Tables[0].Rows[0]["安全生产费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["工会经费"] != null && ds.Tables[0].Rows[0]["工会经费"].ToString() != "")
                {
                    this.工会经费 = decimal.Parse(ds.Tables[0].Rows[0]["工会经费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["其它费用"] != null && ds.Tables[0].Rows[0]["其它费用"].ToString() != "")
                {
                    this.其它费用 = decimal.Parse(ds.Tables[0].Rows[0]["其它费用"].ToString());
                }
                if (ds.Tables[0].Rows[0]["期间"] != null)
                {
                    this.期间 = ds.Tables[0].Rows[0]["期间"].ToString();
                }
                if (ds.Tables[0].Rows[0]["beiyong1"] != null)
                {
                    this.beiyong1 = ds.Tables[0].Rows[0]["beiyong1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["beiyong2"] != null)
                {
                    this.beiyong2 = ds.Tables[0].Rows[0]["beiyong2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["工资及津贴_备注"] != null)
                {
                    this.工资及津贴_备注 = ds.Tables[0].Rows[0]["工资及津贴_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["节日补贴_备注"] != null)
                {
                    this.节日补贴_备注 = ds.Tables[0].Rows[0]["节日补贴_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["养老统筹_备注"] != null)
                {
                    this.养老统筹_备注 = ds.Tables[0].Rows[0]["养老统筹_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["福利费_备注"] != null)
                {
                    this.福利费_备注 = ds.Tables[0].Rows[0]["福利费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["劳动保护费_备注"] != null)
                {
                    this.劳动保护费_备注 = ds.Tables[0].Rows[0]["劳动保护费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["住房公积金_备注"] != null)
                {
                    this.住房公积金_备注 = ds.Tables[0].Rows[0]["住房公积金_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["住房补贴_备注"] != null)
                {
                    this.住房补贴_备注 = ds.Tables[0].Rows[0]["住房补贴_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["材料费_备注"] != null)
                {
                    this.材料费_备注 = ds.Tables[0].Rows[0]["材料费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["工程出包费_备注"] != null)
                {
                    this.工程出包费_备注 = ds.Tables[0].Rows[0]["工程出包费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["固定资产_备注"] != null)
                {
                    this.固定资产_备注 = ds.Tables[0].Rows[0]["固定资产_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["办公费_备注"] != null)
                {
                    this.办公费_备注 = ds.Tables[0].Rows[0]["办公费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["差旅费_备注"] != null)
                {
                    this.差旅费_备注 = ds.Tables[0].Rows[0]["差旅费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["水电费_备注"] != null)
                {
                    this.水电费_备注 = ds.Tables[0].Rows[0]["水电费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["物业管理费_备注"] != null)
                {
                    this.物业管理费_备注 = ds.Tables[0].Rows[0]["物业管理费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["交通运输费用_备注"] != null)
                {
                    this.交通运输费用_备注 = ds.Tables[0].Rows[0]["交通运输费用_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["邮电费用_备注"] != null)
                {
                    this.邮电费用_备注 = ds.Tables[0].Rows[0]["邮电费用_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["维修费用_备注"] != null)
                {
                    this.维修费用_备注 = ds.Tables[0].Rows[0]["维修费用_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["会议费_备注"] != null)
                {
                    this.会议费_备注 = ds.Tables[0].Rows[0]["会议费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["培训费_备注"] != null)
                {
                    this.培训费_备注 = ds.Tables[0].Rows[0]["培训费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["业务招待费_备注"] != null)
                {
                    this.业务招待费_备注 = ds.Tables[0].Rows[0]["业务招待费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["劳务费_备注"] != null)
                {
                    this.劳务费_备注 = ds.Tables[0].Rows[0]["劳务费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["租赁费_备注"] != null)
                {
                    this.租赁费_备注 = ds.Tables[0].Rows[0]["租赁费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["税金及附加_备注"] != null)
                {
                    this.税金及附加_备注 = ds.Tables[0].Rows[0]["税金及附加_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["安全生产费用_备注"] != null)
                {
                    this.安全生产费用_备注 = ds.Tables[0].Rows[0]["安全生产费用_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["工会经费_备注"] != null)
                {
                    this.工会经费_备注 = ds.Tables[0].Rows[0]["工会经费_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["其它费用_备注"] != null)
                {
                    this.其它费用_备注 = ds.Tables[0].Rows[0]["其它费用_备注"].ToString();
                }
                if (ds.Tables[0].Rows[0]["钻探出包费"] != null && ds.Tables[0].Rows[0]["钻探出包费"].ToString() != "")
                {
                    this.钻探出包费 = decimal.Parse(ds.Tables[0].Rows[0]["钻探出包费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["钻孔进尺"] != null && ds.Tables[0].Rows[0]["钻孔进尺"].ToString() != "")
                {
                    this.钻孔进尺 = decimal.Parse(ds.Tables[0].Rows[0]["钻孔进尺"].ToString());
                }
                if (ds.Tables[0].Rows[0]["印刷费"] != null && ds.Tables[0].Rows[0]["印刷费"].ToString() != "")
                {
                    this.印刷费 = decimal.Parse(ds.Tables[0].Rows[0]["印刷费"].ToString());
                }
                if (ds.Tables[0].Rows[0]["印刷费_备注"] != null)
                {
                    this.印刷费_备注 = ds.Tables[0].Rows[0]["印刷费_备注"].ToString();
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPCostDetail ");
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
            strSql.Append(" FROM ERPCostDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public Pager GetListAndPaging(string strWhere, int currentPage, int pageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *, (select top 1 XMName from ERPProjectCost b where a.XMBH=b.XMBH) as XMName,(工资及津贴 +节日补贴+养老统筹 +福利费 +劳动保护费 +住房公积金 +住房补贴 +材料费 +工程出包费 +固定资产+办公费 +差旅费 +水电费 +物业管理费 +交通运输费用 +邮电费用 +维修费用 +会议费 +培训费 +业务招待费 +劳务费 +租赁费 +税金及附加 +安全生产费用 +工会经费 +印刷费+其它费用) sum ");
            strSql.Append(" FROM ERPCostDetail a");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return new Pager(strSql.ToString(), currentPage, pageSize);

        }


        #endregion
    }
}





