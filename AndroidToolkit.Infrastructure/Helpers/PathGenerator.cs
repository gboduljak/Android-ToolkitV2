using System.IO;

namespace AndroidToolkit.Infrastructure.Helpers
{
    public static class PathGenerator 
    {
        public static string Generate(string path)
        {
            return string.Format(@"""{0}""", path);
        }

        public static string Generate(params string[] paths)
        {
            return string.Format(@"""{0}""", Path.Combine(paths));
        }
    }
}
