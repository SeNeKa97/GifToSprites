using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GifToSpritesClasses
{
    public static class PathHelper
    {
        public static string GetFullPathWithoutExtension(string path) {
            return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), System.IO.Path.GetFileNameWithoutExtension(path));
        }
    }
}
