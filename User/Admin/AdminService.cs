using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MindOrgenizerToDo.Services
{
    public class AdminService : UserService
    {
        public AdminService(string baseUri) : base(baseUri)
        {
        }

        public async Task<HttpResponseMessage> DeleteUser(long userId)
        {
            string url = $"{_baseUri}/api/user/{userId}";
            return await _apiClient.SendRequestWithToken(url, "DELETE");
        }


        public async Task<HttpResponseMessage> UpdateUserInfo(long userId, string email, string nickname, string password)
        {
            string url = $"{_baseUri}/api/user/update";

            Console.WriteLine("User Update from User Service");
            JsonObject jsonData = new JsonObject
            {
                ["id"] = userId, //maybe implement this as a function?
                ["email"] = email,
                ["nickname"] = nickname,
                ["password"] = password
            };

            return await _apiClient.SendRequestWithToken(url, "POST", jsonData);
        }
    }
}
