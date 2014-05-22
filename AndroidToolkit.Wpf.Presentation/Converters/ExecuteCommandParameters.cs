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
    public class ExecuteCommandParameters : EntityBase, IDataErrorInfo, IDisposable
    {
        private string _text;
        private string _text2;
        private string _text3;
        private string _text4;
        private string _text5;
        private string _text6;
        private string _text7;
        private string _text8;
        private string _text9;
        private string _text10;
        private bool _bool;

        public TextBlock Context { get; set; }


        [StringLength(100, MinimumLength = 3, ErrorMessage = @"The command text must be between 3 and 100 characters long.")]
        [Required(ErrorMessage = @"Please enter the command text")]
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

   
        public string Text6
        {
            get { return _text6; }
            set
            {
                if (this._text6 != value)
                {
                    NotifyPropertyChanging();
                    this._text6 = value;
                    NotifyPropertyChanged();
                }
            }
        }

    
        public string Text7
        {
            get { return _text7; }
            set
            {
                if (this._text7 != value)
                {
                    NotifyPropertyChanging();
                    this._text7 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Text8
        {
            get { return _text8; }
            set
            {
                if (this._text8 != value)
                {
                    NotifyPropertyChanging();
                    this._text8 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Text9
        {
            get { return _text9; }
            set
            {
                if (this._text9 != value)
                {
                    NotifyPropertyChanging();
                    this._text9 = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Text10
        {
            get { return _text10; }
            set
            {
                if (this._text10 != value)
                {
                    NotifyPropertyChanging();
                    this._text10 = value;
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

        public void Dispose()
        {
            this.Target = null;
            this.Context = null;
            this.Text = null;
            this.Text2 = null;
            this.Text3 = null;
            this.Text4 = null;
            this.Text5 = null;
            this.Text6 = null;
            this.Text7 = null;
            this.Text8 = null;
            this.Text9 = null;
        }

        ~ExecuteCommandParameters()
        {
            Dispose();
        }
    }
}
