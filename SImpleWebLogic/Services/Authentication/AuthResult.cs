public record AuthResult(
    bool Success,
    string Email,
    string UserName,
    string Token,
    string RefreshToken)
{
    public readonly Dictionary<string, string> ErrorMessages = new();
}
