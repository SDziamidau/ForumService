using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;
using System.Net;
using ForumRepository;

namespace ForumService
{
    public class ForumService : IForumService
    {
        private IForumRepository repository;
        public ForumService()
        {
            repository = new ForumXmlRepository();
        }

        public ForumService(IForumRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Post> GetPosts()
        {
            IEnumerable<ForumRepository.IPost> posts = (IEnumerable<ForumRepository.IPost>)this.repository.GetAllPosts();

            if (object.ReferenceEquals(null, posts))
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            //TODO: add any adapter/controller for conversion
            List<Post> result = new List<Post>();
            foreach(ForumRepository.Post p in posts)
            {
                result.Add(new Post(){Id = p.Id, Body = p.Body});
            }

            return result;
        }
        public Post GetPost(string id)
        {
            ForumRepository.Post p = (ForumRepository.Post)this.repository.GetPost(int.Parse(id));            

            if (object.ReferenceEquals(null, p))
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            //TODO: add any adapter/controller for conversion
            Post result = new Post() { Id = p.Id, Body = p.Body };

            return result;
        }
        public bool AddPost(string body)
        {
            if (!this.repository.AddPost(new ForumRepository.Post(0,body)))
                throw new WebFaultException(HttpStatusCode.BadRequest);

            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;

            return true;
        }
        public bool DeletePost(string id)
        {
            if (!this.repository.DeletePost(int.Parse(id)))
                throw new WebFaultException(HttpStatusCode.BadRequest);

            return true;
        }
        public IEnumerable<Comment> GetComments()
        {
            IEnumerable<ForumRepository.IComment> comments = (IEnumerable<ForumRepository.IComment>)this.repository.GetAllComments();

            if (object.ReferenceEquals(null, comments))
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            //TODO: add any adapter/controller for conversion
            List<Comment> result = new List<Comment>();
            foreach (ForumRepository.Comment c in comments)
            {
                result.Add(new Comment() { Id = c.Id, PostId = c.PostId, Body = c.Body });
            }

            return result;
        }
        public Comment GetComment(string id)
        {
            ForumRepository.Comment c = (ForumRepository.Comment)this.repository.GetComment(int.Parse(id));

            if (object.ReferenceEquals(null, c))
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }

            //TODO: add any adapter/controller for conversion
            Comment result = new Comment() { Id = c.Id, PostId = c.PostId, Body = c.Body };

            return result;
        }
        public bool AddComment(string postId, string body)
        {
            if (!this.repository.AddComment(new ForumRepository.Comment(0, int.Parse(postId), body)))
                throw new WebFaultException(HttpStatusCode.BadRequest);

            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Created;

            return true;
        }
        public bool DeleteComment(string id)
        {
            if (!this.repository.DeleteComment(int.Parse(id)))
                throw new WebFaultException(HttpStatusCode.BadRequest);

            return true;
        }

    }
}
