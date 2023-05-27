using LataPrzestepne.Data;
using LataPrzestepne.Interfaces;
using LataPrzestepne.Models;
using Microsoft.EntityFrameworkCore;

namespace LataPrzestepne.Services
{
    public class LeapYearsService : ILeapYearsService
    {
        private readonly ApplicationDbContext _context;

        public LeapYearsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void addRecord(LeapYears leapYears)
        {
            _context.LeapYears.Add(leapYears);
            _context.SaveChanges();
        }

        public IQueryable<LeapYears> getYears()
        {
            return _context.LeapYears.AsQueryable();
        }

        public DbSet<LeapYears> isEmpty()
        {
            return _context.LeapYears;
        }

        public async void deleteRecord(LeapYears leapYears)
        {
            _context.LeapYears.Remove(leapYears);
            _context.SaveChanges();
        }

        public async Task<LeapYears> getLeapYearsById(int? id)
        {
            return await _context.LeapYears.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<LeapYears> getRecordToDelete(int? id)
        {
            return await _context.LeapYears.FindAsync(id);
        }
    }
}
