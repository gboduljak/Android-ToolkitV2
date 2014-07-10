using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AndroidToolkit.Memory;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace AndroidToolkit.Wpf.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for Error.xaml
    /// </summary>
    public partial class Error : MetroWindow, IDisposable
    {
        public Error()
        {
            InitializeComponent();
            ThemeManager.ChangeAppStyle(this,ThemeManager.Accents.First(x=>x.Name=="Red"),ThemeManager.AppThemes.First(x=>x.Name=="BaseDark"));
        }

        #region Dependency Properties

        public static readonly DependencyProperty ErrorTitleProperty = DependencyProperty.Register(
            "ErrorTitle", typeof(string), typeof(Error));

        public static readonly DependencyProperty ErrorContentProperty = DependencyProperty.Register(
            "ErrorContent", typeof(string), typeof(Error));

        #endregion

        #region Properties

        public string ErrorTitle
        {
            get { return (string)GetValue(ErrorTitleProperty); }
            set { SetValue(ErrorTitleProperty, value); }
        }

        public string ErrorContent
        {
            get { return (string)GetValue(ErrorContentProperty); }
            set { SetValue(ErrorContentProperty, value); }
        }

        #endregion

        #region Tasks

        private Task SaveError(Exception ex)
        {
            return Task.Run(() =>
            {
                string errorLog = string.Format("*** ERROR on {0} ***\n\n{1}\n\n*** ERROR ***\n\n*** INNER EXCEPTION ***\n\n{2}\n\n*** INNER EXCEPTION ***\n\n*** STACK TRACE ***\n\n{3}\n\n*** STACK TRACE ***\n\n",
                DateTime.Now, ex.Message, ex.InnerException, ex.StackTrace);
                FileStream stream = null;
                try
                {
                    stream = new FileStream("ErrorLog.log", FileMode.Append, FileAccess.Write, FileShare.Read);
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        stream = null;
                        writer.Write(errorLog);
                    }
                }
                finally
                {
                    if (stream != null) stream.Dispose();
                }
            });
        }

        #endregion

        
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            MemoryManager.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
        }

        ~Error()
        {
            Dispose();
        }
    }
}
