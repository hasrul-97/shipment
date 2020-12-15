using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionHandler.Entities
{
    public class UserDetail
    {
        public string UserGUID { get; set; }
        public string Name { get; set; }
        public string UserID { get; set; }
        public int IsActive { get; set; }
        public string UserType { get; set; }
        public DateTime UserSince { get; set; }
    }
}
