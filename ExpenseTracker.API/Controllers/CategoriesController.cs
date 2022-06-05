using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
   /// <summary>
   /// Categories Controller
   /// </summary>
   [Route("api/[controller]")]
   [ApiController]
   public class CategoriesController : ControllerBase
   {
      private readonly IUnitOfWork unitOfWork;
      /// <summary>
      /// Construtor for CategoriesContrroler
      /// </summary>
      /// <param name="unitOfWork"></param>
      public CategoriesController(IUnitOfWork unitOfWork)
      {
         this.unitOfWork = unitOfWork;
      }
      [HttpGet]
      [Route("[action]")]
      public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
      {
         try
         {
            var categoryInId = new Category
            {
               CategoryId = categoryDto.CategoryId,
               CategoryName = categoryDto.CategoryName,
               CreatedDate = categoryDto.CreatedDate
            };
            var categoryAdded = unitOfWork.CategoryRepository.Add(categoryInId);
            await unitOfWork.SaveChangesAsync();

            var categoryToReturn = await unitOfWork.CategoryRepository.GetByIdAsync(categoryAdded.CategoryId);
            return Ok(categoryAdded);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }


   }
}
