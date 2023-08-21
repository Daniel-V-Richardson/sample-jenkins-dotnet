using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace User.Pages
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        [Required]
        [RegularExpression(@"^[A-Za-z]{3,}$", ErrorMessage = "Username should have 3 or more letters and no symbols or numbers")]
        public string Username { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@".{6,}", ErrorMessage = "Password should be at least 6 characters long")]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}", ErrorMessage = "Email Should be in the mentioned format: 'example@domain.com'")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Phone Number should be of 10 Digits")]
        public string PhoneNumber { get; set; }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (MySqlConnection connection = DBConfig.GetConnection())
            {
                connection.Open();

                string insertQuery = "INSERT INTO users (Username, Password, Email, PhoneNumber) " +
                                    "VALUES (@Username, @Password, @Email, @PhoneNumber)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

                    command.ExecuteNonQuery();
                }
            }

          return RedirectToPage("/Loginpage");
        }
    }
}