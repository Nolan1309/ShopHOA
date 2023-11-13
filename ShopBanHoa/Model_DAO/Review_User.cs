using ShopBanHoa.Connection;
using ShopBanHoa.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Model_DAO
{
    public class Review_User
    {
        DataConnection db = new DataConnection();
        public List<FormReview> getds(int id)
        {
            List<FormReview> listds = new List<FormReview>();
            string sql = "select IdReview, tenkhachhang,Thongtin from FormReview where IdSanpham = " + id;
            using (SqlConnection connection = db.sqlstring())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                adapter.Dispose();
                int lend = dt.Rows.Count;
                FormReview fr = null;
                for (int i = 0; i < lend; i++)
                {
                    fr = new FormReview();
                    fr.IdReview = Convert.ToInt32( dt.Rows[i]["IdReview"].ToString());
                    fr.tenkhachhang = dt.Rows[i]["tenkhachhang"].ToString();
                    fr.Thongtin = dt.Rows[i]["Thongtin"].ToString();
                    listds.Add(fr);
                }
                
            }
            return listds;
        }
    }
}