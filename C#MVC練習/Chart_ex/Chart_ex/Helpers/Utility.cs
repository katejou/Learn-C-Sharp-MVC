using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_APi_Ex.Helpers
{
    public class Utility
    {
        public int[] getNumbers(int num) 
        {
            Random rdn = new Random(Guid.NewGuid().GetHashCode());
            int[] Nums = new int[num];
            for (int i = 0; i < num; i++)
            {
                Nums[i] = rdn.Next(1,500);
            }
            return Nums;
        }
    }
}