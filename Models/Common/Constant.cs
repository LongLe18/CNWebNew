using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public class Constant
    {
        // LEVEL Account => Phan quyen
        public static int LEVEL_USER = 0;
        public static int LEVEL_ADMIN = 1;
        public static int LEVEL_STAFF = 2;
        public static int LEVEL_MANAGER = 3;

        // ID Chuc Vu
        public static int ID_STAFF = 1;
        public static int ID_MANAGER = 2;
        public static int ID_SHIPPER = 3;
        public static int ID_USER = 4;

        // Status FoodCategory
        public static int ID_UNAVAILABLE = 0;
        public static int ID_AVAILABLE = 1;
        public static string STR_AVAILABLE = "Đang phục vụ";
        public static string STR_UNAVAILABLE = "Hết phục vụ";

        // Status BLog
        public static int STATUS_WAIT = 0;
        public static int STATUS_ACCEPTED = 1;
        public static int STATUS_DENIED = 2;

        // Status Order
        public static int STATUS_ORDERED = 0;
        public static int STATUS_PAYMENTED = 1;
        public static int STATUS_DELIVERY = 2;
        public static int STATUS_DELIVERING = 3;
        public static int STATUS_DONE = 4;
        public static int STATUS_CANCEL = 5;
    }
}
