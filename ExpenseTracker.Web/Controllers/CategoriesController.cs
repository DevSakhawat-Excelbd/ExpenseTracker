//using ExpenseTracker.Domain.Dto;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Text;

//namespace ExpenseTracker.Web.Controllers
//{
//   public class CategoriesController : Controller
//   {
//      #region Index
//      [HttpGet]
//      public async Task<IActionResult> Index()
//      {
//         var catagory = await GetAll();
//         //if (catagory == null)
//         //    return RedirectToAction("Index");
//         return View(catagory);
//      }

//      private async Task<List<CategoryDto?>> GetAll()
//      {
//         using var client = new HttpClient();
//         var response = await client.GetAsync($"http://localhost:5120/api/Categories/LoadCategory");
//         if (!response.IsSuccessStatusCode)
//            return new List<CategoryDto>();
//         string result = await response.Content.ReadAsStringAsync();
//         var category = JsonConvert.DeserializeObject<List<CategoryDto>>(result);
//         return category ?? new List<CategoryDto>();
//      }
//      #endregion Index


//      #region Create
//      [HttpGet]
//      public IActionResult Create()
//      {
//         var category = new CategoryDto();

//         return View(category);
//      }
//      [HttpPost]
//      [ValidateAntiForgeryToken]
//      public async Task<IActionResult> Create(CategoryDto model)
//      {
//         CategoryDto category = new CategoryDto
//         {
//            CategoryId = model.CategoryId,
//            CategoryName = model.CategoryName,
//         };
//         var CreateCategoryAdded = await CreateCategory(category);

//         if (CreateCategoryAdded == null)
//         {
//            return View(category);
//         }
//         return RedirectToAction("Index", "Categories");
//      }
//      private async Task<CategoryDto> CreateCategory(CategoryDto categories)
//      {
//         var data = JsonConvert.SerializeObject(categories);
//         using var client = new HttpClient();
//         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
//         var response = await client.PostAsync($"http://localhost:5120/api/Categories/AddCategory", httpContent);

//         if (!response.IsSuccessStatusCode)
//         {
//            return null;
//         }

//         string result = await response.Content.ReadAsStringAsync();
//         return JsonConvert.DeserializeObject<CategoryDto>(result);
//      }
//      #endregion
//   }
//}



