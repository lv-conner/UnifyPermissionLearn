using CoreMVC.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Component
{
    public class MenuComponent:ViewComponent
    {
        private readonly IGetName getName;
        public MenuComponent(IGetName getName)
        {
            this.getName = getName;
        }
    }
}
