using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ZWL.DBUtility;
using System.Collections.Generic;
using ZWL.Common;//请先添加引用
namespace ZWL.BLL
{
    /// <summary>
    /// 类ERPHeTongDaoZhang。
    /// </summary>
    public class ERPHeTongDaoZhang
    {
        public ERPHeTongDaoZhang()
        { }
        #region Model
        private int _id;
        private string _htbh;
        private string _htname;
        private string _htbm;
        private string _htbmjbr;//部门经办人
        private string _zylb;//专业类别
        private string _skzh;//收款账户
        private string _kpfs;
        private string _fkdw;
        private decimal _htje;//合同金额
        private decimal _kpje;//开票金额
        private decimal _dzje;//到账金额        
        private DateTime _dztime;//到账时间
        private int _nworktodoid;//收款审批对应nworktodoid

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
        /// 合同部门经办人
        /// </summary>
        public string BMJBR
        {
            set { _htbmjbr = value; }
            get { return _htbmjbr; }
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
        /// 收款账户
        /// </summary>
        public string SKZH
        {
            set { _skzh = value; }
            get { return _skzh; }
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
		/// 到账金额
		/// </summary>
        public decimal DaoZhangJE
        {
            set { _dzje = value; }
            get { return _dzje; }
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
        public DateTime DaoZhangTime
        {
            set { _dztime = value; }
            get { return _dztime; }
        }
        /// <summary>
        /// 收款审批对应nworktodoid
        /// </summary>
        public int NWorkToDoID
        {
            set { _nworktodoid = value; }
            get { return _nworktodoid; }
        }


        #endregion Model

        #region Relative Model
        public ZWL.BLL.ERPUser CurrentUser
        {
            get
            {
                var _currentUser = new ZWL.BLL.ERPUser();
                if (!string.IsNullOrEmpty(BMJBR))
                {
                    var tempUser = new ZWL.BLL.ERPUser().GetModel("UserName='" + BMJBR + "'");
                    if (tempUser != null)
                        _currentUser = tempUser;
                }
                return _currentUser;
            }
        }

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
        public ZWL.BLL.ERPHeTongYuShouKuan CurrentYuShouKuan()
        {
            if (ID > 0 && NWorkToDoID > 0)
            {
                var _currentYuShouKuan = new ZWL.BLL.ERPHeTongYuShouKuan();
                var list = _currentYuShouKuan.GetModelListBySqlWhere(string.Format("ConnectID={0} and ConnectDaoID={1}", NWorkToDoID, ID));
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }

            }
            return null;
        }
        #endregion

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from ERPHeTongDaoZhang");
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
            strSql.Append("insert into ERPHeTongDaoZhang(");
            strSql.Append("HTBH,HTName,BM,BMJBR,SKZH,KaiPiaoFS,FKDW,HTJE,KaiPiaoJE,DaoZhangJE,DaoZhangTime,ZYLB,NWorkToDoID )");
            strSql.Append(" values (");
            strSql.Append("@HTBH,@HTName,@BM,@BMJBR,@SKZH,@KaiPiaoFS,@FKDW,@HTJE,@KaiPiaoJE,@DaoZhangJE,@DaoZhangTime,@ZYLB,@NWorkToDoID )");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTBH", SqlDbType.VarChar,50),
                    new SqlParameter("@HTName", SqlDbType.VarChar,500),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@BMJBR", SqlDbType.VarChar,50),
                    new SqlParameter("@SKZH", SqlDbType.VarChar,200),
                    new SqlParameter("@KaiPiaoFS",SqlDbType.VarChar,50),
                    new SqlParameter("@FKDW",SqlDbType.VarChar,200),
                    new SqlParameter("@HTJE", SqlDbType.Decimal),
                    new SqlParameter("@KaiPiaoJE", SqlDbType.Decimal),
                    new SqlParameter("@DaoZhangJE", SqlDbType.Decimal),
                    new SqlParameter("@DaoZhangTime", SqlDbType.DateTime),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@NWorkToDoID",SqlDbType.Int)
                                        };
            parameters[0].Value = HTBH;
            parameters[1].Value = HTName;
            parameters[2].Value = BM;
            parameters[3].Value = BMJBR;
            parameters[4].Value = SKZH;
            parameters[5].Value = KaiPiaoFS;
            parameters[6].Value = FKDW;
            parameters[7].Value = HTJE;
            parameters[8].Value = KaiPiaoJE;
            parameters[9].Value = DaoZhangJE;
            parameters[10].Value = DaoZhangTime;
            parameters[11].Value = ZYLB;
            parameters[12].Value = NWorkToDoID;
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
        //public void Update(int nworktodoid)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update ERPHeTongShouKuan set ");
        //    strSql.Append("HTName=@HTName,");
        //    strSql.Append("BM=@BM,");
        //    strSql.Append("ZYLB=@ZYLB,");
        //    strSql.Append("HTJE=@HTJE,");
        //    strSql.Append("KaiPiaoJE=@KaiPiaoJE,");
        //    strSql.Append("ShengYuJE=@ShengYuJE,");
        //    strSql.Append("KaiPiaoFS=@KaiPiaoFS,");
        //    strSql.Append("FKDW=@FKDW,");
        //    strSql.Append("NWorkToDoID=@NWorkToDoID");
        //    strSql.Append(" where NWorkToDoID=@NWorkToDoID ");
        //    SqlParameter[] parameters = {	
        //            new SqlParameter("@HTName", SqlDbType.VarChar,500),
        //            new SqlParameter("@BM", SqlDbType.VarChar,50),                 
        //            new SqlParameter("@ZYLB", SqlDbType.VarChar,50),                   
        //            new SqlParameter("@HTJE", SqlDbType.Float), 
        //            new SqlParameter("@KaiPiaoJE", SqlDbType.Float), 
        //            new SqlParameter("@ShengYuJE", SqlDbType.Float),                
        //            new SqlParameter("@KaiPiaoFS", SqlDbType.VarChar,50),
        //            new SqlParameter("@FKDW", SqlDbType.VarChar,200),
        //            new SqlParameter("@NWorkToDoID", SqlDbType.Int,6)};
        //    parameters[0].Value = HTName;
        //    parameters[1].Value = BM;
        //    parameters[2].Value = ZYLB;
        //    parameters[3].Value = HTJE;
        //    parameters[4].Value = KaiPiaoJE;
        //    parameters[5].Value = ShengYuJE;
        //    parameters[6].Value = KaiPiaoFS;
        //    parameters[7].Value = FKDW;
        //    parameters[8].Value = nworktodoid;

        //    DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        //}

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {

            return DbHelperSQL.GetMaxID("ID", "ERPHeTongDaoZhang");
        }
        /// <summary>
		/// 删除一条数据
		/// </summary>
        public void Delete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ERPHeTongDaoZhang ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [ERPHeTongDaoZhang] set ");
            strSql.Append("HTBH=@HTBH,");
            strSql.Append("HTName=@HTName,");
            strSql.Append("BM=@BM,");
            strSql.Append("BMJBR=@BMJBR,");
            strSql.Append("SKZH=@SKZH,");
            strSql.Append("KaiPiaoFS=@KaiPiaoFS,");
            strSql.Append("FKDW=@FKDW,");
            strSql.Append("HTJE=@HTJE,");
            strSql.Append("KaiPiaoJE=@KaiPiaoJE,");
            strSql.Append("DaoZhangJE=@DaoZhangJE,");
            strSql.Append("DaoZhangTime=@DaoZhangTime,");
            strSql.Append("NWorkToDoID=@NWorkToDoID,");
            strSql.Append("ZYLB=@ZYLB");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@HTBH", SqlDbType.VarChar,50),
                    new SqlParameter("@HTName", SqlDbType.VarChar,500),
                    new SqlParameter("@BM", SqlDbType.VarChar,50),
                    new SqlParameter("@BMJBR", SqlDbType.VarChar,50),
                    new SqlParameter("@SKZH", SqlDbType.VarChar,200),
                    new SqlParameter("@KaiPiaoFS", SqlDbType.VarChar,50),
                    new SqlParameter("@FKDW", SqlDbType.VarChar,200),
                    new SqlParameter("@HTJE", SqlDbType.Decimal,9),
                    new SqlParameter("@KaiPiaoJE", SqlDbType.Decimal,9),
                    new SqlParameter("@DaoZhangJE", SqlDbType.Decimal,9),
                    new SqlParameter("@DaoZhangTime", SqlDbType.DateTime),
                    new SqlParameter("@NWorkToDoID", SqlDbType.Int,4),
                    new SqlParameter("@ZYLB", SqlDbType.VarChar,50),
                    new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = HTBH;
            parameters[1].Value = HTName;
            parameters[2].Value = BM;
            parameters[3].Value = BMJBR;
            parameters[4].Value = SKZH;
            parameters[5].Value = KaiPiaoFS;
            parameters[6].Value = FKDW;
            parameters[7].Value = HTJE;
            parameters[8].Value = KaiPiaoJE;
            parameters[9].Value = DaoZhangJE;
            parameters[10].Value = DaoZhangTime;
            parameters[11].Value = NWorkToDoID;
            parameters[12].Value = ZYLB;
            parameters[13].Value = ID;

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
            strSql.Append("select  top 1 ID,HTBH,HTName,BM,BMJBR,SKZH,KaiPiaoFS,FKDW,HTJE,KaiPiaoJE,DaoZhangJE,DaoZhangTime,ZYLB,NWorkToDoID ");
            strSql.Append(" FROM ERPHeTongDaoZhang ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ID", SqlDbType.Int,6)};
            parameters[0].Value = ID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
                BMJBR = ds.Tables[0].Rows[0]["BMJBR"].ToString();
                SKZH = ds.Tables[0].Rows[0]["SKZH"].ToString();
                KaiPiaoFS = ds.Tables[0].Rows[0]["KaiPiaoFS"].ToString();
                FKDW = ds.Tables[0].Rows[0]["FKDW"].ToString();
                HTJE = decimal.Parse(ds.Tables[0].Rows[0]["HTJE"].ToString());
                ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                KaiPiaoJE = decimal.Parse(ds.Tables[0].Rows[0]["KaiPiaoJE"].ToString());
                DaoZhangJE = decimal.Parse(ds.Tables[0].Rows[0]["DaoZhangJE"].ToString());
                if (ds.Tables[0].Rows[0]["DaoZhangTime"].ToString() != "")
                {
                    DaoZhangTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["DaoZhangTime"].ToString());
                }

                ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
                if (ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
                {
                    NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
                }
            }

        }
        //public void GetSTRModel(string htID)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select  top 1 ID,HTBH,HTName,BM,ZYLB,HTJE,KaiPiaoJE,ShengYuJE,SQTime,KaiPiaoFS,FKDW,SKZT,NWorkToDoID ");
        //    strSql.Append(" FROM ERPHeTongShouKuan ");
        //    strSql.Append(" where HTBH=@HTBH ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@HTBH", SqlDbType.VarChar,50)};
        //    parameters[0].Value = htID;

        //    DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
        //        {
        //            ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
        //        }
        //        if (ds.Tables[0].Rows[0]["HTBH"].ToString() != "")
        //        {
        //            HTBH = ds.Tables[0].Rows[0]["HTBH"].ToString();
        //        }
        //        HTName = ds.Tables[0].Rows[0]["HTName"].ToString();
        //        BM = ds.Tables[0].Rows[0]["BM"].ToString();
        //        HTJE = float.Parse(ds.Tables[0].Rows[0]["HTJE"].ToString());
        //        ZYLB = ds.Tables[0].Rows[0]["ZYLB"].ToString();
        //        KaiPiaoJE = float.Parse(ds.Tables[0].Rows[0]["KaiPiaoJE"].ToString());
        //        ShengYuJE = float.Parse(ds.Tables[0].Rows[0]["ShengYuJE"].ToString());
        //        if (ds.Tables[0].Rows[0]["SQTime"].ToString() != "")
        //        {
        //            SQTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["SQTime"].ToString());
        //        }
        //        KaiPiaoFS = ds.Tables[0].Rows[0]["KaiPiaoFS"].ToString();
        //        FKDW = ds.Tables[0].Rows[0]["FKDW"].ToString();
        //        SKZT = ds.Tables[0].Rows[0]["SKZT"].ToString();
        //        if (ds.Tables[0].Rows[0]["NWorkToDoID"].ToString() != "")
        //        {
        //            NWorkToDoID = int.Parse(ds.Tables[0].Rows[0]["NWorkToDoID"].ToString());
        //        }
        //    }

        //}

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



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM ERPHeTongDaoZhang ");
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
            string strmapping = method.getSQLTable("ERPHeTongDaoZhang");
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
        public List<ZWL.BLL.ERPHeTongDaoZhang> GetModelListBySqlWhere(string strWhere)
        {
            var result = new List<ZWL.BLL.ERPHeTongDaoZhang>();
            var source = GetList(strWhere);
            if (source != null && source.Tables.Count > 0)
            {
                foreach (DataRow item in source.Tables[0].Rows)
                {
                    result.Add(DataTableHelper.CreateItem<ZWL.BLL.ERPHeTongDaoZhang>(item));
                }
            }

            return result;
        }

        public List<ZWL.BLL.ERPHeTongDaoZhang> GetModelList(string NWorkToDoID)
        {
            var result = new List<ZWL.BLL.ERPHeTongDaoZhang>();
            var ds = GetList("NWorkToDoID=" + NWorkToDoID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                result = DataTableHelper.ConvertTo<ZWL.BLL.ERPHeTongDaoZhang>(ds.Tables[0]);
            }
            return result;
        }

        /// <summary>
        /// 获取HTML格式的收款金额格式（用于填入合同收款表单的东西）
        /// </summary>
        /// <param name="NworkID"></param>
        /// <returns></returns>
        public static string GetDaoZhangMingXiHTML(string NworkID)
        {
            //获取到账金额和到账日期
            string DZHTML_Model = "<tr height='30' > <td style='word-break: break-all' valign='center' width='200' align='middle'>到账金额</td> <td style='word-break: break-all' valign='center' width='200' align='center'>{0} 元</td><td style='word-break: break-all' valign='center' width='85' align='middle'>到账日期</td> <td style='word-break: break-all' valign='center' width='200' align='center'>{1}</td><td style='word-break: break-all' valign='center' align='middle'>&#8203;</td> <td style='word-break: break-all' valign='center' align='middle'>&#8203;</td>  </tr> ";
            string DZContent = string.Empty;
            decimal? CountPrice = 0.00M;
            ZWL.BLL.ERPHeTongDaoZhang HeTongDaoZhangMingXi = new ZWL.BLL.ERPHeTongDaoZhang();
            List<ZWL.BLL.ERPHeTongDaoZhang> result = HeTongDaoZhangMingXi.GetModelList(NworkID);
            //重新组转到账金额的HTML
            foreach (ZWL.BLL.ERPHeTongDaoZhang item in result)
            {
                DZContent += string.Format(DZHTML_Model, item.DaoZhangJE, Convert.ToDateTime(item.DaoZhangTime).ToString("yyyy-MM-dd"));
                CountPrice += item.DaoZhangJE;
            }

            //添加合计信息行
            if (result.Count > 0)
            {
                DZContent += "<tr height='30' > <td style='word-break: break-all' valign='center' width='200' align='middle'>合计</td> <td style='word-break: break-all' valign='center' width='200' align='center'>" + CountPrice + " 元</td><td style='word-break: break-all' valign='center' width='85' align='middle'></td> <td style='word-break: break-all' valign='center' width='200' align='center'></td><td style='word-break: break-all' valign='center' align='middle'>&#8203;</td> <td style='word-break: break-all' valign='center' align='middle'>&#8203;</td>  </tr>";
            }

            return DZContent;
        }

        #endregion  成员方法
    }
}

