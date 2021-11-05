using Dapper.Contrib.Extensions;        // удобная штука)
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using ToDoList.Helpers;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // в событии индекса использем вид-модель
        public IActionResult Index()
        {
            ToDoListViewModel viewModel = new ToDoListViewModel();
            return View("Index", viewModel);
        }

        // создаём событие редактирования тасков
        public IActionResult Edit(int id)
        {
            ToDoListViewModel viewModel = new ToDoListViewModel();
            viewModel.EditableItem = viewModel.TodoItems.FirstOrDefault(x => x.Id == id);
            return View("Index", viewModel);
        }

        // создаём событие удаления тасков
        public IActionResult Delete(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ToDoListItem item = db.Get<ToDoListItem>(id);
                if (item != null)
                    db.Delete(item);
                return RedirectToAction("Index");
            }
        }

        // создаём событие создания/обновления тасков, принимающее
        // дату таска
        public IActionResult CreateUpdate(ToDoListViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = DbHelper.GetConnection())
                {
                    if (viewModel.EditableItem.Id <= 0)
                    {
                        viewModel.EditableItem.AddDate = DateTime.Now;
                        db.Insert<ToDoListItem>(viewModel.EditableItem);
                    }
                    else
                    {
                        ToDoListItem dbItem = db.Get<ToDoListItem>(viewModel.EditableItem.Id);
                        var result = TryUpdateModelAsync<ToDoListItem>(dbItem, "EditableItem");
                        db.Update<ToDoListItem>(dbItem);
                    }
                }
                return RedirectToAction("Index");
            }
            else
                return View("Index", new ToDoListViewModel());
        }

        // создаём событие проверки на завершение таска
        public IActionResult ToggleIsDone(int id)
        {
            using (var db = DbHelper.GetConnection())
            {
                ToDoListItem item = db.Get<ToDoListItem>(id);
                if (item != null)
                {
                    item.IsDone = !item.IsDone;
                    db.Update<ToDoListItem>(item);
                }
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // добавляем экшн описания
        public IActionResult About()
        {
            // во вьюдата добавляем немножко описания
            ViewData["Message"] = "Страница с описанием веб-приложения.";

            return View();
        }

        // добавляем экшн контактной информации 
        public IActionResult Contact()
        {
            ViewData["Message"] = "Страница контактов.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
