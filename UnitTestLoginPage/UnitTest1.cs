using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest1
    {
        UserData userData = new UserData();
        string inputName, inputPassword, fullName;
        int actualUserId;
        [TestMethod]
        public void TestMethod1()
        {
            // specify the value of test inputs
            inputName = "wu154";
            inputPassword = "wu1234";
            // specify the value of expected outputs
            Boolean expectedReturn = true;
            int expectedUserId = 1;
            // obtain the actual outputs by calling the method under testing
            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            actualUserId = userData.UserID;
            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);
        }
        [TestMethod]
        public void TestMethod2()
        {
            // specify the value of test inputs
            inputName = "renau121";
            inputPassword = "re";
            // specify the value of expected outputs
            Boolean expectedReturn = false;
            int expectedUserId = -1;
            // obtain the actual outputs by calling the method under testing
            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            actualUserId = userData.UserID;
            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);
        }
        [TestMethod]
        public void TestMethod3()
        {
            // specify the value of test inputs
            inputName = "";
            inputPassword = "";
            // specify the value of expected outputs
            Boolean expectedReturn = false;
            int expectedUserId = -1;
            // obtain the actual outputs by calling the method under testing
            Boolean actualReturn = userData.LogIn(inputName, inputPassword);
            actualUserId = userData.UserID;
            Assert.AreEqual(expectedReturn, actualReturn);
            Assert.AreEqual(expectedUserId, actualUserId);
        }
        [TestMethod]
        public void TestMethod4()
        {
            // specify the value of test inputs
            inputName = "testUser12";
            inputPassword = "ts1234";
            fullName = "Im Batman";
            // specify the value of expected outputs
            Boolean expectedReturn = true;
            // obtain the actual outputs by calling the method under testing
            Boolean actualReturn = userData.Register(inputName, inputPassword, "RG", false, fullName);
            actualUserId = userData.UserID;
            Assert.AreEqual(expectedReturn, actualReturn);

            // Remove user after use of test
            expectedReturn = true;
            
            actualReturn = userData.deleteAccount();
            Assert.AreEqual(expectedReturn, actualReturn);

        }
        [TestMethod]
        public void TestMethod5()
        {
            // specify the value of test inputs
            inputName = "testUser13";
            inputPassword = "ts1234";
            fullName = "Im Robin";
            // specify the value of expected outputs
            Boolean expectedReturn = true;
            // obtain the actual outputs by calling the method under testing
            Boolean actualReturn = userData.Register(inputName, inputPassword, "RG", false, fullName);
            actualUserId = userData.UserID;
            Assert.AreEqual(expectedReturn, actualReturn);

            // Remove user after use of test
            expectedReturn = true;

            actualReturn = userData.deleteAccount();
            Assert.AreEqual(expectedReturn, actualReturn);
        }
        [TestMethod]
        public void TestMethod6()// Attempt to register the same username twice
        {
            // specify the value of test inputs
            inputName = "testUser14";
            inputPassword = "ts1234";
            fullName = "Im the Joker";
            // specify the value of expected outputs
            Boolean expectedReturn = true;
            // obtain the actual outputs by calling the method under testing
            Boolean actualReturn = userData.Register(inputName, inputPassword, "RG", false, fullName);
            actualUserId = userData.UserID;
            Assert.AreEqual(expectedReturn, actualReturn);

            // Attempt to enter the same user again

            inputName = "testUser14";
            inputPassword = "ts1234";
            fullName = "No Im the Joker";
            // specify the value of expected outputs
            expectedReturn = false;
            // obtain the actual outputs by calling the method under testing
            actualReturn = userData.Register(inputName, inputPassword, "RG", false, fullName);
            actualUserId = userData.UserID;
            Assert.AreEqual(expectedReturn, actualReturn);

            // Remove user after use of test
            expectedReturn = true;

            actualReturn = userData.deleteAccount();
            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void TestMethod7()  // Add and Remove ISBN from favorites
        {
             // Log in
             inputName = "wu154";
             inputPassword = "wu1234";
             // specify the value of expected outputs
             Boolean expectedReturn = true;
             // obtain the actual outputs by calling the method under testing
             Boolean actualReturn = userData.LogIn(inputName, inputPassword);
             Assert.AreEqual(expectedReturn, actualReturn);

            //Console.WriteLine(userData.isLogged()) ;
            actualReturn = userData.addFavorite("1617290890");
            expectedReturn = true;

            Assert.AreEqual(expectedReturn, actualReturn);


            actualReturn = userData.removeFavorite("1617290890");
            expectedReturn = true;

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        [TestMethod]
        public void TestMethod8()  // Attemp to remove ISBN which doesnt exis't
        {

            Boolean actualReturn = userData.removeFavorite("1617290892");
            Boolean expectedReturn = false;

            Assert.AreEqual(expectedReturn, actualReturn);

        }
        [TestMethod]
        public void TestMethod9()  // Attempt to change username (valid)
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                string username = "testing";
                Boolean expectedReturn = true;
                Boolean actualReturn = userData.changeUsername(username);
                Assert.AreEqual(expectedReturn, actualReturn);
                // change back
                userData.changeUsername("test");
            }

        }
        public void TestMethod10()  // Attempt to change username (empty)
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                string username = "";
                Boolean expectedReturn = false;
                Boolean actualReturn = userData.changeUsername(username);
                Assert.AreEqual(expectedReturn, actualReturn);
            }

        }
        [TestMethod]
        public void TestMethod11()  // Attempt to change password (valid)
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                string password = "test12345";
                Boolean expectedReturn = true;
                Boolean actualReturn = userData.changePassword(password);
                Assert.AreEqual(expectedReturn, actualReturn);
                // change back
                userData.changePassword("test1234");
            }
        }
        [TestMethod]
        public void TestMethod12()  // Attempt to change password (invalid)
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                string password = "te";
                Boolean expectedReturn = false;
                Boolean actualReturn = userData.changePassword(password);
                Assert.AreEqual(expectedReturn, actualReturn);
            }
        }
        [TestMethod]
        public void TestMethod13()  // Attempt to change password (invalid)
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                string password = "";
                Boolean expectedReturn = false;
                Boolean actualReturn = userData.changePassword(password);
                Assert.AreEqual(expectedReturn, actualReturn);
            }
        }
        [TestMethod]
        public void TestMethod14()  // Attempt to change full name (valid)
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                string fullName = "Testing Account The Third";
                Boolean expectedReturn = true;
                Boolean actualReturn = userData.changeFullName(fullName);
                Assert.AreEqual(expectedReturn, actualReturn);
                // change back
                userData.changeFullName("Test Account");
            }
        }
        [TestMethod]
        public void TestMethod15()  // Attempt to change full name (invalid)
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                string fullName = "";
                Boolean expectedReturn = false;
                Boolean actualReturn = userData.changeFullName(fullName);
                Assert.AreEqual(expectedReturn, actualReturn);
            }
        }
        [TestMethod]
        public void TestMethod16()  // Attempt to delete account
        {
            // use test account
            inputName = "test";
            inputPassword = "test1234";
            if (userData.LogIn(inputName, inputPassword))
            {
                Boolean expectedReturn = true;
                Boolean actualReturn = userData.deleteAccount();
                Assert.AreEqual(expectedReturn, actualReturn);
                // re-create test account
                userData.Register("test", "test1234", "RG", false, "Test Account");
            }
        }

        [TestMethod]
        public void TestMethod17()  // Add book to Wishlist
        {
            inputName = "dclark"; // user login
            inputPassword = "dc1234"; // user password
            Boolean expectedReturn = true; // expected results from test 
            Boolean actualReturn = userData.LogIn(inputName, inputPassword); // gather the actual results from using the LogIn method

            Assert.AreEqual(expectedReturn, actualReturn); // compare results

            userData.RemovefromWishlist("1554683831"); // ensure the book is removed from the Wishlist for the test
            expectedReturn = true; // expected results from test
            actualReturn = userData.AddtoWishlist("The Illegal", "1554683831", "20.99", "2015", "Lawrence Hill"); // gather the actual results from using the AddtoWishlist method

            Assert.AreEqual(expectedReturn, actualReturn); // compare results
        }

        
        [TestMethod]
        public void TestMethod18()  // Remove book from Wishlist 
        {

            inputName = "jsmith"; // user login
            inputPassword = "js1234"; // user password
            Boolean expectedReturn = true; // expected results from test 
            Boolean actualReturn = userData.LogIn(inputName, inputPassword); // gather the actual results from using the LogIn method

            userData.AddtoWishlist("love", "11x11", "10.10", "2000", "who"); // ensure the book is insdie the Wishlist for this test
            actualReturn = userData.RemovefromWishlist("11x11");
            expectedReturn = true;

            Assert.AreEqual(expectedReturn, actualReturn);

        }

        [TestMethod]
        public void TestMethod19()  // Add duplicate book to Wishlist
        {

            inputName = "dclark"; // user login
            inputPassword = "dc1234"; // user password
            Boolean expectedReturn = true; // expected results from test 
            Boolean actualReturn = userData.LogIn(inputName, inputPassword); // gather the actual results from using the LogIn method

            Assert.AreEqual(expectedReturn, actualReturn); // compare results

            userData.AddtoWishlist("Agile Software Development, Principles, Patterns, and Practices", "0135974445", "70.40", "2002", "Robert C.Martin"); // Make sure the book is inside the Wishlist for the test
            expectedReturn = false; // expected results from test
            actualReturn = userData.AddtoWishlist("Agile Software Development, Principles, Patterns, and Practices", "0135974445", "70.40", "2002", "Robert C.Martin"); // gather the actual results from using the AddtoWishlist method

            Assert.AreEqual(expectedReturn, actualReturn);
        }

        [TestMethod]
        public void TestMethod20()  // Remove a book that isn't present in the Wishlist
        {

            inputName = "dclark"; // user login
            inputPassword = "dc1234"; // user password
            Boolean expectedReturn = true; // expected results from test 
            Boolean actualReturn = userData.LogIn(inputName, inputPassword); // gather the actual results from using the LogIn method

            Assert.AreEqual(expectedReturn, actualReturn); // compare results

            userData.RemovefromWishlist("1554683831"); // ensure the book is removed from the Wishlist for the test
            expectedReturn = true; // expected results from test
            actualReturn = userData.RemovefromWishlist("1554683831"); // gather the actual results from using the RemovefromWishlist method

            Assert.AreEqual(expectedReturn, actualReturn);
        }


        [TestMethod]
        public void TopSellingBooksResult()
        {
            DataTable temp = new DALPurchaseHistory().ListTopSellingBooks();
            Assert.IsNotNull(temp);
        }
    }
}
