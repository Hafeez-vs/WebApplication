namespace WebApplication1.Dto
{
    public class UpdateDto
    {
        public string OldUsername { get; set; }
        public string OldPassword { get; set; }
        public string? newUsername { get; set; }
        public string? newPassword { get; set; }
    }
}
