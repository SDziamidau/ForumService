using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Web;
using System.Net.Http;
using ForumRepository;

namespace ForumService
{
    [ServiceContract]
    public interface IForumService
    {
        [OperationContract]
        [WebGet (ResponseFormat=WebMessageFormat.Json, BodyStyle=WebMessageBodyStyle.Bare, UriTemplate="posts")]
        IEnumerable<Post> GetPosts();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "posts/{id}")]
        Post GetPost(string id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "posts/{body}")]
        bool AddPost(string body);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "DELETE", UriTemplate = "posts/{id}")]
        bool DeletePost(string id);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "comments")]
        IEnumerable<Comment> GetComments();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "comments/{id}")]
        Comment GetComment(string id);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", UriTemplate = "comments/{postid}/{body}")]
        bool AddComment(string postId, string body);

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, Method = "DELETE", UriTemplate = "comments/{id}")]
        bool DeleteComment(string id);

    }
}
