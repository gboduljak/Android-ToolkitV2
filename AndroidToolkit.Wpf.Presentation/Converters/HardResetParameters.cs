using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Data;
using MahApps.Metro.Controls;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class HardResetParameters : EntityBase, IDataErrorInfo
    {

        private string _text;
        private string _text2;
        private string _text3;
        private string _text4;

        private bool _bool = true;

        public TextBlock Context { get; set; }

        [Required(ErrorMessage = @"Please select boot image.")]
        public string Text
        {
            get { return _text; }
            set
            {
                if (this._text != value)
                {
                    NotifyPropertyChanging();
                    this._text = value;
                    NotifyPropertyChanged();

                }
            }
        }

        [Required(ErrorMessage = @"Please select system image.")]
        public string Text2
        {
            get { return _text2; }
            set
            {
                if (this._text2 != value)
                {
                    NotifyPropertyChanging();
                    this._text2 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Text3
        {
            get { return _text3; }
            set
            {
                if (this._text3 != value)
                {
                    NotifyPropertyChanging();
                    this._text3 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Text4
        {
            get { return _text4; }
            set
            {
                if (this._text4 != value)
                {
                    NotifyPropertyChanging();
                    this._text4 = value;
                    NotifyPropertyChanged();
                }
            }
        }

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

        public Flyout Flyout { get; set; }

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
