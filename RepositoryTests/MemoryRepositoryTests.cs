using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Repository;

namespace Team42Test.RepositoryTests
{
    [TestFixture]
    public class MemoryRepositoryTests
    {
        public MemoryRepository repo;
        [SetUp]
        public void Setup()
        {
            repo = new();
        }
        [Test]
        public void GetAllUsers_OnDefaultMemoryRepository_ReturnsUsers()
        {
            var users = repo.GetAllUsers();
            Assert.That(users, Is.Not.Empty);
            Assert.That(users, Is.InstanceOf<IEnumerable<IUser>>());
        }
        [Test]
        public void TestGetUser_IdProvided_ReturnsUserWithId()
        {
            var obj = repo.GetUser(0);

            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.InstanceOf<IUser>());
            Assert.That(obj.Name, Is.EqualTo("a"));
        }

        [Test]
        public void TestGetPost_IdProvided_ReturnsPostWithId()
        {
            var obj = repo.GetPost(0);

            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.InstanceOf<IPost>());
            Assert.That(obj.Content, Is.EqualTo("a"));
        }

        [Test]
        public void TestUpdatePost_IdProvided_UpdatesPostWithId()
        {
            var old = repo.GetPost(0);
            var n = new TextPost() { Content = "b" };
            repo.UpdatePost(old, n);
            var obj = repo.GetPost(0);

            Assert.That(obj, Is.Not.Null);
            Assert.That(obj, Is.InstanceOf<IPost>());
            Assert.That(obj.Content, Is.EqualTo("b"));
        }
        [Test]
        public void GetAllCategories_OnDefaultMemoryRepository_ReturnsCategories()
        {
            var categories = repo.GetAllCategories();
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
            Assert.That(categories, Is.Not.Empty);
        }
        [Test]
        public void GetAllQuestions_OnDefaultMemoryRepository_ReturnsQuestions()
        {
            var questions = repo.GetAllQuestions();
            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(questions, Is.Not.Empty);
        }
        [Test]
        public void AddUser_IDNameProvided_AddsUserWithProvidedData()
        {
            var user = new User { ID = 3, Name = "New User" };
            repo.AddUser(user);

            var retrievedUser = repo.GetUser(user.ID);
            Assert.That(retrievedUser, Is.Not.Null);
            Assert.That(retrievedUser, Is.EqualTo(user));
        }

        [Test]
        public void AddPost__IDContentProvided_AddsPostWithProvidedData()
        {
            var post = new TextPost { ID = 4, Content = "New Post" };
            repo.AddPost(post);

            var retrievedPost = repo.GetPost(post.ID);
            Assert.That(retrievedPost, Is.Not.Null);
            Assert.That(retrievedPost, Is.EqualTo(post));
        }
        [Test]
        public void GetAnswersOfUser_UserIdProvided_ReturnsAnswersWithUserId()
        {
            var userId = 0;
            var answers = repo.GetAnswersOfUser(userId);
            Assert.That(answers, Is.Not.Null);
            Assert.That(answers, Is.InstanceOf<IEnumerable<IAnswer>>());
            Assert.That(answers, Is.Not.Empty);
            Assert.That(answers.All(a => a.UserID == userId), Is.True);
        }

        [Test]
        public void GetBadgesOfUser_UserIdProvided_ReturnsBadgesWithUserId()
        {
            var userId = 0;
            var badges = repo.GetBadgesOfUser(userId);
            Assert.That(badges, Is.Not.Null);
            Assert.That(badges, Is.InstanceOf<IEnumerable<IBadge>>());
            Assert.That(badges, Is.Not.Empty);
        }

        [Test]
        public void GetCategoriesModeratedByUser_UserIdProvided_ReturnsCategoriesWithUserId()
        {
            var userId = 0;
            var categories = repo.GetCategoriesModeratedByUser(userId);
            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
            Assert.That(categories, Is.Not.Empty);
        }

        [Test]
        public void GetCategoryByID_CategoryIdProvided_ReturnsCategoryWithId()
        {
            var categoryId = 0;
            var category = repo.GetCategoryByID(categoryId);
            Assert.That(category, Is.Not.Null);
            Assert.That(category, Is.InstanceOf<ICategory>());
            Assert.That(category.ID, Is.EqualTo(categoryId));
        }

        [Test]
        public void GetCommentsOfUser_UserIdProvided_ReturnsCommentsWithUserId()
        {
            var userId = 0;
            var comments = repo.GetCommentsOfUser(userId);
            Assert.That(comments, Is.Not.Null);
            Assert.That(comments, Is.InstanceOf<IEnumerable<IComment>>());
            Assert.That(comments, Is.Not.Empty);
            Assert.That(comments.All(c => c.UserID == userId), Is.True);
        }

        [Test]
        public void GetNotificationsOfUser_UserIdProvided_ReturnsNotificationsWithUserId()
        {
            var userId = 0;
            var notifications = repo.GetNotificationsOfUser(userId);
            Assert.That(notifications, Is.Not.Null);
            Assert.That(notifications, Is.InstanceOf<IEnumerable<INotification>>());
            Assert.That(notifications, Is.Not.Empty);
            Assert.That(notifications.All(n => n.UserID == userId), Is.True);
        }

        [Test]
        public void GetQuestionsOfUser_UserIdProvided_ReturnsQuestionsWithUserId()
        {
            var userId = 0;
            var questions = repo.GetQuestionsOfUser(userId);
            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(questions, Is.Not.Empty);
            Assert.That(questions.All(q => q.UserID == userId), Is.True);
        }

        [Test]
        public void GetReactionsOfPostByPostID_PostIdProvided_ReturnsReactionsWithPostId()
        {
            var postId = 0;
            var reactions = repo.GetReactionsOfPostByPostID(postId);
            Assert.That(reactions, Is.Not.Null);
            Assert.That(reactions, Is.InstanceOf<IEnumerable<IReaction>>());
        }
        [Test]
        public void GetRepliesOfPost_PostIdProvided_ReturnsRepliesWithPostId()
        {
            var postId = 0;
            var replies = repo.GetRepliesOfPost(postId);
            Assert.That(replies, Is.Not.Null);
            Assert.That(replies, Is.InstanceOf<IEnumerable<IPost>>());
        }

        [Test]
        public void GetTagsOfQuestion_QuestionIdProvided_ReturnsTagsWithQuestionId()
        {
            var questionId = 0;
            var tags = repo.GetTagsOfQuestion(questionId);
            Assert.That(tags, Is.Not.Null);
            Assert.That(tags, Is.InstanceOf<IEnumerable<ITag>>());
        }
        [Test]
        public void AddQuestion_IDContentUserIdProvided_AddsQuestionWithProvidedData()
        {
            var question = new Question { ID = 3, Content = "New Question", UserID = 1 };
            repo.AddQuestion(question);
            var retrievedQuestion = repo.GetQuestion(question.ID);
            Assert.That(retrievedQuestion, Is.Not.Null);
            Assert.That(retrievedQuestion, Is.EqualTo(question));
        }

        [Test]
        public void GetQuestion_QuestionIdProvided_ReturnsQuestionWithId()
        {
            var questionId = 0;
            var expectedQuestion = repo.GetQuestion(questionId);
            var retrievedQuestion = repo.GetQuestion(questionId);
            Assert.That(retrievedQuestion, Is.Not.Null);
            Assert.That(retrievedQuestion, Is.EqualTo(expectedQuestion));
        }
    }
}
