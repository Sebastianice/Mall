using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallDomain.utils {
    public class NumUtils {


    

        public static string GenOrderNo() {
            byte[] numeric = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int r = numeric.Length;
            Random rand = new();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++) {
                sb.Append(numeric[rand.Next(r)]);
            }
            string timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string result = timestamp + sb.ToString();
            return result;

        }

        // '2,3' 转换为[2,3]
        public List<int> StrToInt(string str) {
            var list=new List<int>();
            foreach (var item in str.Split(","))
            {
                list.Add(int.Parse(item));
            }
            return list;
        }
    }
}
