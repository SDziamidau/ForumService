using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumRepository
{
    public interface IComment
    {
        int Id { get; set; }
        int PostId { get; set; }
        string Body { get; set; }
    }
}
