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
        public decimal GiaSP { get; set; }
        public decimal GiaSale { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien
        {
            get
            {
                int giaSpIntPart = (int)GiaSP;
                int giaSaleIntPart = (int)GiaSale;

                // Tính giá trị ThanhTien
                if (giaSaleIntPart == 0)
                {
                    return SoLuong * giaSpIntPart;
                }
                else
                {
                    return SoLuong * giaSaleIntPart;
                }
            }
        }

        public Cart(int maSP)
        {
            MaSP = maSP;
            FetchProductDetails();
        }
        private void FetchProductDetails()
        {
           
            string query = "SELECT MaSP, TenSP, AnhSP, GiaSP,GiaSale FROM SanPham WHERE MaSP = @MaSP";

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
                            MaSP = reader.GetInt32(0);
                            TenSP = reader.GetString(1);
                            AnhSP = reader.GetString(2);
                            GiaSP = reader.GetDecimal(3);
                            GiaSale = reader.GetDecimal(4);
                            SoLuong = 1;
                        }                      
                    }
                }
            }
        }
    }
}