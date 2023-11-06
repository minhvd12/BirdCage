using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopUtils.UtilMethod
{
    public class VoucherCodeGenerator
    {
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Random = new Random();

        public static string GenerateUniqueVoucherCode(int length = 8)
        {
            
            string voucherCode = new string(Enumerable.Repeat(Characters, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());

            return voucherCode;
        }
    }
}
