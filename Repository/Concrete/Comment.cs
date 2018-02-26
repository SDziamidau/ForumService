using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumRepository
{
    public class Comment : IComment
    {
        public Comment(int id, int postId, string body)
        {
            this.Id = id;
            this.PostId = postId;
            this.Body = body;
        }
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Body { get; set; }
        
    }
}
