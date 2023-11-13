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
    public class UserModel
    {
        //List<User> itemUser = new List<User>();
        DataConnection db = new DataConnection();
        public IPagedList<Account> GetAccount(int page ,int pageSize)
        {
            IPagedList<Account> pagedList;
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var accountsList = new List<Account>();
                        while (reader.Read())
                        {
                            var account = new Account();
                            account.AccountID = Convert.ToInt32(reader["AccountID"].ToString());
                            account.FullName = reader["FullName"].ToString();
                            account.Email = reader["Email"].ToString();                       
                            account.Phone = reader["Phone"].ToString();
                            account.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            accountsList.Add(account);
                        }
                        pagedList = accountsList.ToPagedList(page, pageSize);
                    }
                }
            }
            return pagedList;
        }
        public IPagedList<Account> GetAccountSearch(string searchstring, int page, int pageSize)
        {
            IPagedList<Account> pagedList;
            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetAccountSearch", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@search", searchstring);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var accountsList = new List<Account>();
                        while (reader.Read())
                        {
                            var account = new Account();
                            account.AccountID = Convert.ToInt32(reader["AccountID"].ToString());
                            account.FullName = reader["FullName"].ToString();
                            account.Email = reader["Email"].ToString();
                            account.Phone = reader["Phone"].ToString();
                            account.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            accountsList.Add(account);
                        }
                        pagedList = accountsList.ToPagedList(page, pageSize);
                    }
                }
            }
            return pagedList;
        }


        public Account GetAccount(int accountID)
        {
            Account user = null;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetAccountDetail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", accountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            user = new Account();
                            user.AccountID = Convert.ToInt32(reader["AccountID"]);
                            user.MatKhau = (string)reader["MatKhau"];
                            user.Phone = (string)reader["Phone"];
                            user.Email = (string)reader["Email"];
                            user.FullName = (string)reader["FullName"];
                            user.VaiTro = new PhanQuyen();
                            user.VaiTro.tenphanquyen = (string)reader["tenphanquyen"];
                        }
                    }
                }
            }

            return user;
        }
        public int Insert(Account user)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AddUserAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                    command.Parameters.AddWithValue("@Phone", user.Phone);
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@IdVaiTro", user.IdVaiTro);
                    rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }

            return rowsAffected;
        }
        public int UpdateAccount(Account user)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UpdateUserAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", user.AccountID);   
                    command.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                    command.Parameters.AddWithValue("@Phone", user.Phone);     
                    command.Parameters.AddWithValue("@FullName", user.FullName);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
        public int DeleteAccount(int accountID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = db.sqlstring())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_DeleteAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", accountID);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }



    }
}