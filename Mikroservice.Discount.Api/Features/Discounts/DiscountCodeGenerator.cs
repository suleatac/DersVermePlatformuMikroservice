using System.Security.Cryptography;

namespace Mikroservice.Discount.Api.Features.Discounts
{
    public class DiscountCodeGenerator
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public static string GenerateCode(int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                int idx = RandomNumberGenerator.GetInt32(chars.Length);
                stringChars[i] = chars[idx];
            }
            return new string(stringChars);
        }
    }
}
