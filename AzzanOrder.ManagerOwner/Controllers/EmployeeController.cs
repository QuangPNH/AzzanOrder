using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Reflection;

namespace AzzanOrder.ManagerOwner.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly string _apiUrl = new Config()._apiUrl;
        public async Task<IActionResult> List(int? page)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }

            Employee emp = new Employee();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            List<Employee> employees = new List<Employee>();
            int totalEmployees = 0;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee");
                    string data = await res.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<Employee>>(data);
                    totalEmployees = employees.Count(e => e.IsDelete == false);
                }
                catch (HttpRequestException e)
                {
                    // Handle the exception here
                    ModelState.AddModelError(string.Empty, "Request error. Please contact administrator.");
                }
            }
            employees = employees.Where(e => e.ManagerId == emp.EmployeeId && e.IsDelete == false && !(e.Role.RoleName.ToLower() == "Magager".ToLower() || e.Role.RoleName.ToLower() == "Manager".ToLower())).ToList();

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
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
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


        [HttpPost]
        public async Task<IActionResult> Add(Employee employee)
        {


            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }

            //if (employeeImage != null && employeeImage.Length > 0)
            //             employee.Image = await ImageToBase64Async(employeeImage);

            Employee emp = new Employee();
            Role role = new Role();
            List<Role> roles = new List<Role>();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Role/{employee.RoleId}"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        role = JsonConvert.DeserializeObject<Role>(message);


                    }
                }
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Role"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        roles = JsonConvert.DeserializeObject<List<Role>>(message);

                        roles.RemoveAll(role => role.RoleName == "Magager" || role.RoleName == "Manager");

                    }
                }
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
                ManagerId = emp.EmployeeId,
                OwnerId = emp.OwnerId,
                IsDelete = false
            };


            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Phone/{employeeToSend.Phone}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["ErrorPhone"] = "Phone is already in use.";  
                    }
                }
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Gmail/{employeeToSend.Gmail}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["ErrorGmail"] = "Email is already in use.";
                        
                    }
                }

                if (TempData["ErrorPhone"] != null || TempData["ErrorGmail"] != null)
                {
                    return View(new Model() { employee = employee, roles = roles });
                }
                
                using (HttpResponseMessage res = await client.PostAsJsonAsync(_apiUrl + "Employee/Add/", employeeToSend))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();


                    }
                }
            }
            return RedirectToAction("List", "Employee");
        }


        public async Task<IActionResult> Update(int id)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
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
        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
          

            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }



            Employee emp = new Employee();
            
            List<Role> roles = new List<Role>();
            if (HttpContext.Request.Cookies.TryGetValue("LoginInfo", out string empJson))
            {
                emp = JsonConvert.DeserializeObject<Employee>(empJson);
            }


            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Role/{employee.RoleId}"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        role = JsonConvert.DeserializeObject<Role>(message);


                    }
                }
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Role"))
                {
                    using (HttpContent content = res.Content)
                    {
                        string message = await res.Content.ReadAsStringAsync();
                        roles = JsonConvert.DeserializeObject<List<Role>>(message);
                        roles.RemoveAll(role => role.RoleName == "Magager" || role.RoleName == "Manager");
                    }
                }
            }

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
                ManagerId = emp.EmployeeId,
                OwnerId = emp.OwnerId,
                IsDelete = false,
                Role = role
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
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Phone/{employeeToUpdate.Phone}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string mess = await res.Content.ReadAsStringAsync();
                        var a = JsonConvert.DeserializeObject<Employee>(mess);
                        if (a.EmployeeId != employeeToUpdate.EmployeeId)
                        {
                            TempData["ErrorPhone"] = "Phone is already in use.";
                        }
                    }
                }
                using (HttpResponseMessage res = await client.GetAsync(_apiUrl + $"Employee/Gmail/{employeeToUpdate.Gmail}"))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string mess = await res.Content.ReadAsStringAsync();
                        var a = JsonConvert.DeserializeObject<Employee>(mess);
                        if (a.EmployeeId != employeeToUpdate.EmployeeId)
                        {
                            TempData["ErrorGmail"] = "Email is already in use.";
                        }
                    }
                }

                if (TempData["ErrorPhone"] != null || TempData["ErrorGmail"] != null)
                {
                    return View(new Model() { employee = employee, roles = roles });
                }
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

            return RedirectToAction("List", "Employee");
        }


        /*public IActionResult Delete()
        {
            return RedirectToAction("List", "Employee");
        }*/



        public async Task<IActionResult> DeleteAsync(int id)
        {
            AuthorizeLogin authorizeLogin = new AuthorizeLogin(HttpContext);
            var loginStatus = await authorizeLogin.CheckLogin();
            if (loginStatus.Equals("owner"))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (loginStatus.Equals("manager"))
            {
            }
            else if (loginStatus.Equals("owner expired"))
            {
                ViewBag.Message = "Your subscription has expired. Please subscribe again.";
                return RedirectToAction("Login", "Home");
            }
            else if (loginStatus.Equals("manager expired"))
            {
                ViewBag.Message = "Your owner's subscription has expired for over a week.\nFor more instruction, please contact the owner.";
                return RedirectToAction("Login", "Home");
            }
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.DeleteAsync(_apiUrl + "Employee/Delete/" + id);
                string mess = await res.Content.ReadAsStringAsync();
                ViewBag.error = mess;

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
    }
}

