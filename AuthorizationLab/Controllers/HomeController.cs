using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationLab.Controllers
{
    //[Authorize(Roles = "Administrator")]
    //[Authorize(Policy = "EmployeeId")]
    //[Authorize(Policy = "Over21Only")]
    [Authorize(Policy = "BuildingEntry")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}