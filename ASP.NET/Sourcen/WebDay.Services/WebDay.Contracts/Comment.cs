using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebDay.Contracts
{
    public class BlogComment
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
