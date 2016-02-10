using System.ComponentModel;

namespace Worker.Web.ViewModel.Employee
{
    public enum StatusViewModel
    {
        [Description("Активен")]
        Active,

        [Description("Неактивен")]
        NotActive
    }
}