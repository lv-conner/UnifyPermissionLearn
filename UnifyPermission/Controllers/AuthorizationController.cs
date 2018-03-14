using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UnifyPermission.Filter;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnifyPermission.Controllers
{
    [ActionPermission]
    public abstract class AuthorizationController : Controller
    {
    }
}
