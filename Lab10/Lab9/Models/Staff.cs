namespace Lab9.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Qualifications { get; set; }
        public string? OtherDetails { get; set; }
    }
}
