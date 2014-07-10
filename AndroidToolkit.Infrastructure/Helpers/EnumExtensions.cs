using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            EnumDescriptionAttribute[] attributes =
                (EnumDescriptionAttribute[])
                    fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }

        public static IList ToList(this Type type) 
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            return (Enum.GetValues(type).Cast<Enum>().Select(value => new {Text = GetDescription(value)})).ToList();
        }
    }
}
