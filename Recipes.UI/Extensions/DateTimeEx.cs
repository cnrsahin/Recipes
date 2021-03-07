using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipes.UI.Extensions
{
    public static class DateTimeEx
    {
        public static string DateUnderscore(this DateTime dateTime)
        {
            return $"{dateTime.Second}_{dateTime.Minute}_{dateTime.Hour}_{dateTime.Day}_{dateTime.Month}_{dateTime.Year}";
        }
    }
}
