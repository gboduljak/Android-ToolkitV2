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
        [STAThread]
        public static string ShowDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog {Title = "Choose", Multiselect = false};
            DialogResult result = dialog.ShowDialog();
            return dialog.FileName;
        }

        public static string ShowDialog(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog {Multiselect = false, Filter = filter, Title = "Choose"};
            DialogResult result = dialog.ShowDialog();
            return dialog.FileName;
        }
    }
}
