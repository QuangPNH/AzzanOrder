﻿using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace payos.Models
{
    public class AuthorizeLogin
    {
        private readonly string _apiUrl = "https://localhost:7183/api/";
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

            try
            {
                _httpContext.Request.Cookies.TryGetValue("LoginInfo", out string loginInfoJson); // Use the instance field
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
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            HttpResponseMessage res = await client.GetAsync(_apiUrl + "Employee/Phone/" + emp.Phone);
                            if (res.IsSuccessStatusCode)
                            {
                                string data = await res.Content.ReadAsStringAsync();
                                emp = JsonConvert.DeserializeObject<Employee>(data);
                                if (emp.Phone != null)
                                {
                                    return "manager";
                                }
                            }
                            else { }
                        }
                        catch (HttpRequestException e) { }
                    }
                }
                else
                {
                    owner = JsonConvert.DeserializeObject<Owner>(user);
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            HttpResponseMessage res = await client.GetAsync(_apiUrl + "Owner/Phone/" + owner.Phone);
                            if (res.IsSuccessStatusCode)
                            {
                                string data = await res.Content.ReadAsStringAsync();
                                owner = JsonConvert.DeserializeObject<Owner>(data);
                                if (owner.Phone != null)
                                {
                                    return "owner";
                                }
                            }
                            else { }
                        }
                        catch (HttpRequestException e) { }
                    }
                };
            }
            return "null";
        }
    }
}
