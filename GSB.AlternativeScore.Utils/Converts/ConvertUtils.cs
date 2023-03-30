using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB.AlternativeScore.Utils.Converts
{
    public static class ExtensionConvertUtils
    {

        public static string ByteToStringUtf8(this byte[] source )
        {
            return Encoding.UTF8.GetString(source, 0, source.Length);
        }

        public static string ByteToStringUtf8RemoveBackslash(this byte[] source)
        {
            return Encoding.UTF8.GetString(source, 0, source.Length).Replace("\"", "");
        }

        public static string ByteToStringHex(this byte[] source)
        {
            return BitConverter.ToString(source).Replace("-", "");
        }

        public static DateTime UnixTimeStampToDateTime(this int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static long DateTimeToUnixTimeStamp(this DateTime itemDate)
        {
            // Unix timestamp is seconds past epoch
            TimeSpan timeSpan = itemDate - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)timeSpan.TotalSeconds;
        }

    }
}
