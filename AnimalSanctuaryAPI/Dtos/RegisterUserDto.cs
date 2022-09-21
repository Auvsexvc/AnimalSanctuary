namespace AnimalSanctuaryAPI.Dtos
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
    }
}