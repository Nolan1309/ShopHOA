using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBanHoa.Areas.Admin.Code
{
    [Serializable]
    public class UserSession
    {
        public long ID { get; set; }
        public string UserName { get; set; }
    }
}