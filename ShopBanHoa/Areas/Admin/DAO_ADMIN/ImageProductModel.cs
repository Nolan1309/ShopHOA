using ShopBanHoa.Connection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Areas.Admin.DAO_ADMIN
{
	public class ImageProductModel
	{
		DataConnection db = new DataConnection();

		public void insertImageProduct(int productId, string url)
		{
			using (SqlConnection connection = db.sqlstring())
			{
				connection.Open();
				string sql = "insert into HinhAnhSanPham values(@MaSP,@DuongDan)";
				SqlCommand command = new SqlCommand(sql, connection);
				command.Parameters.AddWithValue("@MaSP", productId);
				command.Parameters.AddWithValue("@DuongDan", url);
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		public List<string> getImageProduct(int productId)
		{
			List<string> list = new List<string>();
			using (SqlConnection connection = db.sqlstring())
			{
				connection.Open();
				string sql = "select DuongDan from HinhAnhSanPham where MaSP = @MaSP";
				SqlCommand command = new SqlCommand(sql, connection);
				command.Parameters.AddWithValue("@MaSP", productId);
				SqlDataReader reader = command.ExecuteReader();
				while (reader.Read())
				{
					list.Add(reader["DuongDan"].ToString());
				}
				connection.Close();
			}
			return list;
		}

		public void deleteImageProduct(int productId)
		{
			using (SqlConnection connection = db.sqlstring())
			{
				connection.Open();
				string sql = "delete from HinhAnhSanPham where MaSP = @MaSP";
				SqlCommand command = new SqlCommand(sql, connection);
				command.Parameters.AddWithValue("@MaSP", productId);
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
	}
}