using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindOrgenizerToDo
{
    public class UserSession
    {
        private static UserSession instance;

        private static WebApiClient _client = new WebApiClient();
        private WebApiClient client;

        public static string Token { get; set; }

        public static int id { get; set; }

        public string Email { get; private set; }
        

        public bool IsAdmin { get; set; } //Can be private?

        private UserSession(string email)
        {
            Email = email;
        }
        
        // Checks if the current user is an admin.
        public bool isUserAdmin()
        {
            return instance.IsAdmin;
        }

        public static UserSession GetInstance(string email)
        {
            if (instance == null)
            {
                instance = new UserSession(email);
            }
            return instance;
        }

        public void SetIsAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        // Gets the existing instance of the session
        public static UserSession GetInstance()
        {
            if (instance == null)
            {
                throw new Exception("User session is not initialized.");
            }
            return instance;
        }

        /*public WebApiClient GetClient()
        {
            return client;
        }*/

        public static WebApiClient GetClient()
        {
            return _client;
        }

        public void SetClient(WebApiClient webApiClient)
        {
            client = webApiClient;
        }

        public void SetToken(string token)
        {
            Token = token;
            Console.WriteLine("user token is: ");
            Console.Write(token);
        }

        public void SetUserID(int userId)
        {
            id = userId;
            Console.WriteLine("user id is: ");
            Console.Write(id);
        }

        public int getUserID()
        {
            return id;
        }

        public string GetToken() { 
            return Token; 
        }
    }

    /*
     * initialize it: UserSession session = UserSession.GetInstance("user@example.com");
     * access it: string email = UserSession.GetInstance().Email;
     */
}
