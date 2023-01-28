using Data;
using Data.Model;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
    public class EntityHelper
    {
        private IServiceProvider _provider;
        private RootHelper _rootHelper;

        public EntityHelper(RootHelper rootHelper)
        {
            _rootHelper = rootHelper;
        }

        public string UpdateEmpleadoFoto(Empleado empleado,EmpleadoEditModel empleadoEditModel)
        {
            var nombreActualFoto = Path.GetFileName(empleado.Foto);
            var nuevoNombre = empleadoEditModel.File?.FileName;
            var path = "";
            if(nombreActualFoto ==nuevoNombre && empleadoEditModel.File!=null && empleadoEditModel.File.Length>0)
            {
                path = _rootHelper.GetImagePath(empleado.Id, nombreActualFoto);
                if (File.Exists(path))
                {
                    
                    File.Delete(path);

                }
            }
            bool changed = false;
            if(empleadoEditModel.File?.Length>0)
            {
               
                var filename =empleadoEditModel.File.FileName;
                path = _rootHelper.GetImagePath(empleado.Id, filename);
                empleadoEditModel.File.SaveFile(path);
                changed = true;
                path = _rootHelper.GetRelativeImagePath(path);
            }
            return changed ? path : empleado.Foto;
        }



    }
}
