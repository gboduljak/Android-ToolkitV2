using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndroidToolkit.Infrastructure.Helpers
{
    public static class FileDialog
    {

        public static string ShowDialog(bool multiselect)
        {
            StringBuilder files = new StringBuilder();
            OpenFileDialog dialog = new OpenFileDialog { Title = "Choose", Multiselect = multiselect };
            DialogResult result = dialog.ShowDialog();
            foreach (string file in dialog.FileNames)
            {
                if (files.Length == 0)
                {
                    files.AppendLine(file.Trim() + ",");
                }
                else
                {
                    files.AppendLine("\n" + file.Trim() + ",");
                }
            }
            return files.ToString();
        }

        public static string ShowDialog(string filter, bool multiselect)
        {
            StringBuilder files = new StringBuilder();
            OpenFileDialog dialog = new OpenFileDialog { Title = "Choose", Multiselect = multiselect, Filter = filter };
            DialogResult result = dialog.ShowDialog();
            foreach (string file in dialog.FileNames)
            {
                if (files.Length == 0)
                {
                    files.AppendLine(file.Trim());
                }
                else
                {
                    files.AppendLine("\n" + file.Trim());
                }
            }
            return files.ToString();
        }
    }
}
