using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Validators
{
    public class EmailValidationAttribute:ValidationAttribute
    {
        
        public override bool IsValid(object? value)
        {
            try
            {
                
                return !string.IsNullOrEmpty(new MailAddress(value?.ToString()!).Address);
            }
            catch
            {
                return false;
            }
        }
    }
}
