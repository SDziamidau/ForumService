using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.IO;
using System.Collections.Generic;
using System.Linq;
using ForumRepository;


namespace ForumTests
{
    [TestClass]
    public class XmlRepositoryTests
    {       

        string postBody1 = "First Post Body";
        string postBody2 = "Second Post Body";

        string commentBody1 = "First Comment Body";
        string commentBody2 = "Second Comment Body";

        [TestMethod]
        public void RepoConstructorWithParamsTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }

            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            Assert.IsNotNull(xmlRepo);
            Assert.IsTrue(File.Exists("\\XML_Data\\PostsDB.xml"));
            Assert.IsTrue(File.Exists("\\XML_Data\\CommentsDB.xml"));
        }

        [TestMethod]
        public void RepoConstructorWithValidatorTest()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\PostsDB.xml"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\PostsDB.xml");
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository(new ForumXMLValidator());
            Assert.IsNotNull(xmlRepo);
            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\PostsDB.xml"));
            Assert.IsTrue(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\XML_Data\\CommentsDB.xml"));
        }

        [TestMethod]
        public void AddPostTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            Assert.IsTrue(xmlRepo.AddPost(new Post(1, postBody1)));
            Assert.IsTrue(xmlRepo.AddPost(new Post(2, postBody2)));

            List<IPost> posts = xmlRepo.GetAllPosts().ToList<IPost>();
            Assert.AreEqual(posts.Count, 2);
        }

        [TestMethod]
        public void GetPostTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            xmlRepo.AddPost(new Post(1, postBody1));
            xmlRepo.AddPost(new Post(2, postBody2));

            IPost post = xmlRepo.GetPost(1);
          
            Assert.IsNotNull(post);
            Assert.AreEqual<string>(post.Body, postBody1);
            Assert.AreEqual<string>(xmlRepo.GetPost(2).Body, postBody2);
            Assert.IsNull(xmlRepo.GetPost(3));

        }

        [TestMethod]
        public void GetAllPostsTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            xmlRepo.AddPost(new Post(1, postBody1));
            xmlRepo.AddPost(new Post(2, postBody2));

            List<IPost> posts = xmlRepo.GetAllPosts().ToList<IPost>();

            Assert.IsNotNull(posts);
            Assert.AreEqual(posts.Count, 2);
        }

        [TestMethod]
        public void DeletePostTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            xmlRepo.AddPost(new Post(1, postBody1));
            xmlRepo.AddPost(new Post(2, postBody2));

            Assert.IsTrue(xmlRepo.DeletePost(1));

            List<IPost> posts = xmlRepo.GetAllPosts().ToList<IPost>();
            Assert.AreEqual(posts.Count, 1);

            Assert.IsNotNull(xmlRepo.GetPost(2));
            Assert.IsNull(xmlRepo.GetPost(1));
        }


        [TestMethod]
        public void AddCommentTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            Assert.IsTrue(xmlRepo.AddComment(new Comment(1, 1, commentBody1)));
            Assert.IsTrue(xmlRepo.AddComment(new Comment(2, 1, commentBody2)));

            List<IComment> comments = xmlRepo.GetAllComments().ToList<IComment>();
            Assert.AreEqual(comments.Count, 2);
        }

        [TestMethod]
        public void GetCommentTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            xmlRepo.AddComment(new Comment(1, 1, commentBody1));
            xmlRepo.AddComment(new Comment(2, 1, commentBody2));

            IComment comment = xmlRepo.GetComment(1);

            Assert.IsNotNull(comment);
            Assert.AreEqual<string>(comment.Body, commentBody1);
            Assert.AreEqual<string>(xmlRepo.GetComment(2).Body, commentBody2);
            Assert.IsNull(xmlRepo.GetComment(3));
        }

        [TestMethod]
        public void GetAllCommentsTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            xmlRepo.AddComment(new Comment(1, 1, commentBody1));
            xmlRepo.AddComment(new Comment(2, 1, commentBody2));

            List<IComment> comments = xmlRepo.GetAllComments().ToList<IComment>();

            Assert.IsNotNull(comments);
            Assert.AreEqual(comments.Count, 2);
        }

        [TestMethod]
        public void DeleteCommentTest()
        {
            if (File.Exists("\\XML_Data\\PostsDB.xml"))
            {
                File.Delete("\\XML_Data\\PostsDB.xml");
                File.Delete("\\XML_Data\\CommentsDB.xml");
            }
            IForumRepository xmlRepo = new ForumXmlRepository("\\XML_Data\\PostsDB.xml", "\\XML_Data\\CommentsDB.xml");
            xmlRepo.AddComment(new Comment(1, 1, commentBody1));
            xmlRepo.AddComment(new Comment(2, 1, commentBody2));

            Assert.IsTrue(xmlRepo.DeleteComment(1));

            List<IComment> comments = xmlRepo.GetAllComments().ToList<IComment>();
            Assert.AreEqual(comments.Count, 1);

            Assert.IsNotNull(xmlRepo.GetComment(2));
            Assert.IsNull(xmlRepo.GetComment(1));
        }
    }
}
