using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers
{
    public static class FileHelper
    {

        public static void SaveFile(this IFormFile file, string path)
        {
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

        }

        public static  void CreateFolder(string path)
        {
            int begin = 0;
            int end = path.LastIndexOf($"{Path.DirectorySeparatorChar}");
            path = path.Substring(begin, end);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
    }
}
