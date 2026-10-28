using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.Models;
using StudentManagement.API.Services;

namespace StudentManagement.API.Controllers;

/// <summary>
/// REST API controller for Student management (CRUD operations).
///
/// [ApiController] enables:
///   - Automatic model validation: invalid DTOs return 400 Bad Request
///     before the action code even runs.
///   - Attribute routing requirement.
///
/// Route base:  https://localhost:7017/api/students
/// </summary>
[ApiController]
[Route("api/[controller]")]   // [controller] = "Students"  ->  api/students
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly ILogger<StudentsController> _logger;

    // Constructor Injection: the DI container supplies the service
    // registered in Program.cs (AddSingleton<IStudentService, StudentService>()).
    public StudentsController(IStudentService studentService,
        ILogger<StudentsController> logger)
    {
        _studentService = studentService;
        _logger = logger;
    }

    /// <summary>GET api/students - returns all students.</summary>
    [HttpGet]
    public ActionResult<IEnumerable<StudentResponseDto>> GetAllStudents()
    {
        _logger.LogInformation("Fetching all students");
        return Ok(_studentService.GetAll());
    }

    /// <summary>GET api/students/{id} - returns one student or 404.</summary>
    [HttpGet("{id:int}")]
    public ActionResult<StudentResponseDto> GetStudentById(int id)
    {
        var student = _studentService.GetById(id);

        if (student is null)
        {
            _logger.LogWarning("Student with id {Id} was not found", id);
            return NotFound(new { message = $"Student with id {id} was not found." });
        }

        return Ok(student);
    }

    /// <summary>
    /// POST api/students - creates a student.
    /// Returns 201 Created with a Location header pointing at the new resource.
    /// </summary>
    [HttpPost]
    public ActionResult<StudentResponseDto> CreateStudent(CreateStudentDto dto)
    {
        var created = _studentService.Create(dto);
        _logger.LogInformation("Student created with id {Id}", created.Id);

        return CreatedAtAction(
            nameof(GetStudentById),          // where the new resource lives
            new { id = created.Id },          // route values for that action
            created);                         // response body
    }

    /// <summary>PUT api/students/{id} - updates a student or returns 404.</summary>
    [HttpPut("{id:int}")]
    public IActionResult UpdateStudent(int id, UpdateStudentDto dto)
    {
        var updated = _studentService.Update(id, dto);

        if (updated is null)
        {
            _logger.LogWarning("Update failed - student {Id} not found", id);
            return NotFound(new { message = $"Student with id {id} was not found." });
        }

        return Ok(updated);
    }

    /// <summary>DELETE api/students/{id} - deletes a student, returns 204 No Content.</summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteStudent(int id)
    {
        var deleted = _studentService.Delete(id);

        if (!deleted)
        {
            _logger.LogWarning("Delete failed - student {Id} not found", id);
            return NotFound(new { message = $"Student with id {id} was not found." });
        }

        return NoContent();
    }
}
