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

      /// <summary>
      /// Load All Expense Detail Entity
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      [Route("[action]")]
      public async Task<IActionResult> LoadExpenseDetail()
      {
         try
         {
            var expenseDetails = await unitOfWork.ExpenseDetailRepository.GetAll().ToListAsync();
            return Ok(expenseDetails);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }

      /// <summary>
      /// Data Based on CategoryId
      /// </summary>
      /// <param name="categoryId"></param>
      /// <returns></returns>
      [HttpGet]
      [Route("[action]/{categoryId}")]
      public async Task<IActionResult> CategoryBaseExpense(int categoryId)
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

      /// <summary>
      /// Insert Expense Details entity
      /// </summary>
      /// <param name="expenseDetailDto"></param>
      /// <returns></returns>
      [HttpPost]
      [Route("[action]")]
      public async Task<IActionResult> AddExpenseDetail([FromBody] ExpenseDetailDto expenseDetailDto)
      {
         try
         {
            var ExpenseDatailsEntity = new ExpenseDetail
            {
               ExpenseDetailId = expenseDetailDto.ExpenseDetailId,
               ExpenseAmount = expenseDetailDto.ExpenseAmount,
               ExpenseDate = expenseDetailDto.ExpenseDate,
               CategoryId = expenseDetailDto.CategoryId,
               CreatedDate = expenseDetailDto.CreatedDate
            };
            var ExpenceDetaillsAdd = unitOfWork.ExpenseDetailRepository.Add(ExpenseDatailsEntity);
            await unitOfWork.SaveChangesAsync();
            var expenseDetailReturn = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(ExpenceDetaillsAdd.ExpenseDetailId);
            return Ok(expenseDetailReturn);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went  wrong!");
         }
      }


      [HttpPut]
      [Route("[action]")]
      public async Task<IActionResult> EditExpenseDetail([FromBody] ExpenseDetailDto expenseDetailDto)
      {
         try
         {
            if (expenseDetailDto.ExpenseDetailId == Guid.Empty)
               return BadRequest();
            var expenseDetailDb = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(expenseDetailDto.ExpenseDetailId);
            if (expenseDetailDb == null)
               return NotFound();

            expenseDetailDb.ExpenseAmount = expenseDetailDto.ExpenseAmount;
            expenseDetailDb.ExpenseDate = expenseDetailDto.ExpenseDate;
            expenseDetailDb.ModifiedDate = expenseDetailDto.ModifiedDate;
            expenseDetailDb.CategoryId = expenseDetailDto.CategoryId;

            var updateExpense = unitOfWork.ExpenseDetailRepository.Update(expenseDetailDb);
            await unitOfWork.SaveChangesAsync();
            var expenseDetailReturn = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(updateExpense.ExpenseDetailId);
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
      public async Task<IActionResult>FindExpenseDetailByKey(Guid key)
      {
         try
         {
            var expenseDetail = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(key);
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
