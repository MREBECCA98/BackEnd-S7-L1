using BackEnd_S7_L1.Models.Dto;
using BackEnd_S7_L1.Models.Entities;
using BackEnd_S7_L1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd_S7_L1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet()]
        public async Task<IActionResult> Gets()
        {
            try
            {
                return Ok(await _studentService.GetAsNoTracking());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        //by id
        [HttpGet("{Id:guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            try
            {
                if (Guid.Empty == Id)
                {
                    return BadRequest();
                }
                return Ok(await _studentService.GetByIdAsNoTracking(Id));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //by name
        [HttpGet("by-name/{Nome}")]
        public async Task<IActionResult> GetByName(string Nome)
        {
            try
            {
                Student student = await _studentService.GetByNameAsNoTracking(Nome);

                if (student is null)
                {
                    return BadRequest();
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("search/{search-term}")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            try
            {
                List<Student> students = await _studentService.SearchByTermAsNoTracking(searchTerm);
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<StudentResponseDto>> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest();

                StudentResponseDto? result = await _studentService.Delete(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut]
        public async Task<IActionResult> Update(StudentRequestDto student)
        {
            try
            {
                if (student is not null && student.Id != Guid.Empty)
                {
                    Student fromDB = await this._studentService.GetById(student.Id);

                    if (fromDB is not null)
                    {
                        fromDB.Nome = student.Nome;
                        fromDB.Cognome = student.Cognome;
                        fromDB.Email = student.Email;
                        bool result = await this._studentService.Save();

                        return result ? Ok() : BadRequest();

                    }
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}