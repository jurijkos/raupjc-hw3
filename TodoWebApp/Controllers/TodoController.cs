
using System;

using System.Collections.Generic;

using System.Linq;

using System.Net;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TodoRepository;
using TodoWebApp.Models;
using TodoWebApp.Models.TodoViewModels;

namespace TodoWebApp.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController(ITodoRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var activeTodos = _repository.GetActive(await GetCurrentUserIdAsync());
            return View(activeTodos);
        }



        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var todoItem = new TodoItem(model.Text, await GetCurrentUserIdAsync());
                _repository.Add(todoItem);
                return RedirectToAction("Index");
            }
            return View();
        }



        public async Task<IActionResult> Completed()
        {
            var completedTodos = _repository.GetCompleted(await GetCurrentUserIdAsync());
            return View(completedTodos);
        }



        public async Task<IActionResult> CompleteTodo(Guid todoId)
        {
            _repository.MarkAsCompleted(todoId, await GetCurrentUserIdAsync());
            return RedirectToAction("Index");
        }



        private async Task<Guid> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return new Guid(user.Id);
        }
    }
}