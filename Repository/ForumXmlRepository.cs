using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace ForumRepository
{
    public class ForumXmlRepository: IForumRepository
    {
        string postsDBPath = AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\PostsDB.xml";
        string commentsDBPath = AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\CommentsDB.xml";

        private XDocument xmlPostsData;
        private XDocument xmlCommentsData;

        private IValidation validator;

        public ForumXmlRepository()
        {
            if (File.Exists(postsDBPath))
            {
                xmlPostsData = XDocument.Load(postsDBPath);
            }
            else
            {
                string postsDir = postsDBPath.Replace("\\PostsDB.xml", string.Empty);
                if (!Directory.Exists(postsDir)) Directory.CreateDirectory(postsDir);

                xmlPostsData = new XDocument(new XElement("posts"));       
                xmlPostsData.Save(postsDBPath);
            }

            if (File.Exists(commentsDBPath))
            {
                xmlCommentsData = XDocument.Load(commentsDBPath);
            }
            else
            {
                string commentsDir = commentsDBPath.Replace("\\CommentsDB.xml", string.Empty);
                if (!Directory.Exists(commentsDir)) Directory.CreateDirectory(commentsDir);

                xmlCommentsData = new XDocument(new XElement("comments"));
                xmlCommentsData.Save(commentsDBPath);
            }

            this.validator = new ForumXMLValidator(xmlPostsData, xmlCommentsData);
        }

        public ForumXmlRepository(string postsPath, string commentsPath)
        {
            if (!string.IsNullOrEmpty(postsPath)) 
                this.postsDBPath = postsPath;          

            if (File.Exists(postsDBPath))
            {
                xmlPostsData = XDocument.Load(postsDBPath);
            }
            else
            {
                string postsDir = postsDBPath.Replace("\\PostsDB.xml", string.Empty);
                if (!Directory.Exists(postsDir)) Directory.CreateDirectory(postsDir);

                xmlPostsData = new XDocument(new XElement("posts"));
                xmlPostsData.Save(postsDBPath);
            }

            if (!string.IsNullOrEmpty(commentsPath)) 
                this.commentsDBPath = commentsPath;

            if (File.Exists(commentsDBPath))
            {
                xmlCommentsData = XDocument.Load(commentsDBPath);
            }
            else
            {
                string commentsDir = commentsDBPath.Replace("\\CommentsDB.xml", string.Empty);
                if (!Directory.Exists(commentsDir)) Directory.CreateDirectory(commentsDir);

                xmlCommentsData = new XDocument(new XElement("comments"));
                xmlCommentsData.Save(commentsDBPath);
            }

            this.validator = new ForumXMLValidator(xmlPostsData, xmlCommentsData); 
        }
        public ForumXmlRepository(IValidation validator)
        {
            this.validator = validator;

            if (File.Exists(postsDBPath))
            {
                xmlPostsData = XDocument.Load(postsDBPath);
            }
            else
            {
                string postsDir = postsDBPath.Replace("\\PostsDB.xml", string.Empty);
                if (!Directory.Exists(postsDir)) Directory.CreateDirectory(postsDir);

                xmlPostsData = new XDocument(new XElement("posts"));
                xmlPostsData.Save(postsDBPath);
            }

            if (File.Exists(commentsDBPath))
            {
                xmlCommentsData = XDocument.Load(commentsDBPath);
            }
            else
            {
                string commentsDir = commentsDBPath.Replace("\\CommentsDB.xml", string.Empty);
                if (!Directory.Exists(commentsDir)) Directory.CreateDirectory(commentsDir);

                xmlCommentsData = new XDocument(new XElement("comments"));
                xmlCommentsData.Save(commentsDBPath);
            }
        }
      
        public bool AddPost(IPost post)
        {
            this.validator.ValidatePost(post);

            int maxPostId = (int)(from p in xmlPostsData.Descendants("post") 
                                  orderby (int) p.Element("id") descending 
                                  select (int)p.Element("id")
                                  ).FirstOrDefault();

            post.Id = maxPostId + 1;

            xmlPostsData.Root.Add(new XElement("post", 
                                                new XElement("id", post.Id), 
                                                new XElement("body", post.Body)
                                              )
                                 );

            xmlPostsData.Save(postsDBPath);
            
            return true;
        }
        public IPost GetPost(int id)
        {
            XElement xmlPost = xmlPostsData.Root.Elements("post").Where(p => (int)p.Element("id") == id).FirstOrDefault();

            if (object.ReferenceEquals(null, xmlPost)) return null;

            return new Post(
                            (int)xmlPost.Element("id"), 
                            xmlPost.Element("body").Value.ToString()
                       );
        }

        public IEnumerable <IPost> GetAllPosts()
        {
            IEnumerable<IPost> posts =  (from p in xmlPostsData.Descendants("post")
                                         select new Post(
                                                        (int)p.Element("id"),
                                                        p.Element("body").Value
                                                    )).ToList<IPost>();
            return posts;
        }
        public bool DeletePost(int id)
        {
            try
            {
                if (object.ReferenceEquals(null, xmlPostsData.Root.Elements("post").Where(p => (int)p.Element("id") == id).FirstOrDefault()))
                    return false;
                xmlPostsData.Root.Elements("post").Where(p => (int)p.Element("id") == id).Remove();
                xmlPostsData.Save(postsDBPath);
            }
            catch { return false; }

            return true;
        }

        public bool AddComment(IComment comment)
        {
            this.validator.ValidateComment(comment);

            int maxCommentId = (int)(from p in xmlCommentsData.Descendants("comment")
                                  orderby (int)p.Element("id") descending
                                  select (int)p.Element("id")
                                  ).FirstOrDefault();

            comment.Id = maxCommentId + 1;

            xmlCommentsData.Root.Add(new XElement("comment",
                                                new XElement("id", comment.Id),
                                                new XElement("postid", comment.PostId),
                                                new XElement("body", comment.Body)
                                              )
                                 );

            xmlCommentsData.Save(commentsDBPath);

            return true;
        }
        public IComment GetComment(int id)
        {
            XElement xmlComment = xmlCommentsData.Root.Elements("comment").Where(с => (int)с.Element("id") == id).FirstOrDefault();

            if (object.ReferenceEquals(null, xmlComment)) return null;

            return new Comment(
                            (int)xmlComment.Element("id"),
                            (int)xmlComment.Element("postid"),
                            xmlComment.Element("body").Value.ToString()
                       );
        }
        public IEnumerable <IComment> GetAllComments()
        {
            IEnumerable<IComment> comments = (from c in xmlCommentsData.Descendants("comment")
                                        select new Comment(
                                                        (int)c.Element("id"),
                                                        (int)c.Element("postid"),
                                                        c.Element("body").Value
                                                    )).ToList<IComment>();
            return comments;
        }
        public bool DeleteComment(int id)
        {
            try
            {
                if (object.ReferenceEquals(null, xmlCommentsData.Root.Elements("comment").Where(с => (int)с.Element("id") == id).FirstOrDefault()))
                    return false;

                xmlCommentsData.Root.Elements("comment").Where(с => (int)с.Element("id") == id).Remove();
                xmlCommentsData.Save(commentsDBPath);
            }
            catch { return false; }

            return true;
        }
    }
}
