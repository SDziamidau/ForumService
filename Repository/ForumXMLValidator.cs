using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Linq;
using System.IO;

namespace ForumRepository
{
    public class ForumXMLValidator: IValidator
    {
        string postsDBPath = AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\PostsDB.xml";
        string commentsDBPath = AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\CommentsDB.xml";

        private XDocument xmlPostsData;
        private XDocument xmlCommentsData;

        public ForumXMLValidator()
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
        }

        public ForumXMLValidator(XDocument xmlPostsData, XDocument xmlCommentsData)
        {
            this.xmlPostsData = xmlPostsData;
            this.xmlCommentsData = xmlCommentsData;
        }
        
    
        public bool ValidatePost(IPost post)
        {
            //TODO: add post elements verification
            return true;
        }
        public bool ValidateComment(IComment comment)
        {
            if (object.ReferenceEquals(null, xmlPostsData.Root.Elements("post").Where(p => (int)p.Element("id") == comment.PostId).FirstOrDefault()))
                return false;

            return true;
        }
    }
}
