using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


namespace MindOrgenizerToDo
{
    public class WebApiClient
    {
        private static WebApiClient instance;
        private static readonly object lockObject = new object();
        private readonly HttpClient client;
        private CookieContainer cookieContainer;

        public WebApiClient()
        {
            client = new HttpClient(); // Initialize HttpClient instance in the constructor
            cookieContainer = new CookieContainer();
        }

        // Singleton access point
        public static WebApiClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new WebApiClient();
                        }
                    }
                }
                return instance;
            }
        }


        public async Task<HttpResponseMessage> SendRequestAsync(string URL, string method, object data = null, string contentType = "application/json")
        {
            Console.WriteLine("Without token: ");
            Console.WriteLine("################################################################################");
            Console.WriteLine(URL+" "+data);
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                if (data != null)
                {
                    string jsonData;
                    try
                    {
                        jsonData = JsonSerializer.Serialize(data); // Using System.Text.Json
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error serializing data to JSON: {e.Message}");
                        return null;
                    }

                    var content = new StringContent(jsonData, Encoding.UTF8, contentType);

                    try
                    {
                        var response = await client.SendAsync(new HttpRequestMessage(new HttpMethod(method), URL) { Content = content });
                        return response; // Return the entire response object
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Error sending request: {e.Message}");
                        return null;
                    }
                }
                else
                {
                    try
                    {
                        var response = await client.GetAsync(URL);
                        return response; // Return the entire response object
                    }
                    catch (HttpRequestException e)
                    {
                        Console.WriteLine($"Error sending request: {e.Message}");
                        return null;
                    }
                }
            }
        }

        public async Task<HttpResponseMessage> SendRequestWithToken(string URL, string method, JsonObject data = null, string contentType = "application/json")
        {
            Console.WriteLine("with token: ");
            Console.WriteLine("################################################################################");
            string jwtToken = UserSession.GetInstance().GetToken();
            string urlWithToken = URL + "/" + jwtToken;

          
            Console.WriteLine("\n\n\n");
            Console.WriteLine(method);
            Console.WriteLine(urlWithToken);

            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), urlWithToken);

            if (data != null)
            {
                string jsonData = JsonSerializer.Serialize(data); // Using System.Text.Json
                request.Content = new StringContent(jsonData, Encoding.UTF8, contentType);
            }

            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode(); // Ensure response is successful
                //Console.WriteLine(await response.Content.ReadAsStringAsync());
                return response; // Return the entire HttpResponseMessage object
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error sending request: {e.Message}");
                // Optionally, you might want to handle specific HTTP errors or rethrow for handling further up the call stack
                throw; // Rethrow the exception if you can't handle it here
            }
        }

        public async Task<HttpResponseMessage> Get(string URL)
        {
            string jwtToken = UserSession.GetInstance().GetToken();
            return await SendRequestWithToken(URL, "GET", data: null);
        }


        public async Task<HttpResponseMessage> Post(string URL, JsonObject postData)
        {
            string jwtToken = UserSession.GetInstance().GetToken();
           var response = await SendRequestWithToken(URL, "POST", postData);
            return response;
        }

        public async Task<HttpResponseMessage> Put(string URL, JsonObject putData)
        {
            string jwtToken = UserSession.GetInstance().GetToken();
            Console.WriteLine("Put: "+putData.ToString());
            return await SendRequestWithToken(URL, "PUT", data: putData);
        }

        public async Task<HttpResponseMessage> Delete(string URL)
        {
            string jwtToken = UserSession.GetInstance().GetToken();
            return await SendRequestWithToken(URL, "DELETE", data: null);
        }

    }
}