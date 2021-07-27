using Network.Core.Shared.Abstractions;
using Network.Core.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Network.Infrastructure.DataAcces
{
    public class BirthdayRepository : IBirthdayRepository
    {
        private readonly ApplicationDbContext _context;
        public BirthdayRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Birthday GetBirthdayById(int id)
        {
            return _context.Birthdays
                .FirstOrDefault(Birthday => Birthday.Id == id);
        }

        public IEnumerable<Birthday> GetBirthdays(int page, int pageSize)
        {
            return _context.Birthdays
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
