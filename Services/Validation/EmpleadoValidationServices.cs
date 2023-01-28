using Data.Model;
using Services.Services;
using Shared.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Helpers.Factory;
namespace Services.Validation
{
    public static class EmpleadoValidationServices
    {
        public static async Task<List<KeyValuePair<string, List<string>>>> ValidateEmpleadoPostModel (this IEmpleadoServices validationServices, EmpleadoEditModel model )
        {
            List<KeyValuePair<string, List<string>>> validations = new List<KeyValuePair<string, List<string>>>();
            if (!await validationServices.Exist(x => x.Id == model.Id))
            {
                validations.Add(GetValidationKVEntry("Id", new List<string>() { "No Existe el usuario" }));

            }
            PhoneValidation phoneValidation = new PhoneValidation("El formato de telefono debe ser  (xxx) xxxx-xxxx");
            if(!phoneValidation.IsValid(model.Telefono))
            {
                validations.Add(GetValidationKVEntry("Telefono", new List<string>() { "El formato de telefono debe ser  (xxx) xxxx-xxxx" }));
            }

            if ((await validationServices.GetAll(x => x.Correo.Trim().ToLower() == model.Correo.Trim().ToLower() && x.Id != model.Id)).Count > 0)
            {
                validations.Add(GetValidationKVEntry("Correo", new List<string>() { "ya existe este Correo" }));
            }

            if ((await validationServices.GetAll(x => x.Telefono.Trim() == model.Telefono.Trim() && x.Id != model.Id)).Count > 0)
            {
                validations.Add(GetValidationKVEntry("Telefono", new List<string>() { "ya existe este Telefono" }));
            }

            return validations;
        }
    }
}
