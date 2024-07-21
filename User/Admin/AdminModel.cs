using MindOrgenizerToDo;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MindOrganizerToDo
{
    public class AdminModel : UserModel
    {
        [JsonPropertyName("admin_level")]
        public int AdminLevel { get; set; }

        // Additional properties specific to administrators can be added here

        public AdminModel(int id, string name, string email, string password, int adminLevel)
            : base(id, name, email, password, true) // Ensure isAdmin is always true for admins
        {
            AdminLevel = adminLevel;
        }

        public override string ToString()
        {
            // Include admin-specific details in the ToString method
            return base.ToString() + $", AdminLevel: {AdminLevel}";
        }

        public string ToJson()
        {
            // Serialize this AdminModel instance to JSON
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        public static new AdminModel FromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<AdminModel>(json, options);
        }
    }
}