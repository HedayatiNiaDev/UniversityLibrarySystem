using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Classes
{
    public class Hash2ID
    {
        public static int HAsh2ID(string hashedID)
        {
            try
            {
                string result = "";
                hashedID = hashedID.Remove(0, 40);
                for (int i = 0; i < hashedID.Length - 5; i++)
                    result += hashedID[i];
                return int.Parse(result);
            }
            catch
            {
                return -1;
            }
        }

        public static string RegisterHAsh2ID(string hashedID)
        {
            string result = "";
            hashedID = hashedID.Remove(0, 20);
            for (int i = 0; i < hashedID.Length - 5; i++)
                result += hashedID[i];
            return result;
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ID2hash(object id)
        {
            Random m = new Random();
            string RandomText = RandomString(40);
            return RandomText + id.ToString() + m.Next(10000, 99999).ToString();
        }
    }
}