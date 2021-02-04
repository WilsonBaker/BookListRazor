using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazor.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //ModelState checks if required fields are filled in such as Name in this case.
            if(ModelState.IsValid)
            {
                //Add Book to async queue
                await _db.Book.AddAsync(Book);
                //Update database using async queue
                await _db.SaveChangesAsync();
                //Return to BookList page after successfull creation
                return RedirectToPage("Index");
            }
            else
            {
                //If failed to validate ModelState then stay on same page
                return Page();
            }
        }
    }
}
