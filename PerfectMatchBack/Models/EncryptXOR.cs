using System.Text;

namespace PerfectMatchBack.Models
{
    public class EncryptXOR
    {
        string key = "doPerroibanAbajo";

        public string DecryptXOR(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            for (int i = 0; i < cipherBytes.Length; i++)
            {
                cipherBytes[i] = (byte)(cipherBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Encoding.UTF8.GetString(cipherBytes);
        }
    }
  
}
