using Data;
using Data.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IEmpleadoServices
    {
        Task<Empleado> Add(Empleado empleado, IFormFile file);
        Task<Empleado> Get(int id);
        Task<IList<Empleado>> GetAll();
        Task<Empleado> Add(Empleado empleado);
        Task<Empleado> Update(Empleado empleado);
        Task<Empleado> Update(EmpleadoEditModel empleado);
        Task<bool> Exist(Expression<Func<Empleado, bool>> selector);
        Task<IList<Empleado>> GetAll(Expression<Func<Empleado, bool>> selector);
        Task<bool> Remove(int id);
    }
}
