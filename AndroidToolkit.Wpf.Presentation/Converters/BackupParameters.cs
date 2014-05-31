using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Data;
using AndroidToolkit.Infrastructure.Helpers;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class BackupParameters : EntityBase, IDataErrorInfo
    {
        private bool _bool;
        private string _text;
        private string _text2;

        [Required(ErrorMessage = @"Backup name is required.")]
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
       
        [Required(ErrorMessage = @"Backup location is required.")]
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

        public TextBlock Context { get; set; }

        public ComboBox Context2 { get; set; }

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
