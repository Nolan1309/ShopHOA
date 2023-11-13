using PagedList;
using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ShopBanHoa.Models;

namespace ShopBanHoa.Model_DAO
{
    
    public class Product_USER
    {
        DataConnection db = new DataConnection();
        public IPagedList<SanPham> GetAccountList(int page ,int pageSize)
        {
            IPagedList<SanPham> pagedList;
            List<SanPham> listds = new List<SanPham>();
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("select * from SanPham", connection))
                {
                    var sanPhamList = new List<SanPham>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sp = new SanPham();

                            sp.MaSP = Convert.ToInt32(reader["MaSP"]);
                            sp.MaDM = Convert.ToInt32(reader["MaDM"]);
                            sp.TenSP = reader["TenSP"].ToString();
                            sp.AnhSP = reader["AnhSP"].ToString();
                            sp.GiaSP = Convert.ToInt32(reader["GiaSP"]);
                            sp.GiaSale = Convert.ToInt32(reader["GiaSale"]);
                            sp.SalePercent = Convert.ToInt32(reader["SalePercent"]);
                            sp.MotaShort = reader["MotaShort"].ToString();
                            sanPhamList.Add(sp);
                        }
                        pagedList = sanPhamList.ToPagedList(page, pageSize);

                    }

                }
            }

            return pagedList;
        }
        public SanPham GetProductItem(string masp)
        {
            SanPham sp = null;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetProductItemUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@masp", masp);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sp = new SanPham();
                            sp.MaSP = Convert.ToInt32(reader["MaSP"]);
                            sp.TenSP = reader["TenSP"].ToString();
                            sp.AnhSP = reader["AnhSP"].ToString();
                            sp.DanhMuc = new Category();
                            sp.DanhMuc.TenDM = reader["TenDM"].ToString();
                            sp.GiaSP = Convert.ToDecimal(reader["GiaSP"]);
                            sp.GiaSale = Convert.ToDecimal(reader["GiaSale"]);
                            sp.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                            sp.SalePercent = Convert.ToInt32(reader["SalePercent"]);
                            sp.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            sp.MotaShort = reader["MotaShort"].ToString();
                            sp.MotaDai = reader["MotaDai"].ToString();
                            sp.SoluongReview = Convert.ToInt32(reader["SoluongReview"]);
                            sp.Trongluong = Convert.ToInt32(reader["Trongluong"]);
                            sp.nguyenlieu = reader["nguyenlieu"].ToString();
                           

                        }
                    }
                }
                connection.Close();
            }
            return sp;
        }
        public List<SanPham> GetProduct_5sanpham()
        {
            List<SanPham> listds = new List<SanPham>();
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT TOP 5 * FROM SanPham", connection))
                {
                   
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sp = new SanPham();

                            sp.MaSP = Convert.ToInt32(reader["MaSP"]);
                            sp.MaDM = Convert.ToInt32(reader["MaDM"]);
                            sp.TenSP = reader["TenSP"].ToString();
                            sp.AnhSP = reader["AnhSP"].ToString();
                            sp.GiaSP = Convert.ToInt32(reader["GiaSP"]);
                            sp.GiaSale = Convert.ToInt32(reader["GiaSale"]);
                            sp.SalePercent = Convert.ToInt32(reader["SalePercent"]);
                            sp.MotaShort = reader["MotaShort"].ToString();
                            listds.Add(sp);
                        }
                    }

                }
            }
            return listds;
        }
    }
    
}