using System;
using System.Linq;
using System.Web.Mvc;

namespace FARApplication.Web.Utility
{
    public static class EnumToSelectList
    {

        //public static SelectListItem ToSelectList<TEnum>(this TEnum obj)
        //   where TEnum : struct, IComparable, IFormattable, IConvertible // correct one
        //{

        //    return new SelectListItem(Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(v => new SelectListItem
        //    {
        //        Text = v.ToString(),
        //        Value = v.ToString("d",null)
        //    }).ToList());

        //}
    }
}
