using ExpenseTracker.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ExpenseTracker.Web.Controllers
{
    public class ExpenseTrakersController : Controller
    {
        public async Task<IActionResult>Index(string expenseDetaisId)
        {
            var expenseIn = await GetById(expenseDetaisId);
            if (expenseIn == null)
                return RedirectToAction("Index");
            return View(expenseIn);
        }
       private async Task<ExpernseDetailDto> CreateExpenseDetail(ExpernseDetailDto expernseDetailDto)
        {
            var data = JsonConvert.SerializeObject(expernseDetailDto);
            using var client=new HttpClient();
            var httpContent=new StringContent(data, Encoding.UTF8,"application/json");
            var response=await
        }
        private async Task<ExpernseDetailDto>GetById(string expenseDetaisId)
        {
            using var client =new HttpClient();
            var response =await client.GetAsync($"https://localhost:7120/Categories/LoadExpenseDetail/{expenseDetaisId}");
            if (!response.IsSuccessStatusCode)

                return null;

            string result =await response.Content.ReadAsStringAsync();
            var expenseDetail = JsonConvert.DeserializeObject<ExpernseDetailDto>(result);
            return expenseDetail;
        }
    }

}
