﻿using ShopBanHoa.Models;
using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Model_DAO
{
    public class AccountModel_User
    {
        DataConnection db = new DataConnection();
        public AccountModel_User()
        {

        }
        public bool Login(string taikhoan, string matkhau)
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
            string sql = "select * from Account where email = '" + name + "'";
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account = new Account();
                        account.AccountID = Convert.ToInt32(reader["AccountID"].ToString());
                        account.Email = reader["Email"].ToString();
                        account.MatKhau = reader["MatKhau"].ToString();
                        //account.Phone = reader["Phone"].ToString();
                        //account.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                        //account.IdVaiTro = Convert.ToInt32(reader["IdVaiTro"].ToString());
                    }
                }
            }
            return account;
        }
        public bool CheckMail(string Email)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Account WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho câu truy vấn
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = Email });

                    // Thực hiện câu truy vấn và lấy kết quả
                    int count = (int)cmd.ExecuteScalar();
                    //int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }

        }

        public Account getEmail(string mail)
        {
            Account account = null;
            string sql = "select * from Account where email = '" + mail + "'";
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        account = new Account();
                        account.AccountID = Convert.ToInt32(reader["AccountID"].ToString());
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

        public void SaveAccount(Account account)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                string query = "INSERT INTO Account (Email, MatKhau, Phone, FullName, CreateDate, IdVaiTro) " +
                               "VALUES (@Email, @MatKhau, @Phone, @FullName, @CreateDate, 2)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Add parameters for the query
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = account.Email });
                    cmd.Parameters.Add(new SqlParameter("@MatKhau", SqlDbType.VarChar, 30) { Value = account.MatKhau });
                    cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 20) { Value = account.Phone });
                    cmd.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 100) { Value = account.FullName });
                   
                    cmd.Parameters.Add(new SqlParameter("@CreateDate", SqlDbType.Date) { Value = DateTime.Now });
                   

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void newAccount(string email)
        {
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                string query = "INSERT INTO Account ( Email)";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Thêm tham số cho câu truy vấn

                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = email });


                    // Thực hiện câu truy vấn
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}