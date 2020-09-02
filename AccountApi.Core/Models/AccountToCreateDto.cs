using System;
using System.ComponentModel.DataAnnotations;

namespace AccountApi.Core.Models
{
    public class AccountToCreateDto
    {
        [Required]
        public Guid? CustomerId { get; set; }
        
        [Range(0, (double)decimal.MaxValue)]
        public decimal InitialCredit { get; set; }
        public string Name { get; set; }
    }
}
