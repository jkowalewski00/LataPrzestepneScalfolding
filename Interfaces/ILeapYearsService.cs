using LataPrzestepne.Models;
using Microsoft.EntityFrameworkCore;

namespace LataPrzestepne.Interfaces
{
    public interface ILeapYearsService
    {
        public void addRecord(LeapYears leapYears);

        public IQueryable<LeapYears> getYears();

        public DbSet<LeapYears> isEmpty();

        public void deleteRecord(LeapYears leapYears);

        public Task<LeapYears> getLeapYearsById(int? id);

        public Task<LeapYears> getRecordToDelete(int? id);
    }
}
