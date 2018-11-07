using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebUI.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property |
                    AttributeTargets.Field, AllowMultiple = false)]
    public sealed class NameNotAdminValidateAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool result = true;
            string name = value as string;

            var adminCheck = new Regex(@"admin(istrator)*", RegexOptions.IgnoreCase);

            result = !(string.IsNullOrEmpty(name) || adminCheck.IsMatch(name));

            return result;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, "Name cannot be \"Admin\"", name);
        }
    }
}