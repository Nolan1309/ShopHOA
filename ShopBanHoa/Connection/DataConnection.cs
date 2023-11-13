using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Connection
{
    public class DataConnection
    {
        string sql;
        public DataConnection()
        {
            //sql = "Data Source=DESKTOP-MEUMH4V;Initial Catalog=ShopBanHoa_Final;Integrated Security=True";
            sql = "Data Source=DESKTOP-MEUMH4V;Initial Catalog=Shop2;Integrated Security=True";
        }
        public SqlConnection sqlstring()
        {
            return new SqlConnection(sql);
        }
    }
}