using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Data;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class FileOpsParameters : EntityBase, IDataErrorInfo
    {
        private bool _bool;
        private string _text;
        private string _text2;

        public TextBlock Context { get; set; }

        [Required(ErrorMessage = @"Please fill this field.")]
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

        [Required(ErrorMessage = @"Please fill this field.")]
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

        public string Target { get; set; }

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
