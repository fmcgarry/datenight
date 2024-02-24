using System.ComponentModel.DataAnnotations;

namespace DateNightApp.Pages.Account;

internal class PartnerCodeModel
{
    [Required(ErrorMessage = "Partner code is required.")]
    [MinLength(8, ErrorMessage = "Partner code must be at least 8 characters.")]
    public string Code { get; set; } = string.Empty;
}