using ExpenseTracker.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExpenseTracker.Web.Controllers
{
   public class CategoriesController : Controller
   {
      #region Index
      [HttpGet]
      public async Task<IActionResult> Index()
      {
         var catagory = await GetAll();
         //if (catagory == null)
         //    return RedirectToAction("Index");
         return View(catagory);
      }

      private async Task<CategoryDto?> GetAll()
      {
         using var client = new HttpClient();
         var response = await client.GetAsync($"http://localhost:5000/Categories/LoadCategory");
         if (!response.IsSuccessStatusCode)
            return null;
         string result = await response.Content.ReadAsStringAsync();
         var category = JsonConvert.DeserializeObject<CategoryDto>(result);
         return category;
      }
      #endregion
   }
}
