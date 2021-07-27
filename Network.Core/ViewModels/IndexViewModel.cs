using Network.Core.Shared.Entities;
using System.Collections.Generic;

namespace Network.Core.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Birthday> Birthdays { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
