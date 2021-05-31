using KsiazkaAdresowa.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.DTOs
{
    public class AddressDto
    {
        [Required]
        public Appliciant TypeOfAppliciant { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [RegularExpression(@"(^[5-9][0-9]{8}$)|(^[0-9]{2,3}-[0-9]{7}$)|(^\+48-[0-9]{9}$)", ErrorMessage ="Niepoprawny numer telefonu")]
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^[0-9A-Za-z]+@[A-Za-z]+$", ErrorMessage ="Niepoprawny format e-mail")]
        public string Email { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public int NumberOfBuilding { get; set; }
        public int NumberOfLocal { get; set; }
        [Required]
        public string Home { get; set; }
        [Required]
        public string Post { get; set; }
        [Required]
        [RegularExpression(@"^\+?\d{1,2}\-?\d{3}$", ErrorMessage ="Niepoprawny kod pocztowy")]
        public string PostCode { get; set; }
        [Required]
        public Country Country { get; set; }
    }
}
