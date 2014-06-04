using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Helpers
{
    internal static class StringLinesRemover
    {
        public static string RemoveLine(string input, int numberOfLines)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return string.Join(Environment.NewLine, lines.Skip(numberOfLines).ToArray()); 
        }
        public static string ForgetLastLine(string input)
        {
            String newText = String.Empty;
            List<String> lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines.RemoveAll(str => str.Contains("AndroidToolkit"));
            lines.ForEach(str => newText += str + Environment.NewLine);
            return newText;
        }
    }
}
