using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.API.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ExpenseDetailsController : ControllerBase
   {
      private readonly IUnitOfWork unitOfWork;
      public ExpenseDetailsController(IUnitOfWork unitOfWork)
      {
         this.unitOfWork = unitOfWork;
      }
      [HttpPost]
      [Route("[action]")]
      public async Task<IActionResult> AddExpenseDetail([FromBody] ExpernseDetailDto expernseDetailDto)
      {
         try
         {
            var ExpenseDatailsDb = new ExpenseDetail
            {
               ExpenseDetaisId = expernseDetailDto.ExpenseDetaisId,
               ExpenseAmount = expernseDetailDto.ExpenseAmount,
               ExpenseDate = expernseDetailDto.ExpenseDate,
               CategoryId = expernseDetailDto.CategoryId,
               CreatedDate = expernseDetailDto.CreatedDate
               
            };
            var ExpenceDetaillsAdd = unitOfWork.ExpenseDetailRepository.Add(ExpenseDatailsDb);
            await unitOfWork.SaveChangesAsync();
            var expenseDetailReturn = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(ExpenceDetaillsAdd.ExpenseDetaisId);
            return Ok(expenseDetailReturn);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went  wrong!");
         }
      }
      [HttpGet]
      [Route("[action]/{categoryId}")]
      public async Task<IActionResult> LoadExpenseDetail(Guid categoryId)
      {
         try
         {
            var expenseDetailDb = await unitOfWork.ExpenseDetailRepository
                .GetAll()
                .Where(e => e.CategoryId == categoryId && e.IsRowDeleted.Equals(false))
                .OrderByDescending(o => o.CreatedDate)
                .ToListAsync();
            return Ok(expenseDetailDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something want Wrong!");
         }
      }
      [HttpPut]
      [Route("[action]")]
      public async Task<IActionResult> EditExpenseDetail([FromBody] ExpernseDetailDto expernseDetailDto)
      {
         try
         {
            if (expernseDetailDto.ExpenseDetaisId == Guid.Empty)
               return BadRequest();
            var expenseDetailDb = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(expernseDetailDto.ExpenseDetaisId);
            if (expenseDetailDb == null)
               return NotFound();

            expenseDetailDb.ExpenseAmount = expernseDetailDto.ExpenseAmount;
            expenseDetailDb.ExpenseDate = expernseDetailDto.ExpenseDate;
            expenseDetailDb.ModifiedDate = expernseDetailDto.ModifiedDate;
            expenseDetailDb.CategoryId = expernseDetailDto.CategoryId;

            var updateExpense = unitOfWork.ExpenseDetailRepository.Update(expenseDetailDb);
            await unitOfWork.SaveChangesAsync();
            var expenseDetailReturn = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(updateExpense.ExpenseDetaisId);
            return Ok(expenseDetailReturn);

         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something want wrong!");

         }
      }

      /// <summary>
      /// Find Expense by key
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      [HttpGet]
      [Route("[action]/{key}")]
      public async Task<IActionResult> FindExpenseDetailByKey(Guid key)
      {
         try
         {
            var expenseDetail = await unitOfWork.CategoryRepository.GetByIdAsync(key);
            if (expenseDetail == null)
               return NotFound();

            return Ok(expenseDetail);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }



   }
}
