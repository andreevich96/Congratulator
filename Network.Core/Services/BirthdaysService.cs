using Network.Core.Shared.Abstractions;
using Network.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Core.Services
{
    public class BirthdaysService
    {
        private readonly IBirthdayRepository _birthdayRepository;

        public BirthdaysService(IBirthdayRepository birthdayRepository)
        {
            _birthdayRepository = birthdayRepository;
        }

        public IEnumerable<BirthdayViewModel> GetBirthdays()
        {
            return _birthdayRepository.GetBirthdays(page: 1, pageSize: 10)
                .Select(birthday => new BirthdayViewModel
                {
                    Id = birthday.Id,
                    Name = birthday.Name,
                    Relationship = birthday.Relationship,
                    DateOfBirth = birthday.DateOfBirth,
                    Note = birthday.Note,
                });
        }

        public BirthdayViewModel GetBirthdayById(int id)
        {
            var birthday = _birthdayRepository.GetBirthdayById(id);

            if (birthday == null)
            {
                return null;
            }

            return new BirthdayViewModel
            {
                Id = birthday.Id,
                Name = birthday.Name,
                Relationship = birthday.Relationship,
                DateOfBirth = birthday.DateOfBirth,
                Note = birthday.Note,
            };
        }
    }
}
