using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Data;
using MahApps.Metro.Controls;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class RootParameters : EntityBase, IDataErrorInfo
    {
        #region Fields

        private bool _superUser;
        private bool _superSU;
        private MetroWindow _window;
        private bool _createNoWindow;
        private TextBlock _context;
        private string _target;

        #endregion

        #region Properties

        public bool SuperUser
        {
            get { return _superUser; }
            set
            {
                if (_superUser != value)
                {
                    NotifyPropertyChanging();
                    _superUser = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool SuperSU
        {
            get { return _superSU; }
            set
            {
                if (_superSU != value)
                {
                    NotifyPropertyChanging();
                    _superSU = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public MetroWindow Window
        {
            get { return _window; }
            set
            {
                if (_window != value)
                {
                    NotifyPropertyChanging();
                    _window = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool CreateNoWindow
        {
            get { return _createNoWindow; }
            set
            {
                if (_createNoWindow != value)
                {
                    NotifyPropertyChanging();
                    _createNoWindow = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public TextBlock Context
        {
            get { return _context; }
            set
            {
                if (_context != value)
                {
                    NotifyPropertyChanging();
                    _context = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Target
        {
            get { return _target; }
            set
            {
                if (_target != value)
                {
                    NotifyPropertyChanging();
                    _target = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { return base.Validate(columnName); }

        }

        #endregion
    }
}
