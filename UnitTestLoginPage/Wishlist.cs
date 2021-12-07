/* **********************************************************************************
 * For use by students taking 60-422 (Fall, 2014) to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * **********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BookStoreLIB
{
    public class Wishlist
    {
        public DataTable GetWishlistInfo(int userid)
        {
            DALWishlist wishList = new DALWishlist();
            return wishList.GetWishlistInfo(userid);
        }
    }
}
