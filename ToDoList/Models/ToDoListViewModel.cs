using Dapper;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Helpers;

namespace ToDoList.Models
{
    public class ToDoListViewModel
    {
        // конструктор будет получать наши таски
        public ToDoListViewModel()
        {
            using (var db = DbHelper.GetConnection())
            {
                this.EditableItem = new ToDoListItem();
                this.TodoItems = db.Query<ToDoListItem>("EXEC [dbo].[GetItems]").ToList();
            }
        }

        // сами таски
        public List<ToDoListItem> TodoItems { get; set; }
        // редактируемый таск
        public ToDoListItem EditableItem { get; set; }
    }
}
