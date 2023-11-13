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
    public class AccountModel
    {
        DataConnection db = new DataConnection();
        public AccountModel()
        {

        }
        public bool Login(string taikhoan , string matkhau)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_CheckLogin", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số cho stored procedure
                    cmd.Parameters.Add(new SqlParameter("@TaiKhoan", SqlDbType.VarChar, 100) { Value = taikhoan });
                    cmd.Parameters.Add(new SqlParameter("@MatKhau", SqlDbType.NVarChar, 30) { Value = matkhau });

                    // Thực hiện stored procedure và lấy kết quả
                    int isAuthenticated = (int)cmd.ExecuteScalar();
                    if (isAuthenticated == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                }
            }
          
        }
        public Account getID(string name)
        {
            Account account = null;
            string sql = "select * from Account where email = '"+name+"'";
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account = new Account();
                        account.AccountID =Convert.ToInt32( reader["AccountID"].ToString());
                        account.Email = reader["Email"].ToString();
                        account.MatKhau = reader["MatKhau"].ToString();
                        account.Phone = reader["Phone"].ToString();
                        account.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                        account.IdVaiTro = Convert.ToInt32(reader["IdVaiTro"].ToString());
                    }
                }
            }
            return account;
        }
    }
}