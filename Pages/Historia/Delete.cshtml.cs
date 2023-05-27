using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LataPrzestepne.Data;
using LataPrzestepne.Models;

namespace LataPrzestepne.Pages.Historia
{
    public class DeleteModel : PageModel
    {
        private readonly LataPrzestepne.Interfaces.ILeapYearsService _leapYearsService;

        public DeleteModel(LataPrzestepne.Interfaces.ILeapYearsService leapYearsService)
        {
           _leapYearsService = leapYearsService;
        }

        [BindProperty]
      public LeapYears LeapYears { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _leapYearsService.isEmpty() == null)
            {
                return NotFound();
            }

            var leapyears = _leapYearsService.getLeapYearsById(id);

            if (leapyears == null)
            {
                return NotFound();
            }
            else 
            {
                LeapYears = (LeapYears)await leapyears;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _leapYearsService.isEmpty() == null)
            {
                return NotFound();
            }
            var leapyears = _leapYearsService.getRecordToDelete(id);

            if (leapyears != null)
            {
                LeapYears = (LeapYears) await leapyears;
                _leapYearsService.deleteRecord(LeapYears);
            }

            return RedirectToPage("./Index");
        }
    }
}
