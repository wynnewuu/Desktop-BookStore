using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace BookStoreLIB {
  public class ReviewData {
    public string BookId { set; get; }
    public string BookTitle { set; get; }
    public int UserId { set; get; }
    public string Review { set; get; }

    public DataSet FindReview(string bookId) {
      var dbScore = new DALReviewInfo();
      return dbScore.FindReview(bookId);
    }

    public void AddReview(int userID, string bookId, string bookTitle, string review) {
      var dbScore = new DALReviewInfo();
      int Score = dbScore.AddReview(userID, bookId, bookTitle, review);
    }
  }
}
