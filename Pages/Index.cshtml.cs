using LataPrzestepne.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LataPrzestepne.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LataPrzestepne.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ILeapYearsService _leapYearsService;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, ILeapYearsService leapYearsService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _leapYearsService = leapYearsService;
            _userManager = userManager;
        }

        [BindProperty]
        public LeapYears LeapYears { get; set; }

        
        public string Name { get; set; }

        public string AlertMessage { get; set; }

        

        public IActionResult OnPost(ClaimsPrincipal user)
        {

            if (LeapYears.BirthYear == 0)
            {
                ModelState.Remove("LeapYears.BirthYear");
                ModelState.AddModelError("LeapYears.BirthYear", "Pole Rok urodenia jest wymagane");
                return Page();
            }

            LeapYears.CheckYear();
            LeapYears.UserID = _userManager.GetUserId(User);
            LeapYears.UserName = _userManager.GetUserName(User);
            LeapYears.Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            _leapYearsService.addRecord(LeapYears);

            if (LeapYears.LeapYear)
                AlertMessage = $"{LeapYears.FirstName} urodził/a się w {LeapYears.BirthYear} roku. To był rok przestępny";

            else AlertMessage = $"{LeapYears.FirstName} urodził/a się w {LeapYears.BirthYear} roku. To nie był rok przestępny";

            return Page();
        }

        public void OnGet()
        {

        }
    }
}