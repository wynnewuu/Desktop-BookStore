using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTestBookData
    {

        BookData bookdata1 = new BookData("11xx11", 1, "love", "who", 10.1, 2, "2000", "2", "pb");
        BookData bookdata2 = new BookData("11xx11");

        [TestMethod]
        public void TestConstructorAddBook()
        {
            Assert.AreEqual("11xx11", bookdata1.isbn);
            Assert.AreEqual(1, bookdata1.categoryid);
            Assert.AreEqual("love", bookdata1.title);
            Assert.AreEqual("who", bookdata1.author);
            Assert.AreEqual(10.1, bookdata1.price);
            Assert.AreEqual(2, bookdata1.supplierid);
            Assert.AreEqual("2000", bookdata1.year);
            Assert.AreEqual("2", bookdata1.edition);
            Assert.AreEqual("pb", bookdata1.publisher);
        }

        [TestMethod]
        public void TestConstructorDeleteBook()
        {
            Assert.AreEqual("11xx11", bookdata2.isbn);
        }

        [TestMethod]
        public void TestAddBook()
        {
            Assert.AreEqual(1, bookdata1.AddBook());
        }

        [TestMethod]
        public void TestDeleteBook()
        {
           
            Assert.AreEqual(1, bookdata2.DeleteBook());
        }

        [TestMethod]
        public void TestGetBook()
        {

            BookData bookdata3 = new BookData("11xx12", 2, "love2", "who2", 10.2, 2, "2002", "2", "pb2");
            bookdata3.AddBook();
            BookData Bd = new BookData("11xx12");
            BookData bd = Bd.GetBook();

            Assert.AreEqual("11xx12", bd.isbn);
            Assert.AreEqual(2, bd.categoryid);
            Assert.AreEqual("love2", bd.title);
            Assert.AreEqual("who2", bd.author);
            Assert.AreEqual(10.2, bd.price);
            Assert.AreEqual(2, bd.supplierid);
            Assert.AreEqual("2002", bd.year);
            Assert.AreEqual("2 ", bd.edition);
            Assert.AreEqual("pb2", bd.publisher);

            Bd.DeleteBook();
        }

    }
}

