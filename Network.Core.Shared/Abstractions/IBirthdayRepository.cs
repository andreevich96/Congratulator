using Network.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Core.Shared.Abstractions
{
   public interface IBirthdayRepository
    {
        IEnumerable<Birthday> GetBirthdays(int page, int pageSize);
        Birthday GetBirthdayById(int id);
    }
}
