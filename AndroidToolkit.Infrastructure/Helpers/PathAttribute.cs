using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidToolkit.Infrastructure.Helpers
{
    [AttributeUsage(AttributeTargets.Property |
     AttributeTargets.Field, AllowMultiple = false)]
    public sealed class PathAttribute : ValidationAttribute
    {
        //private readonly string _path;
        //public PathAttribute(string path)
        //{
        //    _path = path;
        //}
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var property = validationContext.ObjectType.GetProperty(_path);
            //if (property == null)
            //{
            //    return new ValidationResult(
            //        string.Format("Unknown property: {0}", _path)
            //    );
            //}
            //var otherValue = property.GetValue(validationContext.ObjectInstance, null);
            return !Directory.Exists(value.ToString()) ? new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName)) : null;
        }
    }
}
