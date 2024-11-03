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

        public async Task<IActionResult> ListAsync(int? page)
        {
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

            // Assuming totalEmployees is calculated before this point
            employees = employees.Where(e => e.IsDelete == false).ToList();

            int pageSize = 5;
            int pageNumber = page ?? 1; // Get the page number from query or default to 1
            int maxPageNav = 10; // Number of pagination buttons to show
            totalEmployees = employees.Count(); // Ensure you get the total employees count after filtering
            int totalPages = (int)Math.Ceiling((double)totalEmployees / pageSize);

            // Paginate the employees
            employees = employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();



            Console.WriteLine("sdfsfsffs" + totalPages);
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

            roles.RemoveAll(role => role.RoleName == "Magager");


            var viewModel = new Model
            {
                roles = roles
            };

            return View(viewModel);
        }



        public async Task<IActionResult> AddAction(Employee employee, IFormFile employeeImage)
        {
            //https://localhost:7183/api/Employee/Update

            //Console.WriteLine("HJGJhbjhfrhsbdjfbjhsdfhbsdjfh " + JsonConvert.SerializeObject(employee));


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
                        return RedirectToAction("List", "Employee");
                    }
                }
            }

            return RedirectToAction("Add", "Employee");
        }


        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePost(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Add logic to update the employee
                return RedirectToAction("List");
            }
            return View(employee);
        }

        /*public IActionResult Delete()
        {
            return RedirectToAction("List", "Employee");
        }*/



        public async Task<IActionResult> DeleteAsync(int id)
        {
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
    }

}

