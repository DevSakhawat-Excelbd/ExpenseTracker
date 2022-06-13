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
        public async Task<IActionResult>Index()
        {
            var expenseIn = await GetById();
            expenseIn = expenseIn.OrderBy(x => x.CreatedDate).ThenByDescending(x => x.ExpenseDate).ToList();
            
            return View(expenseIn);
        }
        public async Task<IActionResult> Create()
        {
            var expense = await GetIdByCategoryId();
            ViewData["CategoryId"] = new SelectList(expense, "CategoryId", "CategoryName");
            var expenseDetailDto = new ExpenseDetailDto();
            return View(expenseDetailDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseDetailDto model)
        {
            ExpenseDetailDto expenseDetail = new ExpenseDetailDto
            {
                ExpenseDetailId = model.ExpenseDetailId,
                ExpenseAmount = model.ExpenseAmount,
                ExpenseDate = model.ExpenseDate,
                CategoryId = model.CategoryId,
                CreatedDate = DateTime.Now
            };
            var expenseDetailAdd=await CreateExpenseDetail(expenseDetail); 
            if (expenseDetailAdd == null)
            {
                //ViewBag.CategoryId = expenseDetail.CategoryId;
                return View(expenseDetail);
            }
            return RedirectToAction("Index", new
            {
                expernseDetailId = expenseDetailAdd.ExpenseDetailId.ToString(),
                categoryId=expenseDetailAdd.CategoryId
            
            
            });
            
            
        }

        public async Task<IActionResult> Edit(Guid id,int categoryId)
        {
            var expense = await GetIdByCategoryId();
            ViewData["CategoryId"] = new SelectList(expense, "CategoryId", "CategoryName");
            //var expernseDetail = await GetById(expenseDetailsId);
            //if (expernseDetail == null)
            //{
            //    return RedirectToAction("Index");
            //}

            //return View(expernseDetail);

            var expenseDetail = await GetById(id);

            if (expenseDetail == null)
                return RedirectToAction("Index");

            return View(expenseDetail);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpenseDetailDto expenseDetailDto)
        {
            var expenseDetailUpadate = await UpadateExpenseDetail(expenseDetailDto);
            if (expenseDetailUpadate == null)
            {
                var expense = await GetIdByCategoryId();
                ViewData["CategoryId"] = new SelectList(expense, "CategoryId", "CategoryName");
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
            var response = await client.PostAsync($"http://localhost:5120/api/ExpenseDetails/AddExpenseDetail", httpContent);

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
            var response = await client.PutAsync($"http://localhost:5120/api/ExpenseDetails/EditExpenseDetail", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExpenseDetailDto>(result);
        }

        private async Task<List<ExpenseDetailDto?>>GetById()
        {
            using var client =new HttpClient();
            var response =await client.GetAsync($"http://localhost:5120/api/ExpenseDetails/LoadExpenseDetail");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ExpenseDetailDto>();
            }
               
            string result =await response.Content.ReadAsStringAsync();
            var expenseDetail = JsonConvert.DeserializeObject<List<ExpenseDetailDto>>(result);
            return expenseDetail ?? new List<ExpenseDetailDto>();
        }
        private async Task<List<CategoryDto>> GetIdByCategoryId()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5120/api/Categories/LoadCategory");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CategoryDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            var admissions = JsonConvert.DeserializeObject<List<CategoryDto>>(result);
            return admissions ?? new List<CategoryDto>();
        }
        private async Task<ExpenseDetailDto?> GetById(Guid? id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5120/api/ExpenseDetails/FindExpenseDetailByKey/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            string result = await response.Content.ReadAsStringAsync();
            var categoryInfo = JsonConvert.DeserializeObject<ExpenseDetailDto>(result);

            return categoryInfo;
        }
    }

}