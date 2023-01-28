using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ModelStateDictionaryHelper
    {
        public static void RemoveValidationFor(this ModelStateDictionary modelState, string key)
        {
            if (modelState.Keys.Any(x => x.Equals(key)))
            {
                modelState.Remove(key);
            }
        }
    }
}
