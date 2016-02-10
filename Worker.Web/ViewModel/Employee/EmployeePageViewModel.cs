using System.Collections.Generic;
using System.Web.Mvc;

namespace Worker.Web.ViewModel.Employee
{
    public class EmployeePageViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public IEnumerable<SelectListItem> AllStatuses { get; set; }

        public EmployeeFilter Filter { get; set; }
    }
}