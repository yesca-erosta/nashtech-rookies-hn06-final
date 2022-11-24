namespace Common.Constants
{
    public static class SystemFunction
    {
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
