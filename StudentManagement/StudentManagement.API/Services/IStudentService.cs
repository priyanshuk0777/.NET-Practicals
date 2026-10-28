using StudentManagement.API.Models;

namespace StudentManagement.API.Services;

/// <summary>
/// Contract for the student business/data service.
/// The controller depends on this abstraction (not the concrete class),
/// which is registered in the DI container in Program.cs.
/// </summary>
public interface IStudentService
{
    IEnumerable<StudentResponseDto> GetAll();
    StudentResponseDto? GetById(int id);
    StudentResponseDto Create(CreateStudentDto dto);
    StudentResponseDto? Update(int id, UpdateStudentDto dto);
    bool Delete(int id);
}
