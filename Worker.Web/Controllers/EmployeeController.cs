using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Worker.DTO.Entities;
using Worker.DTO.Handlers;
using Worker.DTO.Queries;
using Worker.Utilites.Extensions;
using Worker.Web.ViewModel.Employee;

namespace Worker.Web.Controllers
{
    /// <summary>
    /// The employee controller.
    /// </summary>
    public class EmployeeController : BaseController<EmployeeHandler>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeController"/> class.
        /// </summary>
        /// <param name="handler"></param>
        public EmployeeController(EmployeeHandler handler) 
            : base(handler)
        {
        }

        /// <summary>
        /// Return page with employees.
        /// </summary>
        public ActionResult Index(EmployeeFilter filter)
        {
            var employeeQuery = new EmployeeQuery
            {
                Skip = filter.CurrentPage.HasValue && filter.PageSize.HasValue ? filter.CurrentPage.Value * filter.PageSize.Value - filter.PageSize.Value : 0,
                Take = filter.PageSize ?? WebsiteConfig.PageSizes.First(),
                Status = filter.StatusFilter
            };

            var employeePageViewModel = InitializeEmployeePageViewModel(filter, employeeQuery);

            if (Request.IsAjaxRequest())
                return PartialView("EmployeeTable", employeePageViewModel);

            return View(employeePageViewModel);
        }

        /// <summary>
        /// Return page for employee details.
        /// </summary>
        public ActionResult Details(int id, EmployeeFilter filter)
        {
            var employeeDto = this.handler.Get(id);
            var newEmployeeViewModel = Mapper.Map<EmployeeDto, EmployeeViewModel>(employeeDto);
            newEmployeeViewModel.Filter = filter;

            return View(newEmployeeViewModel);
        }

        /// <summary>
        /// Return page for create employee.
        /// </summary>
        public ActionResult Create(EmployeeFilter filter)
        {
            var employeeViewModel = new EmployeeViewModel();

            InitializeEmployeeViewModel(employeeViewModel, filter);

            return View(employeeViewModel);
        }

        /// <summary>
        /// Create employee.
        /// </summary>
        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                InitializeEmployeeViewModel(employeeViewModel);

                return View(employeeViewModel);
            }

            var employeeDto = Mapper.Map<EmployeeViewModel, EmployeeDto>(employeeViewModel);
            this.handler.Create(employeeDto);
            employeeViewModel.Filter.CurrentPage = null;

            return RedirectToAction("Index", employeeViewModel.Filter);
        }

        /// <summary>
        /// Return page for edit employee.
        /// </summary>
        public ActionResult Edit(int id, EmployeeFilter filter)
        {
            var employeeDto = this.handler.Get(id);
            var employeeViewModel = Mapper.Map<EmployeeDto, EmployeeViewModel>(employeeDto);

            InitializeEmployeeViewModel(employeeViewModel, filter);

            return View(employeeViewModel);
        }

        /// <summary>
        /// Edit employee.
        /// </summary>
        [HttpPost]
        public ActionResult Edit(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                InitializeEmployeeViewModel(employeeViewModel);

                return View(employeeViewModel);
            }

            var employeeDto = Mapper.Map<EmployeeViewModel, EmployeeDto>(employeeViewModel);
            this.handler.Update(employeeDto);

            return RedirectToAction("Index", employeeViewModel.Filter);
        }

        /// <summary>
        /// Return page for delete employee.
        /// </summary>
        public ActionResult Delete(int id, EmployeeFilter filter)
        {
            var employeeDto = this.handler.Get(id);
            var employeeViewModel = Mapper.Map<EmployeeDto, EmployeeViewModel>(employeeDto);
            employeeViewModel.Filter = filter;

            return View(employeeViewModel);
        }

        /// <summary>
        /// Delete employee.
        /// </summary>
        [HttpPost]
        public ActionResult Delete(EmployeeViewModel employeeViewModel)
        {
            this.handler.Delete(employeeViewModel.Id.Value);
            
            return RedirectToAction("Index", employeeViewModel.Filter);
        }

        /// <summary>
        /// Generate report.
        /// </summary>
        public FileStreamResult GenerateReport()
        {
            var byteArray = this.handler.GenerateReport();
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/plain", "report.txt");
        }

        /// <summary>
        /// Initialize EmployeePageViewModel.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="employeeQuery"></param>
        /// <returns></returns>
        private EmployeePageViewModel InitializeEmployeePageViewModel(EmployeeFilter filter, EmployeeQuery employeeQuery)
        {
            var result = new EmployeePageViewModel
            {
                Employees = Mapper.Map<IEnumerable<EmployeeDto>, IEnumerable<EmployeeViewModel>>(this.handler.Get(employeeQuery)),
                AllStatuses = GetAllStatuses(),
                Filter = new EmployeeFilter
                {
                    CurrentPage = filter.CurrentPage ?? 1,
                    PageSize = filter.PageSize ?? WebsiteConfig.PageSizes.First(),
                    TotalItems = this.handler.Count(employeeQuery),
                    StatusFilter = filter.StatusFilter
                }
            };

            return result;
        }

        /// <summary>
        /// Update and initialize EmployeeViewModel.
        /// </summary>
        /// <param name="employeeViewModel"></param>
        /// <param name="filter"></param>
        private void InitializeEmployeeViewModel(EmployeeViewModel employeeViewModel, EmployeeFilter filter)
        {
            employeeViewModel.Filter = filter;
            InitializeEmployeeViewModel(employeeViewModel);
        }

        /// <summary>
        /// Update and initialize EmployeeViewModel.
        /// </summary>
        /// <param name="employeeViewModel"></param>
        private void InitializeEmployeeViewModel(EmployeeViewModel employeeViewModel)
        {
            employeeViewModel.AllStatuses = GetAllStatuses();
        }

        /// <summary>
        /// Get all statuses.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<SelectListItem> GetAllStatuses()
        {
            var result = Enum.GetValues(typeof(StatusViewModel))
                .Cast<StatusViewModel>()
                .Select(i => new SelectListItem { Text = i.ToDescription(), Value = i.ToString("d") });

            return result;
        }
    }
}