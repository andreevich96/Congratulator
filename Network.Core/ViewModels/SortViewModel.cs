using Network.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Core.ViewModels
{
    public class SortViewModel
    {
        public SortState DateOfBirthSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState RelationshipSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            DateOfBirthSort = sortOrder == SortState.DateOfBirthAsc ? SortState.DateOfBirthDesc : SortState.DateOfBirthAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            RelationshipSort = sortOrder == SortState.RelationshipAsc ? SortState.RelationshipDesc : SortState.RelationshipAsc;
            Current = sortOrder;
        }
    }
}
