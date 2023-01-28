using Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Empleado
    {
        public Empleado()
        {

        }
        public Empleado(EmpleadoModel model)
        {
            this.Apellido = model.Apellido;
            this.Telefono = model.Telefono.Trim();
            this.Correo = model.Correo.Trim();
            this.FechaContratacion = model.FechaContratacion.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            this.Nombre = model.Nombre;
            this.Foto = "placerholder";
        }
        public void Set(EmpleadoEditModel empleadoEditModel)
        {
            this.Apellido   = empleadoEditModel.Apellido;
            this.Telefono= empleadoEditModel.Telefono.Trim();
            this.Correo = empleadoEditModel.Correo.Trim();
            this.Apellido= empleadoEditModel.Apellido;
            this.FechaContratacion = empleadoEditModel.FechaContratacion.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            this.Nombre  = empleadoEditModel.Nombre;
        }

        



        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Foto { get; set; }
        [Display(Name ="Fecha Contratacion")]
        public string FechaContratacion { get; set; }
    }
}
