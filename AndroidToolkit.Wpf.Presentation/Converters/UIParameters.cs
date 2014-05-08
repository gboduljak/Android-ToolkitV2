using System;
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
    public class UIParameters : EntityBase, IDataErrorInfo
    {
        private bool _bool;

        public Flyout Flyout { get; set; }

        public TextBlock Context { get; set; }

        public TextBox Context2 { get; set; }

        public bool Bool
        {
            get { return _bool; }
            set
            {
                if (this._bool != value)
                {
                    NotifyPropertyChanging();
                    this._bool = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Target { get; set; }


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
