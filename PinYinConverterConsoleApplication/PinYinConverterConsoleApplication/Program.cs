using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinYinConverterConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                string chinese = Console.ReadLine();
                Console.WriteLine(GetPinyin(chinese));
                Console.WriteLine(GetFirstPinyin(chinese));
            }
        }

        public static string GetPinyin(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char obj in str)
            {
                ChineseChar chineseChar = new ChineseChar(obj);
                foreach(string pinyin in chineseChar.Pinyins)
                {
                    sb.Append(pinyin);
                }
            }
            return sb.ToString();
        }

        /// <summary> 
        /// 汉字转化为拼音首字母
        /// </summary> 
        /// <param name="str">汉字</param> 
        /// <returns>首字母</returns> 
        public static string GetFirstPinyin(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(obj);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }

    }
}
