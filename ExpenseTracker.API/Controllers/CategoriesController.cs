using ExpenseTracker.Domain.Dto;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

      /// <summary>
      /// Load Category List
      /// </summary>
      /// <returns></returns>
      [HttpGet]
      [Route("[action]")]
      public async Task<IActionResult> LoadCategory()
      {
         try
         {
            var categoryList = await unitOfWork.CategoryRepository.GetAll().ToListAsync();
            return Ok(categoryList);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }

      [HttpPost]
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

      /// <summary>
      /// Edit an Category
      /// </summary>
      /// <param name="category"></param>
      /// <returns></returns>
      [HttpPut]
      [Route("[action]")]
      public async Task<IActionResult> EditCategory(CategoryDto category)
      {
         try
         {
            var categoryEntity = await unitOfWork.CategoryRepository.GetByIdAsync(category.CategoryId);
            if (categoryEntity == null)
            {
               return NotFound();
            }
            else
            {
               categoryEntity.CategoryId = category.CategoryId;
               categoryEntity.CategoryName = category.CategoryName;
               categoryEntity.ModifiedDate = DateTime.Now;
            }
            var categoryEdit = unitOfWork.CategoryRepository.Update(categoryEntity);
            await unitOfWork.SaveChangesAsync();

            var categoryToResult = await unitOfWork.CategoryRepository.GetByIdAsync(category.CategoryId);

            return Ok(categoryToResult);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }


      //[HttpPost("delete")]
      [HttpPost]
      public async Task<IActionResult> DeleteCategory([FromBody] CategoryDto category)
      {
         try
         {
            var categoryEntity = await unitOfWork.CategoryRepository.GetByIdAsync(category.CategoryId);
            if (categoryEntity == null)
            {
               return NotFound();
            }
            else
            {
               categoryEntity.CategoryId = category.CategoryId;
               categoryEntity.CategoryName = category.CategoryName;
               categoryEntity.ModifiedDate = category.ModifiedDate;
               categoryEntity.CreatedDate = category.CreatedDate;
            }
            unitOfWork.CategoryRepository.Delete(categoryEntity);
            await unitOfWork.SaveChangesAsync();

            //var categoryToResult = await unitOfWork.CategoryRepository.GetByIdAsync(category.CategoryId);

            //return Ok(categoryToResult);

            //unitOfWork.CategoryRepository.Delete(categoryEntity);
            //unitOfWork.SaveChanges();
            return Ok();
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
         
      }


      /// <summary>
      /// Find Category by key
      /// </summary>
      /// <param name="key"></param>
      /// <returns></returns>
      [HttpGet]
      [Route("[action]/{key}")]
      public async Task<IActionResult> FindCategoryByKey(int key)
      {
         try
         {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(key);
            if (category == null)
               return NotFound();

            return Ok(category);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
         }
      }
   }
}
