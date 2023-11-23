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
	}
}