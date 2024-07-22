using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MindOrgenizerToDo
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }  // Made public

        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("is_admin")]
        public bool isAdmin { get; }

        public UserModel(int id, string name, string email, string password, bool isAdmin = false)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            this.isAdmin = isAdmin;
        }

        public override string ToString()
        {
            Console.WriteLine("UserModel ToString called");
            return $"Id: {Id}, Name: {Name}, Email: {Email}, isAdmin: {isAdmin}";
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true  // Pretty print the JSON for better readability
            });
        }

        // Method to create a UserModel from a JSON string, ignoring case sensitivity
        public static UserModel FromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true  // Ignore case of JSON property names
            };
            return JsonSerializer.Deserialize<UserModel>(json, options);
        }
    }
}