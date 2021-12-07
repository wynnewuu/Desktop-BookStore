using System;
using System.Diagnostics;

namespace BookStoreLIB
{
    public class UserData
    {
        public int UserID { set; get; }
        public string LoginName { set; get; }
        public string Password { set; get; }
        public string Type { set; get; }
        public Boolean isManager { set; get; }
        public string fullName { set; get; }

        public Boolean isLogged()
        {
            if (UserID > 0)
            {
                return true;
            }
            else return false;
        }

        public int getUserID() {
            return UserID;
        }
        public Boolean LogIn(string loginName, string passWord)
        {
            var dbUser = new DALUserInfo();
            UserID = dbUser.LogIn(loginName, passWord);
            if (UserID > 0)
            {
                LoginName = loginName;
                Password = passWord;
                isManager = dbUser.isManager(loginName, passWord);
                return true;
            }
            else 
                return false;
        }
        public Boolean Register(string LoginName, string Password, string Type, Boolean isManager, string fullName)
        {
            var dbUser = new DALUserInfo();
            if (dbUser.Register(LoginName, Password, Type, isManager, fullName) >= 0)
            {
                UserID = dbUser.LogIn(LoginName, Password);
                this.LoginName = LoginName;
                this.Password = Password;
                this.Type = Type;
                this.isManager = isManager;
                this.fullName = fullName;
                return true;
            }
            else return false;
        }
        public Boolean addFavorite(string isbn)
        {
            if (!this.isLogged())
            {
                return false;
            }
            else {
                var dbUser = new DALUserInfo();
                if (dbUser.addFavorite(UserID, isbn) >= 0)
                {
                    return true;
                }
                else return false;
            }
        }

        public bool removeFavorite(string isbn)
        {
            if (!this.isLogged())
            {
                return false;
            }
            else
            {
                var dbUser = new DALUserInfo();
                if (dbUser.removeFavorite(UserID, isbn) >= 0)
                {
                    return true;
                }
                else return false;
            }
        }

        public Boolean AddtoWishlist(string title, string isbn, string price, string year, string author)
        {
            if (this.isLogged())
            {
                var dbUser = new DALUserInfo();
                if (dbUser.AddtoWishlist(UserID, title, isbn, price, year, author) >= 0)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        public bool RemovefromWishlist(string isbn)
        {
            if (this.isLogged())
            {
                var dbUser = new DALUserInfo();
                if (dbUser.RemovefromWishlist(UserID, isbn) >= 0)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }
        public Boolean changeUsername(string username)
        {
            if (!this.isLogged())
            {
                return false;
            }
            else
            {
                var dbUser = new DALUserInfo();
                if (dbUser.changeUsername(UserID, username) >= 0)
                {
                    return true;
                }
                else return false;
            }
        }
        public Boolean changePassword(string password)
        {
            if (!this.isLogged())
            {
                return false;
            }
            else
            {
                var dbUser = new DALUserInfo();
                if (dbUser.changePassword(UserID, password) >= 0)
                {
                    return true;
                }
                else return false;
            }
        }
        public Boolean changeFullName(string fullName)
        {
            if (!this.isLogged())
            {
                return false;
            }
            else
            {
                var dbUser = new DALUserInfo();
                if (dbUser.changeFullName(UserID, fullName) >= 0)
                {
                    return true;
                }
                else return false;
            }
        }
        public Boolean deleteAccount()
        {
            if (!this.isLogged())
            {
                return false;
            }
            else
            {
                var dbUser = new DALUserInfo();
                if (dbUser.deleteAccount(UserID) >= 0)
                {
                    return true;
                }
                else return false;
            }
        }

        internal bool checkManager(string loginName, string passWord)
        {
            var dbUser = new DALUserInfo();
            UserID = dbUser.LogIn(loginName, passWord);
            if (UserID > 0)
            {
                LoginName = loginName;
                Password = passWord;
                isManager = true;
                return true;
            }
            else return false;
        }

        //internal bool checkManager(string loginName, string passWord)
        //{
        //    var dbUser = new DALUserInfo();
        //    UserID = dbUser.isManager(loginName, passWord);
        //    if (UserID > 0)
        //    {
        //        LoginName = loginName;
        //        Password = passWord;
        //        isManager = true;
        //        return true;
        //    }
        //    else return false;
        //}
    }
}