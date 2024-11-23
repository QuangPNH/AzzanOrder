using AzzanOrder.ManagerOwner.Models;

namespace AzzanOrder.ManagerOwner.Services
{
    public class EmployeeService
    {
		public Owner? CurrentOwner { get; set; }
		public Employee? CurrentEmployee { get; set; }
    }
}
