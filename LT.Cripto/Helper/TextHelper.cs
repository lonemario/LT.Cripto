using System;
using System.Collections.Generic;
using System.Text;

namespace LT.Cripto.Helper
{
    public static class TextHelper
    {
        /// <summary>
        /// Check if string is UTF8 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUnicode(string input)
        {
            var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
            var unicodBytesCount = Encoding.UTF8.GetByteCount(input);
            return asciiBytesCount == unicodBytesCount;
        }


        //private static string NormalizePassPhrase(string passPhrase, string fillerPhrase)
        //{
        //    if (passPhrase.Length > 16)
        //    {
        //        return passPhrase.Substring(0, 16);
        //    }
        //    else
        //    {
        //        if (passPhrase.Length == 16)
        //        {
        //            return passPhrase;
        //        }
        //        else
        //        {
        //            return (passPhrase + fillerPhrase).Substring(0, 16);
        //        }
        //    }
        //}
    }
}
