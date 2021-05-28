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
        public Appliciant TypeOfAppliciant { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        [RegularExpression(@"(^[5-9][0-9]{8}$)|(^[0-9]{2,3}-[0-9]{7}$)|(^\+48-[0-9]{9}$)", ErrorMessage ="Niepoprawny numer telefonu")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^[0-9A-Za-z]+@[A-Za-z]+$", ErrorMessage ="Niepoprawny format e-mail")]
        public string Email { get; set; }
        public string Street { get; set; }
        public int NumberOfBuilding { get; set; }
        public int NumberOfLocal { get; set; }
        public string Home { get; set; }
        public string Post { get; set; }
        [RegularExpression(@"^\+?\d{1,2}\-?\d{3}$", ErrorMessage ="Niepoprawny kod pocztowy")]
        public string PostCode { get; set; }
        public Country Country { get; set; }
    }
}
