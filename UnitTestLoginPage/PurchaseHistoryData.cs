using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLIB
{
    public class PurchaseHistoryData
    {
        //public DataSet dsTopSellingBooks { get; set; }

        public DataTable getTopSellingBooks() {
            var dbPurchaseHistory = new DALPurchaseHistory();
            return dbPurchaseHistory.ListTopSellingBooks();
        }


    }
}
