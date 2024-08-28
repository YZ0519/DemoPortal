using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helper
{
    public static class ConstantHelper
    {
        public static class Url
        {
            //private const string SubDomain = "";//Debug
            public const string SubDomain = ""; //Publish
            public const string Home = SubDomain + "/";
            public const string Sales = SubDomain + "/Sales";
            public const string Expenses = SubDomain + "/Expenses";
            public const string Report = SubDomain + "/Report";
            public const string Profile = SubDomain + "/Profile";
            public const string About = SubDomain + "/Home/About";
            public const string Login = SubDomain + "/Auth";
            public const string SignOut = SubDomain + "/Auth/Logout";
        }
        public static class Auth
        {
            private const string Controller = "/api/v1/Auth/";
            public const string Login = Controller + "Login";
            public const string GetSecretKey = Controller + "GetSecretKey";

            public const string UserWebTokenCookie = "UserTokenCookie";
        }
		public static class Profile
		{
			private const string Controller = "/api/v1/Profile/";
			public const string UpdateUser = Controller + "UpdateUser";
			public const string CreateUser = Controller + "CreateUser";
			public const string GetUser = Controller + "GetUser";
			public const string ChangePassword = Controller + "ChangePassword";

			public const string RedirectChangePassword = "profile-change-password";
			public const string RedirectChangeProfile = "profile-edit";
		}
		public static class Token
        {
            private const string Controller = "/api/v1/Token/";
            public const string GenerateToken = Controller + "GenerateToken";
        }
        public static class Option
        {
            private const string Controller = "/api/v1/Option/";
            public const string GetOptionList = Controller + "GetOptionList";
        }
    }
}
