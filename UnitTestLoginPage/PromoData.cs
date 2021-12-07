using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLIB
{

    public class PromoData
    {
        public int CodeID { set; get; }
        public string Code { set; get; }   
        public int Percentage { set; get; }

        public Boolean isApplied()
        {
            if (Percentage > 0)
            {
                return true;
            }
            else return false;
        }
        public Boolean Apply(string code)
        {
            var dbCode = new DALPromoCode();
            int percentage = dbCode.Apply(code);
            if (percentage > 0)
            {
                Code = code;
                Percentage = percentage;
                return true;
            }
            else return false;
        }
    }
}
