using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ToDoList.Domain.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Attachment { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }        

        public string UserId { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}