using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

using ForumRepository;

namespace ForumService
{
    [DataContract]
    public class Post: IPost
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Body { get; set; }
    }
}