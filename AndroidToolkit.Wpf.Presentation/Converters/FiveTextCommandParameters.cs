using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Baml2006;
using System.Windows.Controls;
using AndroidToolkit.Data;

namespace AndroidToolkit.Wpf.Presentation.Converters
{
    public class FiveTextCommandParameters : EntityBase, IDataErrorInfo
    {

        private string _text;
        private string _text2;
        private string _text3;
        private string _text4;
        private string _text5;

        private bool _bool = true;

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

        public string Text5
        {
            get { return _text5; }
            set
            {
                if (this._text5 != value)
                {
                    NotifyPropertyChanging();
                    this._text5 = value;
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
