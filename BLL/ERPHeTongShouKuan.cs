using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using ZWL.Common;//请先添加引用
using System.Collections.Generic;

namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPHeTongShouKuan。
    /// </summary>
    public class ERPHeTongShouKuan
    {
        public ERPHeTongShouKuan()
        { }
        #region Model
        private int _id;
        private string _htbh;
        private string _htname;
        private string _htbm;
        private string _zylb;//专业类别
        private decimal _htje;//合同金额
        private decimal _kpje;//开票金额
        private decimal _syje;//剩余金额
        private string _kpfs;
        private string _fkdw;
        private DateTime _sqtime;//收款申请时间
        private string _skzt;//收款状态
        private int _nworktodoid;//收款审批对应nworktodoid
        private string _nsr;//纳税人识别号码
        private string _dz;//地址、电话
        private string _kaihuhang;//开户行及账号
        public decimal cdaozhang;
        public decimal daozhang;
        public decimal kaipiaos;
        private string _fpbh;//发票编号
        private decimal? _qpje;
        private decimal? _yqqpje;
        private string _sfyjwhz;
        private string _cscs;

        public string HTBH
        {
            set { _htbh = value; }
            get { return _htbh; }
        }
        /// <summary>
        /// id
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 合同名称
        /// </summary>
        public string HTName
        {
            set { _htname = value; }
            get { return _htname; }
        }
        /// <summary>
        /// 合同部门
        /// </summary>
        public string BM
        {
            set { _htbm = value; }
            get { return _htbm; }
        }
        /// <summary>
        /// 专业类别
        /// </summary>
        public string ZYLB
        {
            set { _zylb = value; }
            get { return _zylb; }
        }
        /// <summary>
		/// 合同金额
		/// </summary>
        public decimal HTJE
        {
            set { _htje = value; }
            get { return _htje; }
        }
        /// <summary>
		/// 开票金额
		/// </summary>
        public decimal KaiPiaoJE
        {
            set { _kpje = value; }
            get { return _kpje; }
        }
        /// <summary>
		/// 剩余金额
		/// </summary>
        public decimal ShengYuJE
        {
            set { _syje = value; }
            get { return _syje; }
        }
        /// <summary>
		/// 开票方式
		/// </summary>
        public string KaiPiaoFS
        {
            set { _kpfs = value; }
            get { return _kpfs; }
        }
        /// <summary>
		/// 付款单位
		/// </summary>
        public string FKDW
        {
            set { _fkdw = value; }
            get { return _fkdw; }
        }
        /// <summary>
		/// 收款申请时间
		/// </summary>
        public DateTime SQTime
        {
            set { _sqtime = value; }
            get { return _sqtime; }
        }
        /// <summary>
        /// 收款状态
        /// </summary>
        public string SKZT
        {
            set { _skzt = value; }
            get { return _skzt; }
        }
        /// <summary>
        /// 收款审批对应nworktodoid
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }
        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string NSRnum
        {
            set { _nsr = value; }
            get { return _nsr; }
        }
        /// <summary>
        /// 地址、电话
        /// </summary>
        public string DZ
        {
            set { _dz = value; }
            get { return _dz; }
        }
        /// <summary>
        /// 开户行及账号
        /// </summary>
        public string KaiHuHang
        {
            set { _kaihuhang = value; }
            get { return _kaihuhang; }
        }
        /// <summary>
        /// 发票编号
        /// </summary>
        public string FPBH
        {
            set { _fpbh = value; }
            get { return _fpbh; }
        }
        /// <summary>
        /// 期票金额
        /// </summary>
        public decimal? QPJE
        {
            set { _qpje = value; }
            get { return _qpje; }
        }
        /// <summary>
        /// 其中：逾期期票金额
        /// </summary>
        public decimal? YQQPJE
        {
            set { _yqqpje = value; }
            get { return _yqqpje; }
        }
        /// <summary>
        /// 是否预计为坏账
        /// </summary>
        public string SFYJWHZ
        {
            set { _sfyjwhz = value; }
            get { return _sfyjwhz; }
        }
        /// <summary>
        /// 催收措施
        /// </summary>
        public string CSCS
        {
            set { _cscs = value; }
            get { return _cscs; }
        }
        #endregion Model

        #region Relative Model

        public ZWL.BLL.ERPNWorkToDo CurrentWorkToDo
        {
            get
            {
                var _currentToDo = new ZWL.BLL.ERPNWorkToDo();
                if (NWorkToDoID > 0)
                {
                    _currentToDo.GetModel(NWorkToDoID);
                }
                return _currentToDo;
            }
        }

        private List<ZWL.BLL.ERPHeTongDaoZhang> _subItems = null;
        public List<ZWL.BLL.ERPHeTongDaoZhang> SubItems
        {
            get
            {
                if (_subItems == null)
                    _subItems = new List<ZWL.BLL.ERPHeTongDaoZhang>();
                if (this.ID > 0)
                {
                    var _currentModel = new ZWL.BLL.ERPHeTongDaoZhang();
                    _subItems = _currentModel.GetModelListBySqlWhere("NWorkToDoID=" + this.NWorkToDoID);
                }
                return _subItems;
            }
            set
            {
                _subItems = value;
            }
        }
        #endregion

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPHeTongShouKuan");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ERPHeTongShouKuan(");
            strSql.Append("HTBH,HTName,BM,ZYLB,HTJE,KaiPiaoJE,ShengYuJE,SQTime,KaiPiaoFS,FKDW,SKZT,NSRnum,DZ,KaiHuHang,NWorkToDoID )");
            strSql.Append(" values (");
            strSql.Append("@HTBH,@HTName,@BM,@ZYLB,@HTJE,@KaiPiaoJE,@ShengYuJE,@SQTime,@KaiPiaoFS,@FKDW,@SKZT,@NSRnum,@DZ,@KaiHuHang,@NWorkToDoID )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTBH", SqlDbType.VarChar,50),
                    new SqlParameter("@HTName", SqlDbType.VarChar,500),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@HTJE", SqlDbType.Decimal),
                    new SqlParameter("@KaiPiaoJE", SqlDbType.Decimal),
                    new SqlParameter("@ShengYuJE", SqlDbType.Decimal),
                    new SqlParameter("@SQTime", SqlDbType.DateTime),
                    new SqlParameter("@KaiPiaoFS",SqlDbType.VarChar,50),
                    new SqlParameter("@FKDW",SqlDbType.VarChar,200),
                    new SqlParameter("@SKZT",SqlDbType.VarChar,50),
                    new SqlParameter("@NSRnum", SqlDbType.VarChar,200),
                    new SqlParameter("@DZ", SqlDbType.VarChar,200),
                    new SqlParameter("@KaiHuHang", SqlDbType.VarChar,200),
                    new SqlParameter("@NWorkToDoID",SqlDbType.Int)
                                        };
            parameters[0].Value = HTBH;
            parameters[1].Value = HTName;
            parameters[2].Value = BM;
            parameters[3].Value = ZYLB;
            parameters[4].Value = HTJE;
            parameters[5].Value = KaiPiaoJE;
            parameters[6].Value = ShengYuJE;
            parameters[7].Value = SQTime;
            parameters[8].Value = KaiPiaoFS;
            parameters[9].Value = FKDW;
            parameters[10].Value = SKZT;
            parameters[11].Value = NSRnum;
            parameters[12].Value = DZ;
            parameters[13].Value = KaiHuHang;
            parameters[14].Value = NWorkToDoID;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 根据NWorkToDoID更新一条数据
        /// </summary>
        public void Update(int nworktodoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ERPHeTongShouKuan set ");
            strSql.Append("HTName=@HTName,");
            strSql.Append("BM=@BM,");
            strSql.Append("ZYLB=@ZYLB,");
            strSql.Append("HTJE=@HTJE,");
            strSql.Append("KaiPiaoJE=@KaiPiaoJE,");
            strSql.Append("ShengYuJE=@ShengYuJE,");
            strSql.Append("KaiPiaoFS=@KaiPiaoFS,");
            strSql.Append("FKDW=@FKDW,");
            strSql.Append("NSRnum=@NSRnum,");
            strSql.Append("DZ=@DZ,");
            strSql.Append("KaiHuHang=@KaiHuHang,");
            strSql.Append("NWorkToDoID=@NWorkToDoID");
            strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTName", SqlDbType.VarChar,500),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@HTJE", SqlDbType.Decimal),
                    new SqlParameter("@KaiPiaoJE", SqlDbType.Decimal),
                    new SqlParameter("@ShengYuJE", SqlDbType.Decimal),
                    new SqlParameter("@KaiPiaoFS", SqlDbType.VarChar,50),
                    new SqlParameter("@FKDW", SqlDbType.VarChar,200),
                    new SqlParameter("@NSRnum", SqlDbType.VarChar,200),
                    new SqlParameter("@DZ", SqlDbType.VarChar,200),
                    new SqlParameter("@KaiHuHang", SqlDbType.VarChar,200),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
            parameters[0].Value = HTName;
            parameters[1].Value = BM;
            parameters[2].Value = ZYLB;
            parameters[3].Value = HTJE;
            parameters[4].Value = KaiPiaoJE;
            parameters[5].Value = ShengYuJE;
            parameters[6].Value = KaiPiaoFS;
            parameters[7].Value = FKDW;
            parameters[8].Value = NSRnum;
            parameters[9].Value = DZ;
            parameters[10].Value = KaiHuHang;
            parameters[11].Value = nworktodoid;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPHeTongShouKuan");
        }
        /// <summary>
		/// 删除一条数据
		/// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPHeTongShouKuan ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,HTBH,HTName,BM,ZYLB,HTJE,KaiPiaoJE,ShengYuJE,SQTime,KaiPiaoFS,FKDW,SKZT,NSRnum,DZ,KaiHuHang,NWorkToDoID ");
            strSql.Append(" FROM ERPHeTongShouKuan ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            SetPropertyValue(ds);
        }
        private void SetPropertyValue(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    this.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTBH"].ToString() != "")
                {
                    HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                HTName = ds.Tables[0].Rows[0]["HTName"].ToString();
                BM = ds.Tables[0].Rows[0]["BM"].ToString();
                HTJE = decimal.Parse(ds.Tables[0].Rows[0]["HTJE"].ToString());
                ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                KaiPiaoJE = decimal.Parse(ds.Tables[0].Rows[0]["KaiPiaoJE"].ToString());
                ShengYuJE = decimal.Parse(ds.Tables[0].Rows[0]["ShengYuJE"].ToString());
                if (ds.Tables[0].Rows[0]["SQTime"].ToString() != "")
                {
                    SQTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["SQTime"].ToString());
                }
                KaiPiaoFS = ds.Tables[0].Rows[0]["KaiPiaoFS"].ToString();
                FKDW = ds.Tables[0].Rows[0]["FKDW"].ToString();
                SKZT = ds.Tables[0].Rows[0]["SKZT"].ToString();

                NSRnum = ds.Tables[0].Rows[0]["NSRnum"].ToString();
                DZ = ds.Tables[0].Rows[0]["DZ"].ToString();
                KaiHuHang = ds.Tables[0].Rows[0]["KaiHuHang"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
            }
        }
        public void GetSTRModel(string htID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,HTBH,HTName,BM,ZYLB,HTJE,KaiPiaoJE,ShengYuJE,SQTime,KaiPiaoFS,FKDW,SKZT,NSRnum,DZ,KaiHuHang,NWorkToDoID ");
            strSql.Append(" FROM ERPHeTongShouKuan ");
            strSql.Append(" where HTBH=@HTBH ");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTBH", SqlDbType.VarChar,50)};
            parameters[0].Value = htID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HTBH"].ToString() != "")
                {
                    HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
                }
                HTName = ds.Tables[0].Rows[0]["HTName"].ToString();
                BM = ds.Tables[0].Rows[0]["BM"].ToString();
                HTJE = decimal.Parse(ds.Tables[0].Rows[0]["HTJE"].ToString());
                ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                KaiPiaoJE = decimal.Parse(ds.Tables[0].Rows[0]["KaiPiaoJE"].ToString());
                ShengYuJE = decimal.Parse(ds.Tables[0].Rows[0]["ShengYuJE"].ToString());
                if (ds.Tables[0].Rows[0]["SQTime"].ToString() != "")
                {
                    SQTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["SQTime"].ToString());
                }
                KaiPiaoFS = ds.Tables[0].Rows[0]["KaiPiaoFS"].ToString();
                FKDW = ds.Tables[0].Rows[0]["FKDW"].ToString();
                SKZT = ds.Tables[0].Rows[0]["SKZT"].ToString();
                NSRnum = ds.Tables[0].Rows[0]["NSRnum"].ToString();
                DZ = ds.Tables[0].Rows[0]["DZ"].ToString();
                KaiHuHang = ds.Tables[0].Rows[0]["KaiHuHang"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
            }

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        //public ERPHeTongShouKuan(string htID)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select * ");
        //    strSql.Append(" FROM ERPHeTongShouKuan ");
        //    strSql.Append(" where HTBH=@HTBH ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@HTBH", SqlDbType.VarChar,50)};
        //    parameters[0].Value = htID;

        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
        //        HTName = ds.Tables[0].Rows[0]["HTName"].ToString();
        //        BM = ds.Tables[0].Rows[0]["BM"].ToString();
        //        ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
        //        HTJE = ds.Tables[0].Rows[0]["HTJE"].ToString();
        //        KaiPiaoJE = ds.Tables[0].Rows[0]["KaiPiaoJE"].ToString();
        //        ShengYuJE = ds.Tables[0].Rows[0]["ShengYuJE"].ToString();
        //        KaiPiaoFS = ds.Tables[0].Rows[0]["KaiPiaoFS"].ToString();
        //        FKDW = ds.Tables[0].Rows[0]["FKDW"].ToString();                

        //        if (ds.Tables[0].Rows[0]["SQTime"].ToString() == "")
        //        {
        //            SQTime = DateTime.Parse("2000/01/01");
        //        }
        //        else
        //        {
        //            SQTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["SQTime"].ToString());
        //        }

        //    }
        //}
        public void GetModelByNWorkId(int workid)
        {
            var ds = GetList("NWorkToDoID=" + workid);
            SetPropertyValue(ds);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<ZWL.BLL.ERPHeTongShouKuan> GetModelListBySqlWhere(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPHeTongShouKuan>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPHeTongShouKuan>(item));
                }
            }

            return result;
        }

        public List<ZWL.BLL.ERPHeTongShouKuan> GetModelList(string NWorkToDoID)
        {
            var result = new List<ZWL.BLL.ERPHeTongShouKuan>();
            var ds = GetList("NWorkToDoID=" + NWorkToDoID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPHeTongShouKuan>(ds.Tables[0]);
            }
            return result;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPHeTongShouKuan ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListMapping(string strWhere)
        {
            string strSql = "";
            ZWL.Common.PublicMethod method = new ZWL.Common.PublicMethod();
            string strmapping = method.getSQLTable("ERPHeTongShouKuan");
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere + " order by 序号 desc";
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public Pager GetListMappingAndPaging(string strWhere, int cPage, int pSize)
        {
            var strSql = "";
            var method = new ZWL.Common.PublicMethod();
            var strmapping = method.getSQLTable("ERPHeTongShouKuan");
            strSql = "select * from (" + strmapping + ") as LB_MrALLFint";
            if (strWhere.Trim() != "")
            {
                strSql += " where " + strWhere;
            }
            return new Pager(strSql, cPage, pSize);
        }

        #endregion  成员方法
    }
}

