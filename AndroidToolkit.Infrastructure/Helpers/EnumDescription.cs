using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Helpers
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field,
    AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;
        public string Description
        {
            get
            {
                return this.description;
            }
        }
        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }
}
