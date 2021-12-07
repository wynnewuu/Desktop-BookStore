using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookStoreLIB
{
    [TestClass]
    public class UnitTestUserManage
    {
        UserAdmin test1 = new UserAdmin(100,"tester100","password100","SG",false,"Tester");
        UserAdmin test2 = new UserAdmin(100);
        [TestMethod]
        public void TestConstructor1()
        {
            Assert.AreEqual(100, test1.userid);
            Assert.AreEqual("tester100", test1.username);
            Assert.AreEqual("password100", test1.password);
            Assert.AreEqual("SG", test1.type);
            Assert.AreEqual(false, test1.isManager);
            Assert.AreEqual("Tester", test1.fullname);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            Assert.AreEqual(100, test2.userid);
            
        }

        [TestMethod]
        public void TestAddUser()
        {
            Assert.AreEqual(1,test1.AdminAddUser());
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            
            Assert.AreEqual(1, test2.AdminDeleteUser());
        }

        [TestMethod]
        public void TestGetUserInfo()
        {


            test1.AdminAddUser();
            
            UserAdmin ua = test2.AdminGetUser();

            Assert.AreEqual(100, ua.userid);
            Assert.AreEqual("tester100", ua.username);
            Assert.AreEqual("password100", ua.password);
            Assert.AreEqual("SG", ua.type);
            Assert.AreEqual(false, ua.isManager);
            Assert.AreEqual("Tester", ua.fullname);

            test2.AdminDeleteUser();
        }

    }
}
