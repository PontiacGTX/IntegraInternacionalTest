using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IEmpleadoRepository
    {
        Task<Empleado> Get(int id);
        Task<IList<Empleado>> GetAll();
        Task<Empleado> Update(Empleado empleado);
        Task<Empleado> Add(Empleado empleado);
        Task<bool> Exist(Expression<Func<Empleado, bool>> selector);
        Task<IList<Empleado>> GetAll(Expression<Func<Empleado, bool>> selector);
        Task<bool> Remove(int id);

    }
}
