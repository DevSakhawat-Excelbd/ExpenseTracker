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
        public async Task<IActionResult> Create(string categoryId)
        {
            var expense = await GetIdByCategoryId(categoryId);
            ViewBag.CategoryId = categoryId;
            var expenseDetailDto = new ExpenseDetail();
            return View(expenseDetailDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpernseDetailDto model)
        {
            ExpernseDetailDto expernseDetail = new ExpernseDetailDto();
            {
                expernseDetail.ExpenseDetaisId = model.ExpenseDetaisId;
                expernseDetail.ExpenseAmount = model.ExpenseAmount;
                expernseDetail.ExpenseDate = model.ExpenseDate;
                expernseDetail.CreatedDate = model.CreatedDate;
            }
            var expenseDetailAdd=await CreateExpenseDetail(expernseDetail); 
            if (expenseDetailAdd == null)
            {
                ViewBag.CategoryId = expernseDetail.CategoryId;
                return View(expernseDetail);
            }
            return RedirectToAction("Index", new
            {
                expernseDetailId = expenseDetailAdd.ExpenseDetaisId.ToString(),
                categoryId=expenseDetailAdd.CategoryId
            
            
            });
            
            
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
            var response = await client.PostAsync($"https://localhost:5120/Categories/AddCategory", httpContent);

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
            var response = await client.PutAsync($"https://localhost:5120/Categories/AddCategory", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpernseDetailDto>(result);
        }

        private async Task<ExpernseDetailDto>GetById(string expenseDetaisId)
        {
            using var client =new HttpClient();
            var response =await client.GetAsync($"https://localhost:5120/Categories/LoadExpenseDetail/{expenseDetaisId}");
            if (!response.IsSuccessStatusCode)

                return null;

            string result =await response.Content.ReadAsStringAsync();
            var expenseDetail = JsonConvert.DeserializeObject<ExpernseDetailDto>(result);
            return expenseDetail;
        }
        private async Task<List<ExpernseDetailDto>> GetIdByCategoryId(string categoryId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:5120/Categories/LoadExpenseDetail/{categoryId}");
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