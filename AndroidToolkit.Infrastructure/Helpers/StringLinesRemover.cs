using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Helpers
{
    public static class StringLinesRemover
    {
        public static string FitString(string input)
        {
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(Environment.NewLine, lines); 
        }

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

        public static string RemoveCmdData(string input)
        {
            String newText = String.Empty;
            List<String> lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            lines.RemoveAll(str => str.ToLower().Contains("androidtoolkit"));
            lines.RemoveAll(str => str.ToLower().Contains("mic"));
            lines.RemoveAll(str => str.ToLower().Contains("daemon"));
            lines.ForEach(str => newText += str + Environment.NewLine);
            return newText;
        }
    }
}
