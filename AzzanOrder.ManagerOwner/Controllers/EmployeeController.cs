
using AzzanOrder.ManagerOwner.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;

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

            employees = employees.Where(e => e.IsDelete == false).ToList();
            int pageSize = 10;
            int pageNumber = page ?? 1;
            int maxPageNav = 10;
            int totalPages = (int)Math.Ceiling((double)totalEmployees / pageSize);

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

        public IActionResult Add()
        {
            return View();
        }



        public async Task<IActionResult> AddAction(Employee employee)
        {
            //https://localhost:7183/api/Employee/Update
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.PostAsJsonAsync(_apiUrl + "Employee/Add/", employee))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string message = await res.Content.ReadAsStringAsync();
                            Console.WriteLine(message);
                            return RedirectToAction("List", "Employee");
                        }
                    }
                }
            }
            return RedirectToAction("Add", "Employee");
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPost(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Add logic to save the employee
                return RedirectToAction("List");
            }
            return View(employee);
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
    }
}

