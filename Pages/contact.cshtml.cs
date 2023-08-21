using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace User.Pages
{
    public class contactModel : PageModel
    {

        [BindProperty]
        [Required]
       
        public string Subject { get; set; }

        [BindProperty]
        [Required]

        public string Description{ get; set; }

        [BindProperty]
        [Required]
        
        public string Email { get; set; }



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




                string insertQuery = "INSERT INTO feedback(Email, Subject, Description) " +
                                    "VALUES (@Email, @Subject, @Description)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Subject", Subject);
                    command.Parameters.AddWithValue("@Description", Description);
                    

                    command.ExecuteNonQuery();
                }
            }

            return RedirectToPage("/Home");
        }
    }
}