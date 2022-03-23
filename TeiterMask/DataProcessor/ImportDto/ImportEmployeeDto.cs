namespace TeisterMask.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;

    using Common;

    public class ImportEmployeeDto
    {
        [Required]
        [MinLength(GlobalConstants.EMPLOYEE_USERNAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.EMPLOYEE_USERNAME_MAX_LENGTH)]
        [RegularExpression(GlobalConstants.EMPLOYEE_USERNAME_REGEX)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(GlobalConstants.EMPLOYEE_PHONE_REGEX)]
        public string Phone { get; set; }

        public int[] Tasks { get; set; }
    }
}
