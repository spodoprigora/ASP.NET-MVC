using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace eShop.Models
{
    public class userModel
    {
            [Required]
            public string Id { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            public string UserName { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string Adress { get; set; }
            [Required]
            public string PasswordHash { get; set; }

            
    }
}