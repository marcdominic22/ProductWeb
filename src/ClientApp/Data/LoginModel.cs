using System.ComponentModel.DataAnnotations;

namespace ClientApp.Data;

public class LoginModel
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    public string TwoFactorCode { get; set; } = string.Empty;
    public string TwoFactorRecoveryCode { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string TokenType { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public long ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
}
