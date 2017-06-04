using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace WebDay.Contracts
{
    [DataContract]
    public class TagCloudItem
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int Weight { get; set; }
    }
}
