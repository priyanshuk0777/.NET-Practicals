using System.ComponentModel.DataAnnotations;

namespace StudentManagement.API.Models;

/// <summary>
/// Request body for POST api/students (create a new student).
/// DataAnnotation attributes are validated automatically by [ApiController];
/// invalid input automatically produces a 400 Bad Request response.
/// </summary>
public class CreateStudentDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(60, MinimumLength = 2,
        ErrorMessage = "Name must be between 2 and 60 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course is required.")]
    [StringLength(40, ErrorMessage = "Course cannot exceed 40 characters.")]
    public string Course { get; set; } = string.Empty;

    [Range(15, 80, ErrorMessage = "Age must be between 15 and 80.")]
    public int Age { get; set; }
}

/// <summary>
/// Request body for PUT api/students/{id} (update an existing student).
/// Same validation rules as CreateStudentDto.
/// </summary>
public class UpdateStudentDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(60, MinimumLength = 2,
        ErrorMessage = "Name must be between 2 and 60 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course is required.")]
    [StringLength(40, ErrorMessage = "Course cannot exceed 40 characters.")]
    public string Course { get; set; } = string.Empty;

    [Range(15, 80, ErrorMessage = "Age must be between 15 and 80.")]
    public int Age { get; set; }
}

/// <summary>
/// Response body returned to the client for every successful operation.
/// Includes the generated Id and enrollment timestamp which clients
/// never send themselves.
/// </summary>
public class StudentResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
    public int Age { get; set; }
    public DateTime EnrolledOn { get; set; }
}