using ExpenseTracker.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ExpenseTracker.Web.Controllers
{
   public class CategoriesController : Controller
   {
      #region start-index
      [HttpGet]
      public async Task<IActionResult> Index()
      {
         var catagory = await GetAllCategory();
         return View(catagory);
      }

      private async Task<List<CategoryDto?>> GetAllCategory()
      {
         using var client = new HttpClient();
         var response = await client.GetAsync($"http://localhost:5120/api/Categories/LoadCategory");
         if (!response.IsSuccessStatusCode)
         {
            return new List<CategoryDto>();
         }
         string result = await response.Content.ReadAsStringAsync();
         var category = JsonConvert.DeserializeObject<List<CategoryDto>>(result);
         return category ?? new List<CategoryDto>();
      }
      #endregion end-index

      #region start-Create
      [HttpGet]
      public IActionResult Create()
      {
         var category = new CategoryDto();

         return View(category);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create(CategoryDto model)
      {
         CategoryDto category = new CategoryDto
         {
            CategoryId = model.CategoryId,
            CategoryName = model.CategoryName,
            CreatedDate = DateTime.Now
         };
         var CreateCategoryAdded = await CreateCategory(category);

         if (CreateCategoryAdded == null)
         {
            return View(category);
         }
         return RedirectToAction("Index", "Categories");
      }

      private async Task<CategoryDto> CreateCategory(CategoryDto categories)
      {
         var data = JsonConvert.SerializeObject(categories);
         using var client = new HttpClient();
         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
         var response = await client.PostAsync($"http://localhost:5120/api/Categories/AddCategory", httpContent);

         if (!response.IsSuccessStatusCode)
         {
            return null;
         }

         string result = await response.Content.ReadAsStringAsync();
         return JsonConvert.DeserializeObject<CategoryDto>(result);
      }
      #endregion end-create


      #region start-edit
      public async Task<IActionResult> Edit(int categoryId)
      {
         var categoryInfo = await GetById(categoryId);

         if (categoryInfo == null)
            return RedirectToAction("Index");

         return View(categoryInfo);
      }


      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(CategoryDto categories)
      {
         CategoryDto category = new CategoryDto
         {
            CategoryId = categories.CategoryId,
            CategoryName = categories.CategoryName,
            CreatedDate = categories.CreatedDate,
            ModifiedDate = categories.ModifiedDate
         };
         var CategoryUpdated = await UpdateCategory(category);
         if (CategoryUpdated == null)
            return View(categories);
         return RedirectToAction("Index", categories);
      }

      private async Task<CategoryDto> UpdateCategory(CategoryDto categories)
      {
         var data = JsonConvert.SerializeObject(categories);
         using var client = new HttpClient();
         var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
         var response = await client.PutAsync($"http://localhost:5120/api/Categories/EditCategory", httpContent);

         if (!response.IsSuccessStatusCode)
            return null;

         string result = await response.Content.ReadAsStringAsync();
         return JsonConvert.DeserializeObject<CategoryDto>(result);
      }
      #endregion end-edit


      #region Start-Category-Delete
      //private async Task<CategoryDto> DeleteCategory(CategoryDto categories)
      //{
      //   var data = JsonConvert.SerializeObject(categories);
      //   using var client = new HttpClient();
      //   var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
      //   var response = await client.PostAsync($"http://localhost:5120/api/Categories/DeleteCategory", httpContent);

      //   if (!response.IsSuccessStatusCode)
      //      return null;

      //   string result = await response.Content.ReadAsStringAsync();
      //   return JsonConvert.DeserializeObject<CategoryDto>(result);
      //}

      //[HttpGet]
      //public async Task<IActionResult> Delete(int categoryId)
      //{
      //   var categoryDelInfo = await GetById(categoryId);

      //   if (categoryDelInfo == null)
      //      return RedirectToAction("Index");

      //   return View(categoryDelInfo);
      //}
      //[HttpPost]
      //[ValidateAntiForgeryToken]
      //public async Task<IActionResult> Delete(CategoryDto categories)
      //{
      //   CategoryDto category = new CategoryDto
      //   {
      //      CategoryId = categories.CategoryId,
      //      CategoryName = categories.CategoryName,
      //      CreatedDate = categories.CreatedDate,
      //      ModifiedDate = categories.ModifiedDate
      //   };
      //   var CategoryUpdated = await DeleteCategory(category);
      //   if (CategoryUpdated == null)
      //      return View(categories);
      //   return RedirectToAction("Index", categories);
      //}
      [HttpGet]
      public async Task<IActionResult> Delete(int categoryId)
      {
         var category = new CategoryDto();
         using (var client = new HttpClient())
         {
            var response = await client.GetAsync("http://localhost:5120/api/Categories/GetDataById?categoryId=" + categoryId);
            string result = response.Content.ReadAsStringAsync().Result;
            category = JsonConvert.DeserializeObject<CategoryDto>(result);
         }
         return View(category);
      }

      //[HttpPost("DeleteConfirm")]
      [HttpPost]
      public async Task<IActionResult> Delete(CategoryDto category)
      {
         var categoryJson = JsonConvert.SerializeObject(category);
         using (var client = new HttpClient())
         {
            HttpContent httpContent = new StringContent(categoryJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:5120/api/Categories/DeleteCategory", httpContent);

            string result = response.Content.ReadAsStringAsync().Result;
         }
         return RedirectToAction("Index", category);
      }
      #endregion End-Category-Delete



      private async Task<CategoryDto?> GetById(int categoryId)
      {
         var categoryInfo = new CategoryDto();
         using var client = new HttpClient();
         var response = await client.GetAsync($"http://localhost:5120/api/Categories/FindCategoryByKey/{categoryId}");
         if (!response.IsSuccessStatusCode)
         {
            return null;
         }
         string result = await response.Content.ReadAsStringAsync();
         categoryInfo = JsonConvert.DeserializeObject<CategoryDto>(result);

         return categoryInfo;
      }
   }
}
