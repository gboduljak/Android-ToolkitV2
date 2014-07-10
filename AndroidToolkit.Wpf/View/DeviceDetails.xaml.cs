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

            HasRoot = hasRoot;
            NoRoot = noRoot;
            DeviceName = name;
            Codename = codename;
            Manufacturer = manufacturer;
            AndroidOS = androidOS;
            AndroidOSCode = androidOSCode;
            BuildProp = buildprop;
        }

        #region Dependency Properties

        public static readonly DependencyProperty HasRootProperty = DependencyProperty.Register(
            "HasRoot", typeof(Rectangle), typeof(DeviceDetails), new PropertyMetadata(default(TextBlock)));

        public static readonly DependencyProperty NoRootProperty = DependencyProperty.Register(
            "NoRoot", typeof(Rectangle), typeof(DeviceDetails), new PropertyMetadata(default(TextBlock)));

        public static readonly DependencyProperty DeviceNameProperty = DependencyProperty.Register(
            "DeviceName", typeof(TextBlock), typeof(DeviceDetails), new PropertyMetadata(default(TextBlock)));

        public static readonly DependencyProperty CodenameProperty = DependencyProperty.Register(
            "Codename", typeof(TextBlock), typeof(DeviceDetails), new PropertyMetadata(default(TextBlock)));

        public static readonly DependencyProperty ManufacturerProperty = DependencyProperty.Register(
            "Manufacturer", typeof(TextBlock), typeof(DeviceDetails), new PropertyMetadata(default(TextBlock)));

        public static readonly DependencyProperty AndroidOSProperty = DependencyProperty.Register(
            "AndroidOS", typeof(TextBlock), typeof(DeviceDetails), new PropertyMetadata(default(TextBlock)));

        public static readonly DependencyProperty AndroidOSCodeProperty = DependencyProperty.Register(
            "AndroidOSCode", typeof(TextBlock), typeof(DeviceDetails), new PropertyMetadata(default(TextBlock)));

        public static readonly DependencyProperty BuildPropProperty = DependencyProperty.Register(
            "BuildProp", typeof(TextBox), typeof(DeviceDetails), new PropertyMetadata(default(TextBox)));

        #endregion

        #region Properties

        public Rectangle HasRoot
        {
            get { return (Rectangle)GetValue(HasRootProperty); }
            set { SetValue(HasRootProperty, value); }
        }

        public Rectangle NoRoot
        {
            get { return (Rectangle)GetValue(NoRootProperty); }
            set { SetValue(NoRootProperty, value); }
        }

        public TextBlock DeviceName
        {
            get { return (TextBlock)GetValue(DeviceNameProperty); }
            set { SetValue(DeviceNameProperty, value); }
        }

        public TextBlock Codename
        {
            get { return (TextBlock)GetValue(CodenameProperty); }
            set { SetValue(CodenameProperty, value); }
        }

        public TextBlock Manufacturer
        {
            get { return (TextBlock)GetValue(ManufacturerProperty); }
            set { SetValue(ManufacturerProperty, value); }
        }

        public TextBlock AndroidOS
        {
            get { return (TextBlock)GetValue(AndroidOSProperty); }
            set { SetValue(AndroidOSProperty, value); }
        }

        public TextBlock AndroidOSCode
        {
            get { return (TextBlock)GetValue(AndroidOSCodeProperty); }
            set { SetValue(AndroidOSCodeProperty, value); }
        }

        public TextBox BuildProp
        {
            get { return (TextBox)GetValue(BuildPropProperty); }
            set { SetValue(BuildPropProperty, value); }
        }
        #endregion

        ~DeviceDetails()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            HasRoot = null;
            NoRoot = null;
            DeviceName = null;
            Codename = null;
            Manufacturer = null;
            AndroidOS = null;
            AndroidOSCode = null;
            BuildProp = null;
            GC.Collect();
            GC.SuppressFinalize(this);
            MemoryManager.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }
    }
}
