namespace AzzanOrder.ManagerOwner.Filters
{
    using AzzanOrder.ManagerOwner.Models;
    using AzzanOrder.ManagerOwner.Services;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Newtonsoft.Json;
    public class EmployeeActionFilter : IActionFilter
    {
        private readonly EmployeeService _employeeService;
		private readonly string _apiUrl = new Config()._apiUrl;
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

					// Check if subscription is less than 7 days away from expiring
					if ((owner.SubscribeEndDate - DateTime.Now).TotalDays < 7)
					{
						// Check if the notification was already sent within the last 24 hours
						if (!context.HttpContext.Request.Cookies.TryGetValue("LastNotificationSent", out string lastNotificationSent) ||
							DateTime.Parse(lastNotificationSent) < DateTime.Now.AddHours(-24))
						{
							// Call the API to update the notification
							using (HttpClient client = new HttpClient())
							{
								var notification = new
								{
									OwnerId = owner.OwnerId,
									Content = $"Your subscription will expire in {(owner.SubscribeEndDate - DateTime.Now).TotalDays:F0} days."
								};
								var content = new StringContent(JsonConvert.SerializeObject(notification), System.Text.Encoding.UTF8, "application/json");
								client.PostAsync(_apiUrl + "Notification/Update", content).Wait();
							}

							// Set a cookie to lock this branch from running for 24 hours
							context.HttpContext.Response.Cookies.Append("LastNotificationSent", DateTime.Now.ToString(), new CookieOptions
							{
								Expires = DateTimeOffset.UtcNow.AddHours(24)
							});
						}
					}
					// Fetch notifications for the owner
					using (HttpClient client = new HttpClient())
					{
						var response = client.GetAsync(_apiUrl + $"Notification/GetByOwnerId/{owner.OwnerId}").Result;
						if (response.IsSuccessStatusCode)
						{
							var notificationsJson = response.Content.ReadAsStringAsync().Result;
							var notifications = JsonConvert.DeserializeObject<List<Notification>>(notificationsJson);
							_employeeService.CurrentOwnerNotifications = notifications.Where(n => n.IsRead != true).ToList();
						}
					}
				}
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
