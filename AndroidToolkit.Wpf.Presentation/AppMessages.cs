using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace AndroidToolkit.Wpf.Presentation
{
    public class AppMessages
    {
        public static class CustomerIsValid
        {
            public static void Send(bool argument)
            {
                Messenger.Default.Send<bool>(argument);
            }

            public static void Register(object recipient, Action<bool> action)
            {
                Messenger.Default.Register(recipient, action);
            }
        }
    }
}
