using StudentManagement.API.Models;

namespace StudentManagement.API.Services;

/// <summary>
/// In-memory implementation of IStudentService.
/// Data lives in a static list for the lifetime of the application
/// (restart = data resets to the seeded records). No database needed.
/// </summary>
public class StudentService : IStudentService
{
    // The in-memory "table". A lock keeps it thread-safe because a
    // Singleton service is shared by all concurrent HTTP requests.
    private readonly List<Student> _students = new();
    private readonly object _lock = new();
    private int _nextId = 1;

    public StudentService()
    {
        // Seed a few records so GET endpoints return useful data on first run.
        var seeds = new[]
        {
            new CreateStudentDto { Name = "Rahul Sharma", Email = "rahul.sharma@example.com", Course = "B.Tech CSE", Age = 20 },
            new CreateStudentDto { Name = "Priya Verma",  Email = "priya.verma@example.com",  Course = "B.Tech IT",  Age = 21 },
            new CreateStudentDto { Name = "Aman Patel",   Email = "aman.patel@example.com",   Course = "BCA",        Age = 19 }
        };

        foreach (var seed in seeds)
        {
            AddInternal(seed);
        }
    }

    /// <summary>GET all students.</summary>
    public IEnumerable<StudentResponseDto> GetAll()
    {
        lock (_lock)
        {
            return _students.Select(MapToResponse).ToList();
        }
    }

    /// <summary>GET a single student by id, or null if not found.</summary>
    public StudentResponseDto? GetById(int id)
    {
        lock (_lock)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            return student is null ? null : MapToResponse(student);
        }
    }

    /// <summary>POST - create a new student with an auto-incremented Id.</summary>
    public StudentResponseDto Create(CreateStudentDto dto)
    {
        lock (_lock)
        {
            return MapToResponse(AddInternal(dto));
        }
    }

    /// <summary>PUT - update an existing student. Returns null if not found.</summary>
    public StudentResponseDto? Update(int id, UpdateStudentDto dto)
    {
        lock (_lock)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student is null)
            {
                return null;
            }

            student.Name = dto.Name;
            student.Email = dto.Email;
            student.Course = dto.Course;
            student.Age = dto.Age;

            return MapToResponse(student);
        }
    }

    /// <summary>DELETE - remove a student. Returns false if not found.</summary>
    public bool Delete(int id)
    {
        lock (_lock)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student is null)
            {
                return false;
            }

            _students.Remove(student);
            return true;
        }
    }

    // ---------------- private helpers ----------------

    private Student AddInternal(CreateStudentDto dto)
    {
        var student = new Student
        {
            Id = _nextId++,
            Name = dto.Name,
            Email = dto.Email,
            Course = dto.Course,
            Age = dto.Age,
            EnrolledOn = DateTime.UtcNow
        };

        _students.Add(student);
        return student;
    }

    private static StudentResponseDto MapToResponse(Student s) => new()
    {
        Id = s.Id,
        Name = s.Name,
        Email = s.Email,
        Course = s.Course,
        Age = s.Age,
        EnrolledOn = s.EnrolledOn
    };
}
