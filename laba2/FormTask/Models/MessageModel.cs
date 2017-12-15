using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FormTask.Models
{
    public class MessageModel
    {
        [Required(ErrorMessage = "Необходимо ввести адрес")]
        [EmailAddress(ErrorMessage = "Неверный формат email")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введите тему")]
        [StringLength(50, ErrorMessage = "Тема не должна превышать 50 символов")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Введите сообщение")]
        [StringLength(2000, ErrorMessage = "Сообщение не должно превышать 2000 символов.")]
        public string Text { get; set; }
    }
}
