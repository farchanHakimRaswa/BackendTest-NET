using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetTestProject.Models
{
    public class TodoItem
    {
        public long Id { get; set; } // unique id of each todo
        public string Title { get; set; } // is used as title todo
        public string Description { get; set; } // is used as description of each title
        public DateTime DateCreated { get; set; }  // is Used as initial date created of  task todo
        public DateTime? FinishDate { get; set; }   // is Used as Target of finisih task todo
        public long percentComplate { get; set; }
        public bool isComplate { get; set; }

        // Set Default value
        public TodoItem()
        {
            DateCreated = DateTime.Now;
            FinishDate = DateTime.Now;
            percentComplate = 0;
            isComplate = false;
        }
    }
}
