using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WebDay.Contracts
{
    [DataContract]
    public class BlogPost
    {
        public int Id { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public string[] Tags { get; set; }

        public BlogComment[] Comments { get; set; }
    }

}
