using ExpenseTracker.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ExpenseTracker.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var catagory = await GetById();
            if (catagory == null)
                return RedirectToAction("Index");
            return View(catagory);
        }

        private async Task<CategoryDto?> GetById()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5120/Categories/LoadCategory/");
            if (!response.IsSuccessStatusCode)
                return null;
            string result = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryDto>(result);
            return category;
        }

        private async Task<CategoryDto> CreateCategory(CategoryDto categories)
        {
            var data = JsonConvert.SerializeObject(categories);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"http://localhost:5120/Categories/AddCategory", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryDto>(result);

        }
        private async Task<CategoryDto>UpdateCategory(CategoryDto categories)
        {
            var data = JsonConvert.SerializeObject(categories);
            using var client = new HttpClient();
            var httpContent = new StringContent(data,Encoding.UTF8,"application/json");
            var response = await client.PutAsync($"http://localhost:5120/Categories/EditCategory", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CategoryDto>(result);

        }


        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            var category = new CategoryDto();
           
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(CategoryDto model)
        {
            CategoryDto category = new CategoryDto
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
            };
            var CreateCategoryAdded = await CreateCategory(category);

            if (CreateCategoryAdded == null)
            {
                return View(category);
            }
            return RedirectToAction("Index", "Categories");

        }
        #endregion


        #region Edit
        public async Task<IActionResult>Edit(string categoryId)
        {
            var categories =await GetById();
            if (categories == null)
                return RedirectToAction("Index");
            return View(categories);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto categories)
        {
            var CategoryUpdated = await UpdateCategory(categories);
            if (CategoryUpdated == null)
                return View(categories);
            return RedirectToAction("Index", new
            {
                categoryId = CategoryUpdated.CategoryId.ToString()
            });

        }
        #endregion




    }
}
