using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tp5.Models;

namespace Tp5.Areas.Admin.ViewModels
{
    public class CreateEditMenuViewModel
    {
        public Menu Menu { get; set; }
        public Menu[] Menus { get; set; }
    }
}
