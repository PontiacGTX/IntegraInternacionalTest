using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EmpleadoRepository: IEmpleadoRepository
    {
        private AppDbContext _appDbContext;

        public EmpleadoRepository(AppDbContext appDbContext) { _appDbContext = appDbContext; }

        public async Task<Empleado> Get(int id)
            => (await _appDbContext.Empleados.FirstOrDefaultAsync(x => x.Id == id))!;

        public async Task<IList<Empleado>> GetAll() => await _appDbContext.Empleados.ToListAsync();

        public async Task<Empleado> Update(Empleado empleado)
        {
            try
            {
                var current = await Get(empleado.Id);
                if(current==null)
                {
                    throw new NullReferenceException();
                }

                var entry = _appDbContext.Entry(current);
                    entry.CurrentValues.SetValues(empleado);

               await _appDbContext.SaveChangesAsync();

                return entry.Entity;

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task<Empleado> Add(Empleado empleado)
        {
            try
            {

                var entry =_appDbContext.Add(empleado);
                await _appDbContext.SaveChangesAsync();

                return entry.Entity;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> Exist(Expression<Func<Empleado,bool>> selector)
            => await _appDbContext.Empleados.AnyAsync(selector);
        public async Task<IList<Empleado>> GetAll(Expression<Func<Empleado,bool>> selector)
            => await _appDbContext.Empleados.Where(selector).ToListAsync();
        public async Task<bool> Remove(int id)
        {
            try
            {
                var current = await Get(id);
                if (current == null)
                {
                    return true;
                }

                var entry = _appDbContext.Empleados.Remove(current);

                await _appDbContext.SaveChangesAsync();

                return ! await Exist(x=>x.Id==id);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
