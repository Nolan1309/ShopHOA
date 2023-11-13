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
    public class CategoryModel
    {
        DataConnection db = new DataConnection();
        public CategoryModel()
        {

        }
        public List<Category> Getds()
        {
            string sql = "select MaDM , TenDM from Category";
            List<Category> list = new List<Category>();
            using (SqlConnection connect = db.sqlstring())
            {
                connect.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                adapter.Dispose();
                int lend = dt.Rows.Count;
                Category ds;
                for (int i = 0; i < lend; i++)
                {
                    ds = new Category();
                    ds.MaDM =Convert.ToInt32( dt.Rows[i]["MaDM"].ToString());
                    ds.TenDM = dt.Rows[i]["TenDM"].ToString();
                    list.Add(ds);
                }
                connect.Close();
            }
            return list;
        }
    }
}