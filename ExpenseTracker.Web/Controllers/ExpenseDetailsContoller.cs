using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ExpenseTracker.Web.Controllers
{
    public class ExpenseDetailsContoller : Controller
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
        public async Task<IActionResult> Create(ExpenseDetailDto model)
        {
         ExpenseDetailDto expenseDetail = new ExpenseDetailDto();
            {
            expenseDetail.ExpenseDetailId = model.ExpenseDetailId;
            expenseDetail.ExpenseAmount = model.ExpenseAmount;
            expenseDetail.ExpenseDate = model.ExpenseDate;
            expenseDetail.CreatedDate = model.CreatedDate;
            }
            var expenseDetailAdd=await CreateExpenseDetail(expenseDetail); 
            if (expenseDetailAdd == null)
            {
                ViewBag.CategoryId = expenseDetail.CategoryId;
                return View(expenseDetail);
            }
            return RedirectToAction("Index", new
            {
                expernseDetailId = expenseDetailAdd.ExpenseDetailId.ToString(),
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
        public async Task<IActionResult> Edit(ExpenseDetailDto expenseDetailDto)
        {
            var expenseDetailUpadate = await UpadateExpenseDetail(expenseDetailDto);
            if (expenseDetailUpadate == null)
            {
                ViewBag.CategoryId = expenseDetailDto.CategoryId;
                return View(expenseDetailDto);
            }
               
            return RedirectToAction("Index", new
            {
                categoryId =expenseDetailUpadate.CategoryId.ToString()
            });
        }


       private async Task<ExpenseDetailDto> CreateExpenseDetail(ExpenseDetailDto expernseDetailDto)
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
            return JsonConvert.DeserializeObject<ExpenseDetailDto>(result);
        }

        private async Task<ExpenseDetailDto> UpadateExpenseDetail(ExpenseDetailDto expernseDetailDto)
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
            return JsonConvert.DeserializeObject<ExpenseDetailDto>(result);
        }

        private async Task<ExpenseDetailDto>GetById(string expenseDetaisId)
        {
            using var client =new HttpClient();
            var response =await client.GetAsync($"https://localhost:5120/Categories/LoadExpenseDetail/{expenseDetaisId}");
            if (!response.IsSuccessStatusCode)

                return null;

            string result =await response.Content.ReadAsStringAsync();
            var expenseDetail = JsonConvert.DeserializeObject<ExpenseDetailDto>(result);
            return expenseDetail;
        }
        private async Task<List<ExpenseDetailDto>> GetIdByCategoryId(string categoryId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:5120/Categories/LoadExpenseDetail/{categoryId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ExpenseDetailDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            var admissions = JsonConvert.DeserializeObject<List<ExpenseDetailDto>>(result);
            return admissions ?? new List<ExpenseDetailDto>();
        }
    }

}