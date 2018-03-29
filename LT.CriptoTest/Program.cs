using LT.Cripto;
using LT.RandomGen;
using System;

namespace LT.CriptoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generate an init vector for test
            string IV = RandomGenerator.GenericOnlyAlfaNumericString(16, 0, false);
            string Password;
            CriptoHelper cripto = new CriptoHelper(IV);

            var stringToEncrypt = String.Empty;
            var stringEncrypted = String.Empty;
            for (int i = 0; i < 10000; i++)
            {
                //Generate Password
                Password = RandomGenerator.GenericString(16, 0, false);
                //Encrypt
                stringToEncrypt = RandomGenerator.GenericString(RandomGenerator.GenericInt(300));
                stringEncrypted = cripto.EncryptString(stringToEncrypt, Password);
                //Decrypt
                if (stringToEncrypt != cripto.DecryptString(stringEncrypted, Password))
                    throw new Exception("The strings don't match!!");
                Console.WriteLine($"String {i.ToString()} ok!");
            }
            Console.ReadKey();
            
        }
    }
}
