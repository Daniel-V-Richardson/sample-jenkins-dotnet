using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace User.Pages.Shared
{
    public class NavModel : PageModel
    {
        public int? LoggedInUserId { get; private set; }
        public string? Username { get; private set; }
            

        public IActionResult OnGet()
        {
            int? loggedInUserId = HttpContext.Session.GetInt32("LoggedInUserId");

            if (loggedInUserId.HasValue)
            {
                using (MySqlConnection connection = DBConfig.GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Username FROM users WHERE Id = @UserId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", loggedInUserId.Value);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Username = reader.GetString("Username");
                            }
                        }
                    }
                }
            }
            else
            {
                return RedirectToPage("/Register");
            }

            return Page();
        }


        public IActionResult OnPost()
        {
            HttpContext.Session.Remove("LoggedInUserId");
            return RedirectToPage("/Loginpage");
        }
    }
}
