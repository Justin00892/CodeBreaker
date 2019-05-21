namespace CodeBreaker
{
    public static class AES
    {
        public static string AESEncrypt(string input, string key)
        {
            var output = "";
            var rounds = 0;
            //check key size
            switch (key.Length)
            {
                case 16:
                    rounds = 10;
                    break;
                case 24:
                    rounds = 12;
                    break;
                case 32:
                    rounds = 14;
                    break;
                default: return input;
            }

            for (var i = 0; i < rounds; i++)
            {

            }

            return output;
        }
    }
}