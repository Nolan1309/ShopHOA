using Dapper;
using PagedList;
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
    public class ProductModel
    {
        List<SanPham> listsp = new List<SanPham>();
        DataConnection db = new DataConnection();
        public ProductModel()
        {

        }
        public IPagedList<SanPham> GetProduct(int page, int pageSize)
        {
            IPagedList<SanPham> pagedList;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var sanPhamList = new List<SanPham>();

                        while (reader.Read())
                        {
                            var sp = new SanPham();
                            sp.MaSP = Convert.ToInt32(reader["MaSP"]);
                            sp.AnhSP = reader["AnhSP"].ToString();
                            sp.TenSP = reader["TenSP"].ToString();
                            sp.DanhMuc = new Category();
                            sp.DanhMuc.TenDM = reader["TenDM"].ToString();
                            sp.GiaSP = Convert.ToDecimal(reader["GiaSP"]);
                            sp.GiaSale = Convert.ToDecimal(reader["GiaSale"]);
                            sp.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                            sp.SalePercent = Convert.ToInt32(reader["SalePercent"]);
                            sanPhamList.Add(sp);
                        }

                        // Tạo danh sách được phân trang từ danh sách sản phẩm
                        pagedList = sanPhamList.ToPagedList(page, pageSize);
                    }
                }
            }

            return pagedList;
        }
        public IPagedList<SanPham> GetProductSearch(string searchstring ,int page, int pageSize)
        {
            IPagedList<SanPham> pagedList;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetProductSearch", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@search", searchstring);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var sanPhamList = new List<SanPham>();

                        while (reader.Read())
                        {
                            var sp = new SanPham();
                            sp.MaSP = Convert.ToInt32(reader["MaSP"]);
                            sp.AnhSP = reader["AnhSP"].ToString();
                            sp.TenSP = reader["TenSP"].ToString();
                            sp.DanhMuc = new Category();
                            sp.DanhMuc.TenDM = reader["TenDM"].ToString();
                            sp.GiaSP = Convert.ToDecimal(reader["GiaSP"]);
                            sp.GiaSale = Convert.ToDecimal(reader["GiaSale"]);
                            sp.SoLuong = Convert.ToInt32(reader["SoLuong"]);
                            sp.SalePercent = Convert.ToInt32(reader["SalePercent"]);
                            sanPhamList.Add(sp);
                        }

                        // Tạo danh sách được phân trang từ danh sách sản phẩm
                        pagedList = sanPhamList.ToPagedList(page, pageSize);
                    }
                }
            }

            return pagedList;
        }
       
        public SanPham GetProductItem(int masp)
        {
            SanPham sp = null;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetProductItem", connection))
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
        public void insert(SanPham sp)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("AddProduct", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@MaDM", sp.MaDM));
                    cmd.Parameters.Add(new SqlParameter("@TenSP", sp.TenSP));
                    cmd.Parameters.Add(new SqlParameter("@GiaSP", sp.GiaSP));
                    cmd.Parameters.Add(new SqlParameter("@GiaSale", sp.GiaSale));
                    cmd.Parameters.Add(new SqlParameter("@SoLuong", sp.SoLuong));
                    cmd.Parameters.Add(new SqlParameter("@SalePercent", sp.SalePercent));
                    cmd.Parameters.Add(new SqlParameter("@MotaShort", sp.MotaShort));
                    cmd.Parameters.Add(new SqlParameter("@MotaDai", sp.MotaDai));
                    cmd.Parameters.Add(new SqlParameter("@Trongluong", sp.Trongluong));
                    cmd.Parameters.Add(new SqlParameter("@nguyenlieu", sp.nguyenlieu));

                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public void update(SanPham sp)
        {
            using (SqlConnection connection =db.sqlstring())
            {
                using (SqlCommand command = new SqlCommand("UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameters
                    command.Parameters.Add(new SqlParameter("@masp", SqlDbType.Int)).Value = sp.MaSP;
                    command.Parameters.Add(new SqlParameter("@MaDM", SqlDbType.Int)).Value = sp.MaDM;
                    command.Parameters.Add(new SqlParameter("@TenSP", SqlDbType.NVarChar, 200)).Value = sp.TenSP;
                    command.Parameters.Add(new SqlParameter("@GiaSP", SqlDbType.Decimal)).Value = sp.GiaSP;
                    command.Parameters.Add(new SqlParameter("@GiaSale", SqlDbType.Decimal)).Value = sp.GiaSale;
                    command.Parameters.Add(new SqlParameter("@SoLuong", SqlDbType.Int)).Value = sp.SoLuong;
                    command.Parameters.Add(new SqlParameter("@SalePercent", SqlDbType.Int)).Value = sp.SalePercent;
                    command.Parameters.Add(new SqlParameter("@MotaShort", SqlDbType.NVarChar, -1)).Value = sp.MotaShort; // Use -1 for NVARCHAR(MAX)
                    command.Parameters.Add(new SqlParameter("@MotaDai", SqlDbType.NVarChar, -1)).Value = sp.MotaDai; // Use -1 for NVARCHAR(MAX)
                    command.Parameters.Add(new SqlParameter("@soluongreview", SqlDbType.Int)).Value = sp.SoluongReview;
                    command.Parameters.Add(new SqlParameter("@Trongluong", SqlDbType.Int)).Value = sp.Trongluong;
                    command.Parameters.Add(new SqlParameter("@nguyenlieu", SqlDbType.NVarChar, 200)).Value = sp.nguyenlieu;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public int DeleteAccount(int id)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DeleteProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@MaSP", id);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
    }
}