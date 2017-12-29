using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApp.Controllers
{
    public class TodoController : Controller
    {
        // GET: /Todo
        public string Index()
        {
            return "This is my default action...";
        }
        //
        //GET: /Todo/Welcome
        public string Welcome()
        {
            return "This is the welcome action method...";
        }
    }
}