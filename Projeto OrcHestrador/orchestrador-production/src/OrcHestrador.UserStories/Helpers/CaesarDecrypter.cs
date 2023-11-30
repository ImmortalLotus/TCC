using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrcHestrador.UserStories.Helpers
{
    public static class CaesarDecrypter
    {
        public static string Decrypt(string encryptedText)
        {
            int decryption = 0;
            string decryptText = "";
            int key = 10;
            for (int i = 0; i < encryptedText.Length; i++)
            {
                int encrypted = (int)encryptedText[i];
                decryption = encrypted - key;
                decryptText += Char.ConvertFromUtf32(decryption);                 
            }
            return decryptText;
        }
    }
}
