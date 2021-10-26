using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CNWeb.Helper
{
    public class ConvertDateTime
    {
        /// <summary>
        /// Hàm hỗ trợ chuyển thời gian thường thành unix
        /// Usage: var dt = DateTime.UtcNow;
        ///        int unixTimestamp = DAO.ConvertDateTime.ConvertDateTimeToUnix(dt);
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>int DatetimeUnix</returns>
        public static int ConvertDateTimeToUnix(DateTime dt)
        {
            return Convert.ToInt32((dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        }

        /// <summary>
        /// Hàm hỗ trợ chuyển unix thành thời gian thường
        /// </summary>
        /// <param name="unix"></param>
        /// <returns>Datetime</returns>
        public static DateTime ConvertUnixToDateTime(int unix)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dt.AddSeconds(unix);
        }
    }
}