using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopUtils.UtilMethod
{
    public static class StringUtil
    {
        public static string BCryptSaltAndHash(this string source)
        {
            return BCrypt.Net.BCrypt.HashPassword(source);
        }
        public static bool IsCorrectHashSource(this string source, string correctHash)
        {
            if (source == null || correctHash == null) return false;
            return BCrypt.Net.BCrypt.Verify(source, correctHash);
        }
    }
}
