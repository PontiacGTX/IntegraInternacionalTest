using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class RootHelper
    {
        IWebHostEnvironment _hostEnvironment;

        public RootHelper(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public string GetImagePath(int id, string filename) => $"{_hostEnvironment.WebRootPath}{Path.DirectorySeparatorChar}img{Path.DirectorySeparatorChar}{id}{Path.DirectorySeparatorChar}{filename}";
        public string GetImageUserPath(int id) => $"{_hostEnvironment.WebRootPath}{Path.DirectorySeparatorChar}img{Path.DirectorySeparatorChar}{id}{Path.DirectorySeparatorChar}";

        public string GetImagePath(string relative) => $"{_hostEnvironment.WebRootPath}{relative}";
        public string GetRelativeImagePath(string path)
        {

            int begin = path.IndexOf($"{Path.DirectorySeparatorChar}img");
            int end = path.Length;
            var subtr = path.Substring(begin, end - begin);
            return subtr;
        }
    }
}
