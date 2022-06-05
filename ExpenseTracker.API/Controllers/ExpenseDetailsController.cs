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
        [HttpGet]
        [Route("action")]
        public async Task<IActionResult> AddExpenseDetails([FromBody] ExpernseDetailDto expernseDetailDto)
        {
            try
            {
                var ExpenseDatailsDb = new ExpenseDetail
                {
                    ExpenseAmount = expernseDetailDto.ExpenseAmount,
                    ExpenseDate = expernseDetailDto.ExpenseDate,
                    CategoryId = expernseDetailDto.CategoryId,
                    CreatedDate = expernseDetailDto.CreatedDate,
                    ModifiedDate = expernseDetailDto.ModifiedDate,
                    IsRowDeleted = expernseDetailDto.IsRowDeleted,
                };
                var ExpenceDetaillsAdd = unitOfWork.ExpenseDetailRepository.Add(ExpenseDatailsDb);
                await unitOfWork.SaveChangesAsync();
                var expenseDetailReturn = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(ExpenceDetaillsAdd.ExpenseDetaisId);
                return Ok(expenseDetailReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went  wrong!");


            }
        }
        [HttpGet]
        [Route("[action]/{ExpenseDetaisId}")]
        public async Task<IActionResult> LoadExpenseDetail(Guid expenseDetaisId)
        {
            try
            {
                var expenseDetailDb = await unitOfWork.ExpenseDetailRepository
                    .GetAll()
                    .Where(e => e.ExpenseDetaisId == expenseDetaisId && e.IsRowDeleted.Equals(false))
                    .OrderByDescending(o => o.CreatedDate)
                    .ToListAsync();
                return Ok(expenseDetailDb);


            }
            catch (Exception ex)
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
                expenseDetailDb.CreatedDate = expernseDetailDto.CreatedDate;
                expenseDetailDb.ModifiedDate = expernseDetailDto.ModifiedDate;
                expenseDetailDb.CategoryId = expernseDetailDto.CategoryId;
                expenseDetailDb.IsRowDeleted = expernseDetailDto.IsRowDeleted;
                var updateExpense = unitOfWork.ExpenseDetailRepository.Update(expenseDetailDb);
                await unitOfWork.SaveChangesAsync();
                var expenseDetailReturn = await unitOfWork.ExpenseDetailRepository.GetByIdAsync(updateExpense.ExpenseDetaisId);
                return Ok(expenseDetailReturn);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something want wrong!");

            }
        }
    }

}
