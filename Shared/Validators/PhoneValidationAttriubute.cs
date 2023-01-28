using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared.Validators
{
    public class PhoneValidation:ValidationAttribute
    {
        public PhoneValidation(string message):base(message)
        {

        }
       
        public override bool IsValid(object? value)
        {
            string val = value?.ToString()!;
            if (string.IsNullOrEmpty(val))
            return false;

            
             return Regex.Match(val, @"\(\d{3}\) ?\d{4}-\d{4}").Success;
        }
    }
}
