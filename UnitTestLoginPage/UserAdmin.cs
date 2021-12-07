using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLIB
{
    public class UserAdmin

    {

        public int userid { set; get; }
        public string username { set; get; }
        public string password { set; get; }
        public string type { set; get; }
        public Boolean isManager { set; get; }
        public string fullname { set; get; }
        private DALUserManage dbUser = new DALUserManage();



        /// <summary>
        /// Constructor for adding users
        /// </summary>
        
        public UserAdmin(int userid, String username, String password,String type,Boolean isManager,String fullname)
        {
            this.userid = userid;
            this.username = username;
            this.password = password;
            this.type = type;
            this.isManager = isManager;
            this.fullname = fullname;
        }


        /// <summary>
        /// Constructor for deleting or getting users
        /// </summary>
        /// <param name="isbn"></param>
        public UserAdmin(int userid)
        {
            this.userid = userid;

        }

        /// <summary>
        /// call AddUserIntoDataBase method from DALBookManage class
        /// </summary>
        /// <returns></returns>
        public int AdminAddUser()
        {

            return dbUser.AddUserIntoDataBase(this.userid, this.username, this.password, this.type, this.isManager, this.fullname);

        }


        /// <summary>
        /// call DeleteUserFromDataBase method from DALBookManage class
        /// </summary>
        /// <returns></returns>
        public int AdminDeleteUser()
        {

            return dbUser.DeleteUserFromDataBase(this.userid);
        }

        public UserAdmin AdminGetUser()
        {
            return dbUser.GetUserFromDataBase(this.userid);
        }

    }
}
