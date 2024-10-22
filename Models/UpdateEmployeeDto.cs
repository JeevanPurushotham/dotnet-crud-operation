namespace EmployeeAdminPortal.Models
{
    public class UpdateEmployeeDto
    {
        public required Guid Id { get; set; } // Add the ID property
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
    }
}
