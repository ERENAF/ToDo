using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.TaskClass
{
    public enum Category
    {
        None,
        Home,
        Job,
        Private_Life,
        Education,
        Other
    }
    public static class CategoryExtra
    {
        public static Color GetCategoryColor(this Category category)
        {
            return category switch
            {
                Category.Home => Color.Blue,
                Category.Job => Color.Green,
                Category.Private_Life => Color.Red,
                Category.Education => Color.Green,
                Category.Other => Color.Orange,
                Category.None => Color.Yellow,
                _ => Color.White
            };
        }
    }
}
