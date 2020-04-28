namespace EmailCountsV2.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RecipientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [RegularExpression("^((?!\\.)[\\w-_.]*[^.])(@\\w+)(\\.\\w+(\\.\\w+)?[^.\\W])$", ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        public string Department { get; set; }
    }
}
