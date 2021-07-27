using Microsoft.AspNetCore.Mvc.Rendering;
using Network.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Core.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Birthday> birtdays, string birtday, string name)
        {
            birtdays.Insert(0, new Birthday { Relationship = "Все", Id = 0 });
            Birthdays = new SelectList(birtdays, "Relationship", "Relationship", birtday);
            SelectedBirthday = birtday;
            SelectedName = name;
        }
        public SelectList Birthdays { get; private set; }
        public string SelectedBirthday { get; private set; }  
        public string SelectedName { get; private set; }

    }
}

