using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedCardWeb.Areas._Public
{
    public class MyVerefiLoginAttribute : ValidationAttribute
    {
        public MyVerefiLoginAttribute()
        {
        }

        public override bool Match(object obj)
        {
            return base.Match(obj);
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}
