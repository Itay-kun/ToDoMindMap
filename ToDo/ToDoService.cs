using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MindOrgenizerToDo.Services
    {
        public class TodoService
        {
            private WebApiClient _apiClient;
            private string _baseUri;

            
            public TodoService(string baseUri)
            {
                _apiClient = UserSession.GetClient();
                _baseUri = baseUri;
            }
        public async Task<HttpResponseMessage> CreateTodo(JsonObject todoDataJson)
        {
            Console.WriteLine("Creating Todo");
            string url = $"{_baseUri}/api/todo";
            
            var response = await _apiClient.Post(url, todoDataJson);
            response.EnsureSuccessStatusCode();
            Console.WriteLine("Todo created successfully!");
            return response;
        }

        public async Task<HttpResponseMessage> UpdateTodo(string todoId, JsonObject todoDataJson)
        {
            Console.Write("ToDoService: Updating Todo: ",todoId);
            Console.WriteLine("\n\n\n");
            string url = $"{_baseUri}/api/todo/{todoId}";
            HttpResponseMessage response = await _apiClient.Put(url, todoDataJson);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteTodo(string todoId)
        {
            Console.WriteLine("ToDoService: Deleting Todo"); Console.Write(todoId);
            string url = $"{_baseUri}/api/todo/{todoId}";
            HttpResponseMessage response = await _apiClient.Delete(url);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Todo deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete Todo.");
            }
            return response;
        }

        public async Task<string> AssignTodo(string todoId, JsonObject assigneeJson)
        {
            Console.WriteLine("ToDoService: Assigning Todo");
            string url = $"{_baseUri}/api/todo/assign/{todoId}";
            var response = await _apiClient.Put(url, assigneeJson);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> GetTodoByAssignee(string assigneeId)
        {
            Console.WriteLine("ToDoService: Getting Todos by Assignee");
            string url = $"{_baseUri}/api/todo/assignee/{assigneeId}";
            var response = await _apiClient.Get(url);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> GetAllTodos()
            {
                string url = $"{_baseUri}/api/todo";
                var response = await _apiClient.Get(url);
                Console.WriteLine();
                Console.WriteLine("All todos: " + response);
                return response;
            }

        public async Task<HttpResponseMessage> GetOverdueTodos()
        {
            Console.WriteLine("ToDoService: Fetching Overdue Todos");
            string url = $"{_baseUri}/api/todo/overdue";
            var response = await _apiClient.Get(url);
            //response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> GetTodosForDate(string date)
        {
            Console.WriteLine("ToDoService: Fetching Todos for Date: " + date);
            string url = $"{_baseUri}/api/todo/due/{date}";
            var response = await _apiClient.Get(url);
            //response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> GetTodoById(string id)
        {
            Console.WriteLine("ToDoService: Fetching Todo by ID");
            string url = $"{_baseUri}/api/todo/{id}";
            var response = await _apiClient.Get(url);
            return response;
        }
    }
}