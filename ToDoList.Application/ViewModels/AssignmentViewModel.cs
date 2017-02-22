using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.ViewModels
{
    public class AssignmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Attachment { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public string UserId { get; set; }
    }
}
