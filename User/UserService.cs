using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MindOrgenizerToDo.Services
{
    public class UserService
    {
        protected WebApiClient _apiClient;
        protected string _baseUri;

        public UserService(string baseUri)
        {
            _apiClient = UserSession.GetClient();  // Ensuring the WebApiClient is properly managed in a singleton pattern
            _baseUri = baseUri;
        }

        public async Task<HttpResponseMessage> RegisterUser(string email, string password, string nickname)
        {
            string url = $"{_baseUri}/api/user/signup";
            JsonObject jsonData = new JsonObject
            {
                ["email"] = email,
                ["password"] = password,
                ["nickname"] = nickname
            };

            return await _apiClient.SendRequestAsync(url, "POST", jsonData);
        }

        public async Task<HttpResponseMessage> LoginUser(string email, string password)
        {
            string url = $"{_baseUri}/api/user/login";
            JsonObject jsonData = new JsonObject
            {
                ["email"] = email,
                ["password"] = password
            };

            return await _apiClient.SendRequestAsync(url, "POST", jsonData);
        }

        public async Task<HttpResponseMessage> UpdateUserInfo(string email,string nickname, string password)
        {
            string url = $"{_baseUri}/api/user/update";
            
            Console.WriteLine("User Update from User Service");
            JsonObject jsonData = new JsonObject
            {
                ["id"] = UserSession.id, //maybe implement this as a function?
                ["email"] = email,
                ["nickname"] = nickname,
                ["password"] = password
            };

            //UserModel user = UserModel.FromJson(jsonData.ToString());

            return await _apiClient.SendRequestWithToken(url, "POST", jsonData);
        }

        public static string ExtractToken(string jsonString)
        {
            try
            {
                using (var document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("token", out var tokenElement))
                    {
                        return tokenElement.GetString();
                    }
                    else
                    {
                        Console.WriteLine("Token property not found in JSON");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON or extracting token: {ex.Message}");
                return null;
            }
        }

        public static int ExtractUserID(string jsonString)
        {
            Console.WriteLine("###################################| ExtractUserID |################################");
            Console.WriteLine(jsonString);
            try
            {
                using (var document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("userId", out var userIdElement))
                    {
                        Console.WriteLine($"{userIdElement.GetInt16()}");
                        return userIdElement.GetInt16();
                    }
                    else
                    {
                        Console.WriteLine("Token property not found in JSON");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON or extracting token: {ex.Message}");
                return 0;
            }
            finally
            { Console.WriteLine("#################################################################################"); }
        }

        public static bool ExtractIsAdmin(string jsonString)
        {
            Console.WriteLine("#####################################| ExtractIsAdmin |###############################");
            Console.WriteLine(jsonString);
            try
            {
                using (var document = JsonDocument.Parse(jsonString))
                {
                    JsonElement root = document.RootElement;
                    if (root.TryGetProperty("isAdmin", out var isAdminElement)) // Check for the exact property name as used in your JSON
                    {
                        Console.WriteLine($"{isAdminElement.GetBoolean()}");
                        return isAdminElement.GetBoolean();
                    }
                    else
                    {
                        Console.WriteLine("isAdmin property not found in JSON");
                        return false; // Assume not an admin if the property is not found
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON or extracting isAdmin: {ex.Message}");
                return false;
            }
            finally
            { Console.WriteLine("#################################################################################"); }
        }

        public async Task<HttpResponseMessage> GetPossibleAssigns()
        {
            string url = $"{_baseUri}/api/user/assigns";
            var response = await _apiClient.Get(url);
            Console.WriteLine();
            Console.WriteLine("All assignees: " + response);
            return response;
        }
    }
}