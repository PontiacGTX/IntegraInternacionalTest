using Microsoft.AspNetCore.Http;
using Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Data.Model
{
    public class EmpleadoModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [PhoneValidation("El formato de telefono debe ser (xxx) xxxx-xxxx")]
        [Required]
        public string Telefono { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Display(Name ="Foto")]
        public IFormFile File { get; set; }

        [Display(Name = "Fecha Contratacion")]
        [Required]
        public DateTime FechaContratacion { get; set; }
    }
}
