using CommonMVC.Models;
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
            _httpClient = new HttpClient();
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
            string url = baseAddress + "user/Create";
            var content = new StringContent(JsonConvert.SerializeObject(userViewModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(userViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string url = baseAddress + "user/Edit/" + id;
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
        public async Task<IActionResult> Edit(int id, UserViewModel userViewModel)
        {
            string url = baseAddress + "user/Edit/" + id;
            var content = new StringContent(JsonConvert.SerializeObject(userViewModel), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string url = baseAddress + "user/Delete/" + id;
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                UserViewModel userViewModel = JsonConvert.DeserializeObject<UserViewModel>(jsonResponse);
                return View(userViewModel);
            }

            
            return NotFound();
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string url = baseAddress + "user/Delete/" + id;
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

 
            return RedirectToAction(nameof(Delete), new { id = id });
        }
    }
}
