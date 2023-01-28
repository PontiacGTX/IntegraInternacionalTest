using Data;
using Data.Model;
using DataAccess.Helpers;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Shared;
using System.Linq.Expressions;

namespace Services.Services
{
    public class EmpleadoServices: IEmpleadoServices
    {
        private IEmpleadoRepository _empleadoRepository;
        private RootHelper _rootHelper;
        private EntityHelper _entityHelper;

        public EmpleadoServices(IEmpleadoRepository empleadoRepository, RootHelper rootHelper, EntityHelper entityHelper)
        {
            _empleadoRepository = empleadoRepository;
            _rootHelper= rootHelper;
            _entityHelper = entityHelper;
        }

        public async Task<Empleado> Get(int id)
            => await _empleadoRepository.Get(id);
        public async Task<Empleado> Add(Empleado empleado,IFormFile file)
        { 
           empleado  = await _empleadoRepository.Add(empleado);
            if (empleado == null)
                return empleado!;

            if (file == null)
                return empleado;

            var path = _rootHelper.GetImagePath(empleado.Id, file.FileName);
            var relative = _rootHelper.GetRelativeImagePath(path);
            var userFolder = _rootHelper.GetImageUserPath(empleado.Id);
            FileHelper.CreateFolder(userFolder);
            file.SaveFile(path);
            empleado.Foto = relative;
            await Update(empleado);


            return empleado;
        }
        public async Task<Empleado> Update(Empleado empleado)
            => await _empleadoRepository.Update(empleado);

        public async Task<Empleado>Update(EmpleadoEditModel empleadoEdit)
        {
            var empleado = await Get(empleadoEdit.Id);
            empleado.Set(empleadoEdit);
            empleado.Foto = _entityHelper.UpdateEmpleadoFoto(empleado, empleadoEdit);
           return await _empleadoRepository.Update(empleado);

        }
        public async Task<bool> Exist(Expression<Func<Empleado,bool>> selector)
            => await _empleadoRepository.Exist(selector);
        public async Task<IList<Empleado>> GetAll()
            => await _empleadoRepository.GetAll();
        public async Task<IList<Empleado>> GetAll(Expression<Func<Empleado,bool>> selector)
            => await _empleadoRepository.GetAll(selector);
        public async Task<bool> Remove(int id)
            =>  await _empleadoRepository.Remove(id);

        public async Task<Empleado> Add(Empleado empleado)
        {
            return await _empleadoRepository.Add(empleado);
        }
    }
}