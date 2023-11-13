using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Models
{
    public class Cart
    {
        DataConnection db = new DataConnection();
        public int MaSP { get; set; }            
        public string TenSP { get; set; }     
        public string AnhSP { get; set; }     
        public decimal GiaSale { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get { return SoLuong * GiaSale; } }
        public Cart(int maSP)
        {
            MaSP = maSP;
            FetchProductDetails();
        }
        private void FetchProductDetails()
        {
          
            string query = "SELECT TenSP, AnhSP, GiaSale, SoLuong FROM SanPham WHERE MaSP = @MaSP";

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaSP", MaSP);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            TenSP = reader.GetString(0);
                            AnhSP = reader.GetString(1);
                            GiaSale = reader.GetDecimal(2);
                            SoLuong = reader.GetInt32(3);
                        }                      
                    }
                }
            }
        }
    }
}