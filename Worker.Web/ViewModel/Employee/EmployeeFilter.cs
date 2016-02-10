using System.ComponentModel;

namespace Worker.Web.ViewModel.Employee
{
    public class EmployeeFilter : PageInfo
    {
        [DisplayName("Статус")]
        public int? StatusFilter { get; set; }
    }
}