using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace AzzanOrder.ManagerOwner.Models
{
	public class AuthorizeLogin
	{
		private readonly string _apiUrl = new Config()._apiUrl;
		private readonly HttpContext _httpContext; // Add this field

		public AuthorizeLogin(HttpContext httpContext) // Modify constructor to accept HttpContext
		{
			_httpContext = httpContext; // Initialize the field
		}

		public async Task<string> CheckLogin()
		{
			Employee emp = new Employee();
			Owner owner = new Owner();
			bool isManager = false;
			bool idNull = false;

			try
			{
				_httpContext.Request.Cookies.TryGetValue("LoginInfo", out string loginInfoJson);

				var loginInfo = JsonConvert.DeserializeObject<Employee>(loginInfoJson);
				if (loginInfo.RoleId != null)
				{
					isManager = true;
				}
				else
				{
					isManager = false;
				}
			}
			catch { }

			if (_httpContext.Request.Cookies.TryGetValue("LoginInfo", out string user)) // Use the instance field
			{
				if (isManager)
				{
					emp = JsonConvert.DeserializeObject<Employee>(user);
					if (emp == null)
					{
						return "null";
					}
					using (HttpClient client = new HttpClient())
					{
						try
						{
                            HttpResponseMessage res = await client.GetAsync(_apiUrl + "/Employee/FirstEmployee/" + emp.OwnerId);
                            if (res.IsSuccessStatusCode)
                            {
                                string data = await res.Content.ReadAsStringAsync();
                                Employee emp1 = JsonConvert.DeserializeObject<Employee>(data);
                                if (emp1.Phone != null)
                                {
                                    return "first manager";
                                }
                            }
						}
						catch (HttpRequestException e) { }
					}
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/Manager/Phone/" + emp.Phone);
                            if (res.IsSuccessStatusCode)
                            {
                                string data = await res.Content.ReadAsStringAsync();
                                emp = JsonConvert.DeserializeObject<Employee>(data);
                                if (emp.Phone != null)
                                {
                                    return "manager";
                                }
                                if (emp.Owner.SubscribeEndDate < DateTime.Now.AddDays(7))
                                {
                                    return "manager expired";
                                }
                            }
                            else { return "null"; }
                        }
                        catch (HttpRequestException e) { }
                    }
                }
				else
				{
					owner = JsonConvert.DeserializeObject<Owner>(user);
					if (owner.OwnerId == 0)
					{
						idNull = true;
					}
					if (owner == null)
					{
						return "null";
					}
					using (HttpClient client = new HttpClient())
					{
						try
						{
							HttpResponseMessage res = await client.GetAsync(_apiUrl + "Owner/Phone/" + owner.Phone);
							if (res.IsSuccessStatusCode)
							{
								string data = await res.Content.ReadAsStringAsync();

                                owner = JsonConvert.DeserializeObject<Owner>(data);

								if (idNull)
								{
									_httpContext.Response.Cookies.Append("LoginInfo", data, new CookieOptions
									{
										Expires = DateTimeOffset.UtcNow.AddDays(30)
									});
								}
								if (owner.SubscribeEndDate < DateTime.Now.AddDays(7))
								{
									return "owner expired";
								}
								if (owner.Phone != null)
								{
									return "owner";
								}
							}
							else { return "null"; }
						}
						catch (HttpRequestException e) { }
					}
				};
			}
			return "null";
		}
	}
}
