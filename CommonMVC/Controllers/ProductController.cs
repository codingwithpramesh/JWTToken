﻿using CommonMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace CommonMVC.Controllers
{
    public class ProductController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7165/api/");
        private readonly HttpClient _httpClient;

        public ProductController()
        {
            _httpClient = new HttpClient() ;

            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string url =   baseAddress+ "user/Index";
            HttpResponseMessage response=  await _httpClient.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                string JsonResponse = await response.Content.ReadAsStringAsync();

                List<UserViewModel> users = JsonConvert.DeserializeObject<List<UserViewModel>>(JsonResponse);
                return View(users);
            }

            return View(new List<UserViewModel>());
        }


        [HttpGet]
        public IActionResult Create( )
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
            string url = baseAddress + "user";
            var content = new StringContent(JsonConvert.SerializeObject(userViewModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            string url = $"{baseAddress}user/Update/?id="+ id;
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(jsonResponse);
                return View(userViewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, UserViewModel userViewModel)
        {
            string url = $"{baseAddress}User/"+ id;
            var content = new StringContent(JsonConvert.SerializeObject(userViewModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        [HttpGet]
        [Route("Product/DeleteData")]
        public async Task<IActionResult> DeleteData(Guid id)
        {
            string url = baseAddress + "User/"+ id;
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(jsonResponse);
                return View(userViewModel);
            }

            
            return NotFound();
        }

       
            
        [HttpDelete("{id:Guid}"), ActionName("DeleteData")]
        [Route("Product/DeleteData")]
        public async Task<IActionResult> Delete(Guid id)
        {
            string url = $"{baseAddress}User/"+ id;
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Delete), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            string url = baseAddress + "user/" + id;
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(jsonResponse);
                return View(userViewModel);
            }

           
            return NotFound();
        }
    }
}
