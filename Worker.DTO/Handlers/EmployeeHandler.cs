using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worker.DAL.Api;
using Worker.DAL.Entities;
using Worker.DTO.Entities;
using Worker.DTO.Mapping;
using Worker.DTO.Queries;

namespace Worker.DTO.Handlers
{
    /// <summary>
    /// The employee handler.
    /// </summary>
    public class EmployeeHandler : BaseHandler<EmployeeDto, Employee>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public EmployeeHandler(IRepository repository) 
            : base(repository)
        {
        }

        /// <summary>
        /// Get filtered employees.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeDto> Get(EmployeeQuery query)
        {
            var entities = repository
                .Get<Employee>(i => query.Status == null || i.Status == (Status)query.Status.Value)
                .OrderBy(i => i.Id)
                .Skip(query.Skip)
                .Take(query.Take);

            return AppAutoMapper.Map<IEnumerable<EmployeeDto>>(entities);
        }

        /// <summary>
        /// Get report.
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateReport()
        {
            var entities = repository.Get<Employee>(i => i.Status == Status.Active)
                .AsEnumerable()
                .Select(i => new { i.Name, SalaryTax = i.Salary, Tax = CountTax(i.Salary), Salary = i.Salary - CountTax(i.Salary) })
                .ToList();

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(string.Format("{0,-50}{1,-25}{2,-25}{3}", "Имя", "Заработная плата", "Сумма налога на з/п", "Заработная плата с вычетом налога"));
            
            foreach (var entity in entities)
            {
                stringBuilder.AppendLine(string.Format("{0,-50}{1,-25:N}{2,-25:N}{3:N}", entity.Name, entity.SalaryTax, entity.Tax, entity.Salary));
            }

            stringBuilder.AppendLine(string.Format("{0,-50}{1,-25:N}{2,-25:N}{3:N}", "Сумма:", entities.Sum(i => i.SalaryTax), entities.Sum(i => i.Tax), entities.Sum(i => i.Salary)));

            return Encoding.Unicode.GetBytes(stringBuilder.ToString());
        }

        /// <summary>
        /// Count filtered employees.
        /// </summary>
        /// <returns>The <see cref="int"/></returns>
        public int Count(EmployeeQuery query)
        {
            return repository.Get<Employee>(i => query.Status == null || i.Status == (Status)query.Status.Value).Count();
        }

        /// <summary>
        /// Count tax.
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        private decimal CountTax(decimal salary)
        {
            if (salary < 10000)
                return salary * 10 / 100;

            if (salary >= 10000 && salary <= 25000)
                return salary * 15 / 100;

            return salary * 25 / 100;
        }
    }
}
