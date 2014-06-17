using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AndroidToolkit.Infrastructure.Helpers;
using AndroidToolkit.Infrastructure.Tools;
using AndroidToolkit.Memory;
using MahApps.Metro.Controls;
using TextBox = System.Windows.Controls.TextBox;

namespace AndroidToolkit.Wpf.View
{
    /// <summary>
    /// Interaction logic for DeviceDetails.xaml
    /// </summary>
    public partial class DeviceDetails : MetroWindow
    {
        public DeviceDetails()
        {
            InitializeComponent();
            this.ExportTile.Click += (sender, e) =>
            {
                var dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();
                try
                {
                    AdbTools.KillAdb();
                    File.WriteAllText(string.Format("{0}\\{1}.txt", dialog.SelectedPath, "build-prop"), buildprop.Text);
                    using (Toast toast = new Toast("build.prop exported"))
                    {
                        toast.Show();
                    }
                }
                catch (Exception ex)
                {
                    using (Toast toast = new Toast(string.Format("Error:{0}", ex.Message)))
                    {
                        toast.Show();
                    }
                }
            };
        }

        #region Properties

        public Rectangle HasRoot
        {
            get { return hasRoot; }
        }

        public Rectangle NoRoot
        {
            get { return noRoot; }
        }

        public new TextBlock Name
        {
            get { return name; }
        }

        public TextBlock Codename
        {
            get { return codename; }
        }

        public TextBlock Manufacturer
        {
            get { return manufacturer; }
        }

        public TextBlock AndroidOS
        {
            get { return androidOS; }
        }

        public TextBlock AndroidOSCode
        {
            get { return androidOSCode; }
        }

        public TextBox BuildProp
        {
            get { return buildprop; }
        }
        #endregion

        ~DeviceDetails()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            MemoryManager.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }
    }
}
