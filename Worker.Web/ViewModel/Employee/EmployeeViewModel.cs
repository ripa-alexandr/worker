using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Worker.Web.ViewModel.Employee
{
    public class EmployeeViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Поле '{0}' должно быть установлено")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до {1} символов")]
        public string Name { get; set; }

        [DisplayName("Должность")]
        [Required(ErrorMessage = "Поле '{0}' должно быть установлено")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до {1} символов")]
        public string Position { get; set; }

        [DisplayName("Статус")]
        [Required(ErrorMessage = "Поле '{0}' должно быть установлено")]
        public int Status { get; set; }

        [DisplayName("Заработная плата")]
        [Required(ErrorMessage = "Поле '{0}' должно быть установлено")]
        [RegularExpression(@"^\d+(,{1}\d{2}$|$)", ErrorMessage = "Поле '{0}' должно быть числом формата 0 или 0,00")]
        [Range(0, 1000000, ErrorMessage = "Поле '{0}' должно быть от {1} до {2}")]
        public decimal Salary { get; set; }

        public IEnumerable<SelectListItem> AllStatuses { get; set; }

        public EmployeeFilter Filter { get; set; }
    }
}