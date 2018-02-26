using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ForumRepository
{
    public interface IForumRepository
    {
        bool AddPost(IPost post);
        IPost GetPost(int id);
        IEnumerable <IPost> GetAllPosts();
        bool DeletePost(int id);

        bool AddComment(IComment comment);
        IComment GetComment(int id);
        IEnumerable<IComment> GetAllComments();
        bool DeleteComment(int id);
    }
}
