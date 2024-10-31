using AzzanOrder.Data.Models;
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

        public async Task<IActionResult> ListAsync()
        {
            List<Employee> employees = new List<Employee>();
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/");
                    if (res.IsSuccessStatusCode)
                    {
                        string data = await res.Content.ReadAsStringAsync();
                        employees = JsonConvert.DeserializeObject<List<Employee>>(data);
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

            var viewModel = new Model
            {
                employees = employees
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            return View();
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


        
        public async Task<IActionResult> Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.DeleteAsync(_apiUrl + "Employee/" + id);
                if (res.IsSuccessStatusCode)
                {
                    string mess = await res.Content.ReadAsStringAsync();
                    ViewBag.error = mess;
                    return RedirectToAction("List", "Employee");
                }
                else
                {
                    //// Handle the error response here
                    //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    //return RedirectToAction("List", "Employee");
                    return Conflict();
                }
            }
        }
    }
}

