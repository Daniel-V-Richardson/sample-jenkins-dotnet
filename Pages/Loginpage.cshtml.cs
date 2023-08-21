using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace User.Pages
{
    public class LoginpageModel : PageModel
    {
        [BindProperty]
        [Required]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "Email Should be in the mentioned format: 'example@domain.com'")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@".{6,}", ErrorMessage = "Password should be at least 6 characters long")]
        public string Password { get; set; }

        public IActionResult OnPost(string Email, string Password)
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                return Page();
            }

            using (MySqlConnection connection = DBConfig.GetConnection())
            {
                connection.Open();

                string selectQuery = "SELECT Id FROM users WHERE Email = @Email AND Password = @Password";

                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32("Id");

                            // Store UserId in session storage
                            HttpContext.Session.SetInt32("LoggedInUserId", userId);

                            // Redirect to Home page
                            return RedirectToPage("/Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid login credentials");
                            return RedirectToPage("/Register");
                        }
                    }
                }
            }
        }
    }
}
