using System.ComponentModel.DataAnnotations;

public record RegistrationRequest(
    [Required] string Email,
    [Required] string Username,
    [Required] string Password);