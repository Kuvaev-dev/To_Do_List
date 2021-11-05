using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    // таск
    public class ToDoListItem
    {
        public int Id { get; set; }  // айди таска

        public DateTime AddDate { get; set; }  // дата добавления таска

        // проверка тайтла таска на корректность
        [Required]
        [MinLength(2, ErrorMessage = "Компонент должен содержать не менее 2 аргментов.")]
        [MaxLength(200, ErrorMessage = "Компонент должен содержать не более 200 аргументов.")]
        public string Title { get; set; }   // и сам тайтл

        public bool IsDone { get; set; }    // закончен таск или нет
    }
}
