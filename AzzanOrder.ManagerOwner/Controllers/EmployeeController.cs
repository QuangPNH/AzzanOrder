using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace AzzanOrder.ManagerOwner.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly string _apiUrl = "https://localhost:7183/api/";
		public async Task<IActionResult> List(int? page)
		{
			if (!await CheckLogin())
			{
				return RedirectToAction("Login", "Home");
			}

			List<Employee> employees = new List<Employee>();
			int totalEmployees = 0;
			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/");
					if (res.IsSuccessStatusCode)
					{
						string data = await res.Content.ReadAsStringAsync();
						employees = JsonConvert.DeserializeObject<List<Employee>>(data);
						totalEmployees = employees.Count(e => e.IsDelete == false);
					}
					else
					{
						// Handle the error response here
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				catch (HttpRequestException e)
				{
					// Handle the exception here
					ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
				}
			}
			employees = employees.Where(e => e.IsDelete == false && !(e.Role.RoleName.ToLower() == "Magager".ToLower() || e.Role.RoleName.ToLower() == "Manager".ToLower())).ToList();
			int pageSize = 10;
			int pageNumber = page ?? 1;
			int maxPageNav = 10;
			totalEmployees = employees.Count();
			int totalPages = (int)Math.Ceiling((double)totalEmployees / pageSize);

			// Paginate the employees
			employees = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			var viewModel = new Model
			{
				anIntegerUsedForCountingNumberOfPageQueuedForTheList = totalPages,
				anIntegerUsedForKnowingWhatTheCurrentPageOfTheList = pageNumber,
				thisIntegerIsUsedForKnowingTheMaxNumberOfPageNavButtonShouldBeDisplayed = maxPageNav,
				employees = employees,
			};
			return View(viewModel);
		}

		public async Task<IActionResult> Add()
		{
			if (!await CheckLogin())
			{
				return RedirectToAction("Login", "Home");
			}
			List<Role> roles = new List<Role>();
			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage res = await client.GetAsync(_apiUrl + "Role");
					if (res.IsSuccessStatusCode)
					{
						string data = await res.Content.ReadAsStringAsync();
						roles = JsonConvert.DeserializeObject<List<Role>>(data);
					}
					else
					{
						// Handle the error response here
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				catch (HttpRequestException e)
				{
					// Handle the exception here
					ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
				}
			}
			roles.RemoveAll(role => role.RoleName.ToLower() == "Magager".ToLower() || role.RoleName.ToLower() == "Manager".ToLower());

			var viewModel = new Model
			{
				roles = roles
			};

			return View(viewModel);
		}



		public async Task<IActionResult> AddAction(Employee employee, IFormFile employeeImage)
		{
			if (!await CheckLogin())
			{
				return RedirectToAction("Login", "Home");
			}

			if (employeeImage != null && employeeImage.Length > 0)
				employee.Image = await ImageToBase64Async(employeeImage);

			//employee.EmployeeId;

			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/1");
					if (res.IsSuccessStatusCode)
					{
						string data = await res.Content.ReadAsStringAsync();
						employee.Manager = JsonConvert.DeserializeObject<Employee>(data);
					}
					else
					{
						// Handle the error response here
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				catch (HttpRequestException e)
				{
					// Handle the exception here
					ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
				}
			}

			if (ModelState.IsValid)
			{
			}

			var employeeToSend = new
			{
				EmployeeName = employee.EmployeeName,
				Gender = employee.Gender,
				Phone = employee.Phone,
				Gmail = employee.Gmail,
				BirthDate = employee.BirthDate,
				RoleId = employee.RoleId,
				HomeAddress = employee.HomeAddress,
				WorkAddress = employee.WorkAddress,
				Image = employee.Image,
				ManagerId = employee.ManagerId,
				OwerId = employee.OwerId,
				IsDelete = false
			};

			using (HttpClient client = new HttpClient())
			{
				using (HttpResponseMessage res = await client.PostAsJsonAsync(_apiUrl + "Employee/Add/", employeeToSend))
				{
					using (HttpContent content = res.Content)
					{
						string message = await res.Content.ReadAsStringAsync();
						Console.WriteLine(message);

					}
				}
			}
			return RedirectToAction("List", "Employee");
		}


		public async Task<IActionResult> Update(int id)
		{
			if (!await CheckLogin())
			{
				return RedirectToAction("Login", "Home");
			}
			Employee employee = new Employee();
			List<Role> roles = new List<Role>();

			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/" + id);
					if (res.IsSuccessStatusCode)
					{
						string data = await res.Content.ReadAsStringAsync();
						employee = JsonConvert.DeserializeObject<Employee>(data);
					}
					else
					{
						// Handle the error response here
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				catch (HttpRequestException e)
				{
					// Handle the exception here
					ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
				}
			}

			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage res = await client.GetAsync(_apiUrl + "Role");
					if (res.IsSuccessStatusCode)
					{
						string data = await res.Content.ReadAsStringAsync();
						roles = JsonConvert.DeserializeObject<List<Role>>(data);
					}
					else
					{
						// Handle the error response here
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				catch (HttpRequestException e)
				{
					// Handle the exception here
					ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
				}
			}
			roles.RemoveAll(role => role.RoleName == "Magager" || role.RoleName == "Manager");


			var viewModel = new Model
			{
				roles = roles,
				employee = employee
			};

			return View(viewModel);
		}

		public async Task<IActionResult> UpdateAction(Employee employee, IFormFile employeeImage)
		{
			if (!await CheckLogin())
			{
				return RedirectToAction("Login", "Home");
			}
			// Convert uploaded image to Base64 if there is a new image
			if (employeeImage != null && employeeImage.Length > 0)
				employee.Image = await ImageToBase64Async(employeeImage);

			// Fetch the manager information if needed
			using (HttpClient client = new HttpClient())
			{
				try
				{
					HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/1");
					if (res.IsSuccessStatusCode)
					{
						string data = await res.Content.ReadAsStringAsync();
						employee.Manager = JsonConvert.DeserializeObject<Employee>(data);
					}
					else
					{
						// Handle error response
						ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
					}
				}
				catch (HttpRequestException e)
				{
					// Handle exception
					ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
				}
			}

			if (ModelState.IsValid)
			{
				// Conditionally include Image property only if it's not null
				var employeeToUpdate = new
				{
					EmployeeId = employee.EmployeeId,
					EmployeeName = employee.EmployeeName,
					Gender = employee.Gender,
					Phone = employee.Phone,
					Gmail = employee.Gmail,
					BirthDate = employee.BirthDate,
					RoleId = employee.RoleId,
					HomeAddress = employee.HomeAddress,
					WorkAddress = employee.WorkAddress,
					ManagerId = employee.ManagerId,
					OwerId = employee.OwerId,
					IsDelete = employee.IsDelete
				};

				// Serialize to JSON and conditionally include "Image" if not null
				var employeeJson = JsonConvert.SerializeObject(employeeToUpdate, new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore
				});

				if (employee.Image != null)
				{
					// Add "Image" property manually if it's not null
					employeeJson = employeeJson.TrimEnd('}');
					employeeJson += $",\"Image\":\"{employee.Image}\"}}";
				}

				using (HttpClient client = new HttpClient())
				{
					try
					{
						// Send update request with modified JSON content
						var content = new StringContent(employeeJson, System.Text.Encoding.UTF8, "application/json");
						HttpResponseMessage res = await client.PutAsync(_apiUrl + "Employee/Update", content);

						if (res.IsSuccessStatusCode)
						{
							string message = await res.Content.ReadAsStringAsync();
							Console.WriteLine(message);
							return RedirectToAction("List", "Employee");
						}
						else
						{
							ModelState.AddModelError(string.Empty, "Update failed. Please try again.");
						}
					}
					catch (HttpRequestException e)
					{
						ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
					}
				}
			}
			return RedirectToAction("List", "Employee");
		}


		/*public IActionResult Delete()
        {
            return RedirectToAction("List", "Employee");
        }*/



		public async Task<IActionResult> DeleteAsync(int id)
		{
			if (!await CheckLogin())
			{
				return RedirectToAction("Login", "Home");
			}
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage res = await client.DeleteAsync(_apiUrl + "Employee/Delete/" + id);
				string mess = await res.Content.ReadAsStringAsync();
				ViewBag.error = mess;
				Console.WriteLine("jhhdghjhgjahgdjhas " + mess);
				return RedirectToAction("List", "Employee");
			}
		}

		private async Task<string> ImageToBase64Async(IFormFile imageFile)
		{
			using (var memoryStream = new MemoryStream())
			{
				await imageFile.CopyToAsync(memoryStream);
				byte[] imageBytes = memoryStream.ToArray();
				string base64String = Convert.ToBase64String(imageBytes);

				// Determine the image type from the ContentType of the IFormFile
				string imageFormat = imageFile.ContentType.Split('/')[1]; // e.g., "jpeg" or "png"

				// Add the data URI prefix
				return $"data:image/{imageFormat};base64,{base64String}";
			}
		}

		/*private async Task<string> ImageToBase64Async(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }*/

		/*private string ImageToBase64(string imagePath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return Convert.ToBase64String(imageBytes);
        }*/



		private async Task<bool> CheckLogin()
		{
			Employee emp = new Employee();
			if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
			{
				Console.WriteLine("sfdjhsbfjs " + empJson);
				emp = JsonConvert.DeserializeObject<Employee>(empJson);
				using (HttpClient client = new HttpClient())
				{
					try
					{
						HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/Phone/" + emp.Phone);
						if (res.IsSuccessStatusCode)
						{
							string data = await res.Content.ReadAsStringAsync();
							emp = JsonConvert.DeserializeObject<Employee>(data);
						}
						else
						{
							// Handle the error response here
							ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
						}
					}
					catch (HttpRequestException e)
					{
						// Handle the exception here
						ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
					}
				}
			}
			if (emp.Phone != null)
			{
				return true;
			}
			return false;
		}
	}
}

