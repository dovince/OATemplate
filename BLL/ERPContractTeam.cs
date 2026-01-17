using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.Common;//请先添加引用
using ZWL.DBUtility;
using System.Collections.Generic;//Please add references

namespace ZWL.BLL
{
    /// <summary>
    /// 专业承包、劳务分包施工队伍基本信息表
    /// </summary>
    public class ERPContractTeam
    {
        public ERPContractTeam()
        { }
        #region Model
        private int _id;
        private string _companyname;
        private string _address;
        private string _tx_phone;
        private string _tx_fax;
        private string _tx_website;
        private string _tx_postal;
        private string _compantxz;
        private string _superiors;
        private string _corp_name;
        private string _corp_title;
        private string _corp_phone;
        private string _corp_id;
        private string _corp_zzdj;
        private string _fryyzzh;
        private string _sgcbfw;
        private int? _zrs;
        private int? _jigong;
        private int? _pugong;
        private int? _gaojizc;
        private int? _zhongjizc;
        private int? _chujizc;
        private decimal? _maxgongcheng;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 注册地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string TX_phone
        {
            set { _tx_phone = value; }
            get { return _tx_phone; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string TX_fax
        {
            set { _tx_fax = value; }
            get { return _tx_fax; }
        }
        /// <summary>
        /// 网址
        /// </summary>
        public string TX_website
        {
            set { _tx_website = value; }
            get { return _tx_website; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string TX_postal
        {
            set { _tx_postal = value; }
            get { return _tx_postal; }
        }
        /// <summary>
        /// 企业性质
        /// </summary>
        public string CompantXZ
        {
            set { _compantxz = value; }
            get { return _compantxz; }
        }
        /// <summary>
        /// 上级主管单位
        /// </summary>
        public string Superiors
        {
            set { _superiors = value; }
            get { return _superiors; }
        }
        /// <summary>
        /// 法定负责人姓名
        /// </summary>
        public string Corp_name
        {
            set { _corp_name = value; }
            get { return _corp_name; }
        }
        /// <summary>
        /// 法定负责人职称
        /// </summary>
        public string Corp_title
        {
            set { _corp_title = value; }
            get { return _corp_title; }
        }
        /// <summary>
        /// 法定负责人电话
        /// </summary>
        public string Corp_phone
        {
            set { _corp_phone = value; }
            get { return _corp_phone; }
        }
        /// <summary>
        /// 法定负责人身份证号
        /// </summary>
        public string Corp_ID
        {
            set { _corp_id = value; }
            get { return _corp_id; }
        }
        /// <summary>
        /// 企业资质等级
        /// </summary>
        public string Corp_ZZDJ
        {
            set { _corp_zzdj = value; }
            get { return _corp_zzdj; }
        }
        /// <summary>
        /// 法人营业执照号
        /// </summary>
        public string FRYYZZH
        {
            set { _fryyzzh = value; }
            get { return _fryyzzh; }
        }
        /// <summary>
        /// 施工承包范围
        /// </summary>
        public string SGCBFW
        {
            set { _sgcbfw = value; }
            get { return _sgcbfw; }
        }
        /// <summary>
        /// 总人数
        /// </summary>
        public int? ZRS
        {
            set { _zrs = value; }
            get { return _zrs; }
        }
        /// <summary>
        /// 技工
        /// </summary>
        public int? JIGONG
        {
            set { _jigong = value; }
            get { return _jigong; }
        }
        /// <summary>
        /// 普工
        /// </summary>
        public int? PUGONG
        {
            set { _pugong = value; }
            get { return _pugong; }
        }
        /// <summary>
        /// 高级职称
        /// </summary>
        public int? GAOJIZC
        {
            set { _gaojizc = value; }
            get { return _gaojizc; }
        }
        /// <summary>
        /// 中级职称
        /// </summary>
        public int? ZHONGJIZC
        {
            set { _zhongjizc = value; }
            get { return _zhongjizc; }
        }
        /// <summary>
        /// 初级职称
        /// </summary>
        public int? CHUJIZC
        {
            set { _chujizc = value; }
            get { return _chujizc; }
        }
        /// <summary>
        /// 能承担的年最大工程量（万元）
        /// </summary>
        public decimal? MAXGONGCHENG
        {
            set { _maxgongcheng = value; }
            get { return _maxgongcheng; }
        }
        #endregion Model


        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ERPContractTeam(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,CompanyName,Address,TX_phone,TX_fax,TX_website,TX_postal,CompantXZ,Superiors,Corp_name,Corp_title,Corp_phone,Corp_ID,Corp_ZZDJ,FRYYZZH,SGCBFW,ZRS,JIGONG,PUGONG,GAOJIZC,ZHONGJIZC,CHUJIZC,MAXGONGCHENG ");
            strSql.Append(" FROM [ERPContractTeam] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CompanyName"] != null)
                {
                    this.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null)
                {
                    this.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_phone"] != null)
                {
                    this.TX_phone = ds.Tables[0].Rows[0]["TX_phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_fax"] != null)
                {
                    this.TX_fax = ds.Tables[0].Rows[0]["TX_fax"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_website"] != null)
                {
                    this.TX_website = ds.Tables[0].Rows[0]["TX_website"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_postal"] != null)
                {
                    this.TX_postal = ds.Tables[0].Rows[0]["TX_postal"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompantXZ"] != null)
                {
                    this.CompantXZ = ds.Tables[0].Rows[0]["CompantXZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Superiors"] != null)
                {
                    this.Superiors = ds.Tables[0].Rows[0]["Superiors"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_name"] != null)
                {
                    this.Corp_name = ds.Tables[0].Rows[0]["Corp_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_title"] != null)
                {
                    this.Corp_title = ds.Tables[0].Rows[0]["Corp_title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_phone"] != null)
                {
                    this.Corp_phone = ds.Tables[0].Rows[0]["Corp_phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_ID"] != null)
                {
                    this.Corp_ID = ds.Tables[0].Rows[0]["Corp_ID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_ZZDJ"] != null)
                {
                    this.Corp_ZZDJ = ds.Tables[0].Rows[0]["Corp_ZZDJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FRYYZZH"] != null)
                {
                    this.FRYYZZH = ds.Tables[0].Rows[0]["FRYYZZH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SGCBFW"] != null)
                {
                    this.SGCBFW = ds.Tables[0].Rows[0]["SGCBFW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZRS"] != null && ds.Tables[0].Rows[0]["ZRS"].ToString() != "")
                {
                    this.ZRS = int.Parse(ds.Tables[0].Rows[0]["ZRS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JIGONG"] != null && ds.Tables[0].Rows[0]["JIGONG"].ToString() != "")
                {
                    this.JIGONG = int.Parse(ds.Tables[0].Rows[0]["JIGONG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PUGONG"] != null && ds.Tables[0].Rows[0]["PUGONG"].ToString() != "")
                {
                    this.PUGONG = int.Parse(ds.Tables[0].Rows[0]["PUGONG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GAOJIZC"] != null && ds.Tables[0].Rows[0]["GAOJIZC"].ToString() != "")
                {
                    this.GAOJIZC = int.Parse(ds.Tables[0].Rows[0]["GAOJIZC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZHONGJIZC"] != null && ds.Tables[0].Rows[0]["ZHONGJIZC"].ToString() != "")
                {
                    this.ZHONGJIZC = int.Parse(ds.Tables[0].Rows[0]["ZHONGJIZC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHUJIZC"] != null && ds.Tables[0].Rows[0]["CHUJIZC"].ToString() != "")
                {
                    this.CHUJIZC = int.Parse(ds.Tables[0].Rows[0]["CHUJIZC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MAXGONGCHENG"] != null && ds.Tables[0].Rows[0]["MAXGONGCHENG"].ToString() != "")
                {
                    this.MAXGONGCHENG = decimal.Parse(ds.Tables[0].Rows[0]["MAXGONGCHENG"].ToString());
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [ERPContractTeam]");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [ERPContractTeam] (");
            strSql.Append("CompanyName,Address,TX_phone,TX_fax,TX_website,TX_postal,CompantXZ,Superiors,Corp_name,Corp_title,Corp_phone,Corp_ID,Corp_ZZDJ,FRYYZZH,SGCBFW,ZRS,JIGONG,PUGONG,GAOJIZC,ZHONGJIZC,CHUJIZC,MAXGONGCHENG)");
            strSql.Append(" values (");
            strSql.Append("@CompanyName,@Address,@TX_phone,@TX_fax,@TX_website,@TX_postal,@CompantXZ,@Superiors,@Corp_name,@Corp_title,@Corp_phone,@Corp_ID,@Corp_ZZDJ,@FRYYZZH,@SGCBFW,@ZRS,@JIGONG,@PUGONG,@GAOJIZC,@ZHONGJIZC,@CHUJIZC,@MAXGONGCHENG)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@CompanyName", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Address", SqlDbType.NText),
                    new SqlParameter("@TX_phone", SqlDbType.NVarChar,50),
                    new SqlParameter("@TX_fax", SqlDbType.NVarChar,50),
                    new SqlParameter("@TX_website", SqlDbType.NVarChar,50),
                    new SqlParameter("@TX_postal", SqlDbType.NVarChar,50),
                    new SqlParameter("@CompantXZ", SqlDbType.NVarChar,200),
                    new SqlParameter("@Superiors", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Corp_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_title", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_phone", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_ID", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_ZZDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@FRYYZZH", SqlDbType.NVarChar,200),
                    new SqlParameter("@SGCBFW", SqlDbType.NText),
                    new SqlParameter("@ZRS", SqlDbType.Int,4),
                    new SqlParameter("@JIGONG", SqlDbType.Int,4),
                    new SqlParameter("@PUGONG", SqlDbType.Int,4),
                    new SqlParameter("@GAOJIZC", SqlDbType.Int,4),
                    new SqlParameter("@ZHONGJIZC", SqlDbType.Int,4),
                    new SqlParameter("@CHUJIZC", SqlDbType.Int,4),
                    new SqlParameter("@MAXGONGCHENG", SqlDbType.Decimal,9)};
            parameters[0].Value = CompanyName;
            parameters[1].Value = Address;
            parameters[2].Value = TX_phone;
            parameters[3].Value = TX_fax;
            parameters[4].Value = TX_website;
            parameters[5].Value = TX_postal;
            parameters[6].Value = CompantXZ;
            parameters[7].Value = Superiors;
            parameters[8].Value = Corp_name;
            parameters[9].Value = Corp_title;
            parameters[10].Value = Corp_phone;
            parameters[11].Value = Corp_ID;
            parameters[12].Value = Corp_ZZDJ;
            parameters[13].Value = FRYYZZH;
            parameters[14].Value = SGCBFW;
            parameters[15].Value = ZRS;
            parameters[16].Value = JIGONG;
            parameters[17].Value = PUGONG;
            parameters[18].Value = GAOJIZC;
            parameters[19].Value = ZHONGJIZC;
            parameters[20].Value = CHUJIZC;
            parameters[21].Value = MAXGONGCHENG;

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
            strSql.Append("update [ERPContractTeam] set ");
            strSql.Append("CompanyName=@CompanyName,");
            strSql.Append("Address=@Address,");
            strSql.Append("TX_phone=@TX_phone,");
            strSql.Append("TX_fax=@TX_fax,");
            strSql.Append("TX_website=@TX_website,");
            strSql.Append("TX_postal=@TX_postal,");
            strSql.Append("CompantXZ=@CompantXZ,");
            strSql.Append("Superiors=@Superiors,");
            strSql.Append("Corp_name=@Corp_name,");
            strSql.Append("Corp_title=@Corp_title,");
            strSql.Append("Corp_phone=@Corp_phone,");
            strSql.Append("Corp_ID=@Corp_ID,");
            strSql.Append("Corp_ZZDJ=@Corp_ZZDJ,");
            strSql.Append("FRYYZZH=@FRYYZZH,");
            strSql.Append("SGCBFW=@SGCBFW,");
            strSql.Append("ZRS=@ZRS,");
            strSql.Append("JIGONG=@JIGONG,");
            strSql.Append("PUGONG=@PUGONG,");
            strSql.Append("GAOJIZC=@GAOJIZC,");
            strSql.Append("ZHONGJIZC=@ZHONGJIZC,");
            strSql.Append("CHUJIZC=@CHUJIZC,");
            strSql.Append("MAXGONGCHENG=@MAXGONGCHENG");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@CompanyName", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Address", SqlDbType.NText),
                    new SqlParameter("@TX_phone", SqlDbType.NVarChar,50),
                    new SqlParameter("@TX_fax", SqlDbType.NVarChar,50),
                    new SqlParameter("@TX_website", SqlDbType.NVarChar,50),
                    new SqlParameter("@TX_postal", SqlDbType.NVarChar,50),
                    new SqlParameter("@CompantXZ", SqlDbType.NVarChar,200),
                    new SqlParameter("@Superiors", SqlDbType.NVarChar,1000),
                    new SqlParameter("@Corp_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_title", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_phone", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_ID", SqlDbType.NVarChar,50),
                    new SqlParameter("@Corp_ZZDJ", SqlDbType.NVarChar,50),
                    new SqlParameter("@FRYYZZH", SqlDbType.NVarChar,200),
                    new SqlParameter("@SGCBFW", SqlDbType.NText),
                    new SqlParameter("@ZRS", SqlDbType.Int,4),
                    new SqlParameter("@JIGONG", SqlDbType.Int,4),
                    new SqlParameter("@PUGONG", SqlDbType.Int,4),
                    new SqlParameter("@GAOJIZC", SqlDbType.Int,4),
                    new SqlParameter("@ZHONGJIZC", SqlDbType.Int,4),
                    new SqlParameter("@CHUJIZC", SqlDbType.Int,4),
                    new SqlParameter("@MAXGONGCHENG", SqlDbType.Decimal,9),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = CompanyName;
            parameters[1].Value = Address;
            parameters[2].Value = TX_phone;
            parameters[3].Value = TX_fax;
            parameters[4].Value = TX_website;
            parameters[5].Value = TX_postal;
            parameters[6].Value = CompantXZ;
            parameters[7].Value = Superiors;
            parameters[8].Value = Corp_name;
            parameters[9].Value = Corp_title;
            parameters[10].Value = Corp_phone;
            parameters[11].Value = Corp_ID;
            parameters[12].Value = Corp_ZZDJ;
            parameters[13].Value = FRYYZZH;
            parameters[14].Value = SGCBFW;
            parameters[15].Value = ZRS;
            parameters[16].Value = JIGONG;
            parameters[17].Value = PUGONG;
            parameters[18].Value = GAOJIZC;
            parameters[19].Value = ZHONGJIZC;
            parameters[20].Value = CHUJIZC;
            parameters[21].Value = MAXGONGCHENG;
            parameters[22].Value = ID;

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
            strSql.Append("delete from [ERPContractTeam] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
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
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,CompanyName,Address,TX_phone,TX_fax,TX_website,TX_postal,CompantXZ,Superiors,Corp_name,Corp_title,Corp_phone,Corp_ID,Corp_ZZDJ,FRYYZZH,SGCBFW,ZRS,JIGONG,PUGONG,GAOJIZC,ZHONGJIZC,CHUJIZC,MAXGONGCHENG ");
            strSql.Append(" FROM [ERPContractTeam] ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CompanyName"] != null)
                {
                    this.CompanyName = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Address"] != null)
                {
                    this.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_phone"] != null)
                {
                    this.TX_phone = ds.Tables[0].Rows[0]["TX_phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_fax"] != null)
                {
                    this.TX_fax = ds.Tables[0].Rows[0]["TX_fax"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_website"] != null)
                {
                    this.TX_website = ds.Tables[0].Rows[0]["TX_website"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TX_postal"] != null)
                {
                    this.TX_postal = ds.Tables[0].Rows[0]["TX_postal"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CompantXZ"] != null)
                {
                    this.CompantXZ = ds.Tables[0].Rows[0]["CompantXZ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Superiors"] != null)
                {
                    this.Superiors = ds.Tables[0].Rows[0]["Superiors"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_name"] != null)
                {
                    this.Corp_name = ds.Tables[0].Rows[0]["Corp_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_title"] != null)
                {
                    this.Corp_title = ds.Tables[0].Rows[0]["Corp_title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_phone"] != null)
                {
                    this.Corp_phone = ds.Tables[0].Rows[0]["Corp_phone"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_ID"] != null)
                {
                    this.Corp_ID = ds.Tables[0].Rows[0]["Corp_ID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Corp_ZZDJ"] != null)
                {
                    this.Corp_ZZDJ = ds.Tables[0].Rows[0]["Corp_ZZDJ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["FRYYZZH"] != null)
                {
                    this.FRYYZZH = ds.Tables[0].Rows[0]["FRYYZZH"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SGCBFW"] != null)
                {
                    this.SGCBFW = ds.Tables[0].Rows[0]["SGCBFW"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ZRS"] != null && ds.Tables[0].Rows[0]["ZRS"].ToString() != "")
                {
                    this.ZRS = int.Parse(ds.Tables[0].Rows[0]["ZRS"].ToString());
                }
                if (ds.Tables[0].Rows[0]["JIGONG"] != null && ds.Tables[0].Rows[0]["JIGONG"].ToString() != "")
                {
                    this.JIGONG = int.Parse(ds.Tables[0].Rows[0]["JIGONG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PUGONG"] != null && ds.Tables[0].Rows[0]["PUGONG"].ToString() != "")
                {
                    this.PUGONG = int.Parse(ds.Tables[0].Rows[0]["PUGONG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["GAOJIZC"] != null && ds.Tables[0].Rows[0]["GAOJIZC"].ToString() != "")
                {
                    this.GAOJIZC = int.Parse(ds.Tables[0].Rows[0]["GAOJIZC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZHONGJIZC"] != null && ds.Tables[0].Rows[0]["ZHONGJIZC"].ToString() != "")
                {
                    this.ZHONGJIZC = int.Parse(ds.Tables[0].Rows[0]["ZHONGJIZC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CHUJIZC"] != null && ds.Tables[0].Rows[0]["CHUJIZC"].ToString() != "")
                {
                    this.CHUJIZC = int.Parse(ds.Tables[0].Rows[0]["CHUJIZC"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MAXGONGCHENG"] != null && ds.Tables[0].Rows[0]["MAXGONGCHENG"].ToString() != "")
                {
                    this.MAXGONGCHENG = decimal.Parse(ds.Tables[0].Rows[0]["MAXGONGCHENG"].ToString());
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
            strSql.Append(" FROM [ERPContractTeam] ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得多个数据实体
        /// </summary>
        public List<ZWL.BLL.ERPContractTeam> GetModelList(string strWhere)
        {
            var ds = GetList(strWhere);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var dt = ds.Tables[0];
                return DataTableHelper.ConvertTo_1<ZWL.BLL.ERPContractTeam>(dt);
            }
            return new List<ZWL.BLL.ERPContractTeam>();
        }

        /// <summary>
        /// 新增一条记录，如果该记录存在，修改该记录
        /// </summary>
        /// <param name="strWhere">检测该数据是否存在</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(string strWhere, ERPContractTeam model)
        {
            ERPContractTeam Model = new ERPContractTeam();
            string sql = "select ID from ERPContractTeam where " + strWhere;
            string ID = ZWL.DBUtility.DbHelperSQL.GetSHSLInt(sql);
            if ("0" == ID)
            {
                return model.Add();
            }
            else
            {
                Model.GetModel(int.Parse(ID));
                Model = model;
                //this.model = model;
                Model.ID = int.Parse(ID);
                Model.Update();
                return int.Parse(ID);
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            var strmapping = "select ID,ROW_NUMBER() OVER (ORDER BY CompanyName) as 序号,CompanyName as 企业名称,Address as 注册地址,TX_phone as 通讯电话 ,TX_fax as 通讯传真 ,TX_website as 通讯网址 ,TX_postal as 邮政编码 ,CompantXZ as 企业性质 ,Superiors as 上级主管单位 ,Corp_name as 法定负责人" +
                " ,Corp_title as 法定负责人职称  ,Corp_phone as 法定负责人电话 ,Corp_ID as 法定负责人身份证 ,Corp_ZZDJ as 企业资质等级 ,FRYYZZH as 法人营业执照号 ,SGCBFW as 施工承包范围 from ERPContractTeam";
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere + " order by 序号 ";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = "select ID,ROW_NUMBER() OVER (ORDER BY CompanyName) as 序号,CompanyName as 企业名称,Address as 注册地址,TX_phone as 通讯电话 ,TX_fax as 通讯传真 ,TX_website as 通讯网址 ,TX_postal as 邮政编码 ,CompantXZ as 企业性质 ,Superiors as 上级主管单位 ,Corp_name as 法定负责人" +
                " ,Corp_title as 法定负责人职称  ,Corp_phone as 法定负责人电话 ,Corp_ID as 法定负责人身份证 ,Corp_ZZDJ as 企业资质等级 ,FRYYZZH as 法人营业执照号 ,SGCBFW as 施工承包范围 from ERPContractTeam";
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere + " order by 序号 ";
            }
            return new Pager(strSql, cPage, pSize);
        }
    }
    #endregion  Method
}
