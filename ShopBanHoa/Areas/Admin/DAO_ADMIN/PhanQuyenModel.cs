using ShopBanHoa.Areas.Admin.Models;
using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Areas.Admin.DAO_ADMIN
{
    public class PhanQuyenModel
    {
        DataConnection db = new DataConnection();
        public PhanQuyenModel()
        {

        }
        public List<PhanQuyen> Getds()
        {
            string sql = "select Idphanquyen , tenphanquyen from PhanQuyen";
            List<PhanQuyen> list = new List<PhanQuyen>();
            using (SqlConnection connect = db.sqlstring())
            {
                connect.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                adapter.Dispose();
                int lend = dt.Rows.Count;
                PhanQuyen ds;
                for (int i = 0; i < lend; i++)
                {
                    ds = new PhanQuyen();
                    ds.Idphanquyen = Convert.ToInt32(dt.Rows[i]["Idphanquyen"].ToString());
                    ds.tenphanquyen = dt.Rows[i]["tenphanquyen"].ToString();
                    list.Add(ds);
                }
                connect.Close();
            }
            return list;
        }
    }
}