using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ExpenseTracker.Web.Controllers
{
    public class ExpenseTrakersController : Controller
    {
        public async Task<IActionResult>Index(string expenseDetaisId)
        {
            var expenseIn = await GetIdByCategoryId(expenseDetaisId);
            expenseIn = expenseIn.OrderBy(x => x.CreatedDate).ThenByDescending(x => x.ExpenseDate).ToList();
            
            return View(expenseIn);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var expense = new ExpernseDetailDto();
            return View(expense);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpernseDetailDto model)
        {
            ExpernseDetailDto expernse = new ExpernseDetailDto
            {
                ExpenseDetaisId = model.ExpenseDetaisId,
                ExpenseDate = model.ExpenseDate,
                ExpenseAmount = model.ExpenseAmount
            };
            var ExpenceAdd = await CreateExpenseDetail(expernse);
            if(ExpenceAdd == null)
            {
                return View(expernse);
            }
       
            return RedirectToAction("Index", "ExpenseTrakers");
        }
        public async Task<IActionResult> Edit(string expenseDetaisId,string categoryId)
        {
            ViewBag.CategoryId = categoryId;
            var expernseDetail = await GetById(expenseDetaisId);
            if (expernseDetail == null)
            {
                return RedirectToAction("Index", new {categoryId});
            }
                
            return View(expernseDetail);

        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpernseDetailDto expernseDetailDto)
        {
            var expenseDetailUpadate = await UpadateExpenseDetail(expernseDetailDto);
            if (expenseDetailUpadate == null)
            {
                ViewBag.CategoryId = expernseDetailDto.CategoryId;
                return View(expernseDetailDto);
            }
               
            return RedirectToAction("Index", new
            {
                categoryId =expenseDetailUpadate.CategoryId.ToString()
            });
        }


       private async Task<ExpernseDetailDto> CreateExpenseDetail(ExpernseDetailDto expernseDetailDto)
        {
            var data = JsonConvert.SerializeObject(expernseDetailDto);
            using var client=new HttpClient();
            var httpContent=new StringContent(data, Encoding.UTF8,"application/json");
            var response = await client.PostAsync($"http://localhost:5120/ExpenceDetails/AddCategory", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            string result =await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpernseDetailDto>(result);
        }

        private async Task<ExpernseDetailDto> UpadateExpenseDetail(ExpernseDetailDto expernseDetailDto)
        {
            var data = JsonConvert.SerializeObject(expernseDetailDto);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"http://localhost:5120/ExpenceDetails/AddCategory", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpernseDetailDto>(result);
        }

        private async Task< List<ExpernseDetailDto?>>GetById(string expenseDetaisId)
        {
            using var client =new HttpClient();
            var response =await client.GetAsync($"http://localhost:5120/ExpenceDetails/LoadExpenseDetail/{expenseDetaisId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ExpernseDetailDto>();
            }
            string result =await response.Content.ReadAsStringAsync();
            var expenseDetail = JsonConvert.DeserializeObject<List<ExpernseDetailDto>>(result);
            return expenseDetail ?? new List<ExpernseDetailDto>(); ;
        }
        private async Task<List<ExpernseDetailDto>> GetIdByCategoryId(string categoryId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5120/Categories/LoadExpenseDetail/{categoryId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ExpernseDetailDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            var admissions = JsonConvert.DeserializeObject<List<ExpernseDetailDto>>(result);
            return admissions ?? new List<ExpernseDetailDto>();
        }
    }

}