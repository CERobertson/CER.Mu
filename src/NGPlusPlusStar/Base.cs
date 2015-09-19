using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGPlusPlusStar
{
    public class Base
    {
        public static readonly string Key;

        static Base()
        {
            var sb = new StringBuilder(); 
            for (int i = 0; i <= (int)char.MaxValue; i++)
            {
                var possibleChar = (char)i;
                if (i.ToString() != string.Empty)
                {
                    sb.Append(possibleChar);
                }
            }
            Base.Key = sb.ToString();
        }
    }
}
