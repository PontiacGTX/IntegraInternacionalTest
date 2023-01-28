using Microsoft.AspNetCore.Http;
using Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class EmpleadoEditModel
    {
        public EmpleadoEditModel()
        {

        }
        public EmpleadoEditModel(Empleado empleado)
        {
            this.Apellido = empleado.Apellido;
            this.Telefono = empleado.Telefono;
            this.Correo = empleado.Correo;
            this.FechaContratacion = DateTime.Parse(empleado.FechaContratacion);
            this.Foto = empleado.Foto;
            this.Nombre = empleado.Nombre;
            this.Id=empleado.Id;
        }
        public IFormFile File { get; set; }
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [PhoneValidation("El formato de telefono debe ser (xxx) xxxx-xxxx")]
        public string Telefono { get; set; }
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        public string Foto { get; set; }
        [Display(Name ="Fecha Contratacion")]
        public DateTime FechaContratacion { get; set; }
    }
}
