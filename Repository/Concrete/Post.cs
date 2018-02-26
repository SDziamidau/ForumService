using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumRepository
{
    public class Post: IPost
    {
        public Post(int id, string body)
        {
            this.Id = id;
            this.Body = body;
        }
        public int Id { get; set; }        
        public string Body { get; set; }
    }
}
