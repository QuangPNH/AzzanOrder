namespace AzzanOrder.ManagerOwner.Filters
{
    using AzzanOrder.ManagerOwner.Models;
    using AzzanOrder.ManagerOwner.Services;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Newtonsoft.Json;
    public class EmployeeActionFilter : IActionFilter
    {
        private readonly EmployeeService _employeeService;

        public EmployeeActionFilter(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                var emp = JsonConvert.DeserializeObject<Employee>(empJson);
                _employeeService.CurrentEmployee = emp;
                _employeeService.CurrentOwner = null;
                if (emp.EmployeeId == 0)
                {
                    var owner = JsonConvert.DeserializeObject<Owner>(empJson);
                    _employeeService.CurrentOwner = owner;
                    _employeeService.CurrentEmployee = null;
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
