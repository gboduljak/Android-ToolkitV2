using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AndroidToolkit.Infrastructure
{
    public class Command : INotifyPropertyChanged
    {
        public Command()
        {
            
        }

        public Command(string text)
        {
            this.Text = text;
        }
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                if (value != this.Text)
                {
                    this._text = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
