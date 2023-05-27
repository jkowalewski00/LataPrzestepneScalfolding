using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LataPrzestepne.Data;
using LataPrzestepne.Models;

namespace LataPrzestepne.Pages.Historia
{
    public class EditModel : PageModel
    {
        private readonly LataPrzestepne.Data.ApplicationDbContext _context;

        public EditModel(LataPrzestepne.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LeapYears LeapYears { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.LeapYears == null)
            {
                return NotFound();
            }

            var leapyears =  await _context.LeapYears.FirstOrDefaultAsync(m => m.Id == id);
            if (leapyears == null)
            {
                return NotFound();
            }
            LeapYears = leapyears;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LeapYears).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeapYearsExists(LeapYears.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LeapYearsExists(int id)
        {
          return (_context.LeapYears?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
