using System;
using System.Diagnostics;
using System.Windows;


namespace BookStoreLIB
{
    public class BookData
    {

        public string isbn { set; get; }
        public int categoryid { set; get; }
        public string title { set; get; }
        public string author { set; get; }
        public double price { set; get; }
        public int supplierid { set; get; }
        public string year { set; get; }
        public string edition { set; get; }
        public string publisher { set; get; }
        private DALBookManage dbUser = new DALBookManage();



        /// <summary>
        /// Constructor for adding books
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="categoryid"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="price"></param>
        /// <param name="supplierid"></param>
        /// <param name="year"></param>
        /// <param name="edition"></param>
        /// <param name="publisher"></param>
        public BookData(String isbn, int categoryid, String title, String author, double price, int supplierid, String year, String edition, String publisher)
        {
            this.isbn = isbn;
            this.categoryid = categoryid;
            this.title = title;
            this.author = author;
            this.price = price;
            this.supplierid = supplierid;
            this.year = year;
            this.edition = edition;
            this.publisher = publisher;
        }


        /// <summary>
        /// Constructor for deleting or getting books
        /// </summary>
        /// <param name="isbn"></param>
        public BookData(String isbn)
        {
            this.isbn = isbn;

        }

        /// <summary>
        /// call AddBookIntoDataBase method from DALBookManage class
        /// </summary>
        /// <returns></returns>
        public int AddBook()
        {

            return dbUser.AddBookIntoDataBase(this.isbn, this.categoryid, this.title, this.author, this.price, this.supplierid, this.year, this.edition, this.publisher);

        }


        /// <summary>
        /// call DeleteBookFromDataBase method from DALBookManage class
        /// </summary>
        /// <returns></returns>
        public int DeleteBook()
        {

            return dbUser.DeleteBookFromDataBase(this.isbn);
        }

        public BookData GetBook()
        {
            return dbUser.GetBookFromDataBase(this.isbn);
        }

    }
}
