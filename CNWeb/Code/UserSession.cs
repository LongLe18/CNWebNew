using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWeb.Code
{
    [Serializable]
    public class UserSession
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string FullName { set; get; }
        public string Country { set; get; }
        public string City { set; get; }
        public int BirthDay { set; get; }
        public string Career { set; get; }
        public string pathImage { set; get; }
        public string PhoneNumber { set; get; }
        public int Level { set; get; }
        public int Wallet { set; get; }
        public int CartID { set; get; }
        public bool? RequiresVerification { set; get; }
    }
}