using System;
using NUnit.Framework;
using Moq;
using UBB_SE_2024_Team_42.Repository.TSQLRepository;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.Question;
using UBB_SE_2024_Team_42.Domain.Reaction;
using System.Collections;
using System.Reflection;

namespace Team42Test.RepositoryTests
{
    [TestFixture]
    public class TSQLRepositoryTests
    {
        public TSQLRepository mockTsqlRepository;
        [SetUp]
        public void Setup()
        {
            mockTsqlRepository = new TSQLRepository();
        }
        [Test]
        public void GetNotificationsOfUser_ReturnsNotifications()
        {
            const long expectedUserId = 1;
            
            var notifications = mockTsqlRepository.GetNotificationsOfUser(expectedUserId);

            Assert.That(notifications, Is.Not.Null);
            Assert.That(notifications, Is.InstanceOf<IEnumerable<INotification>>());
        }
        [Test]
        public void GetCategoriesModeratedByUser_ReturnsCategories()
        {
            const long expectedUserId = 1;
            
            var categories = mockTsqlRepository.GetCategoriesModeratedByUser(expectedUserId);

            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
        }
        [Test]
        public void GetBadgesOfUser_ReturnsBadges()
        {
            const long expectedUserId = 1;
            
            var badges = mockTsqlRepository.GetBadgesOfUser(expectedUserId);

            Assert.That(badges, Is.Not.Null);
            Assert.That(badges, Is.InstanceOf<IEnumerable<IBadge>>());
        }
        [Test]
        public void GetAllUsers_ReturnsUsers()
        {
            var users = mockTsqlRepository.GetAllUsers();

            Assert.That(users, Is.Not.Null);
            Assert.That(users, Is.InstanceOf<IEnumerable<IUser>>());
        }
        [Test]
        public void GetReactionsOfPostByPostId_ReturnsReactions()
        {
            const long expectedPostId = 1;
            
            var reactions = mockTsqlRepository.GetReactionsOfPostByPostId(expectedPostId);

            Assert.That(reactions, Is.Not.Null);
            Assert.That(reactions, Is.InstanceOf<IEnumerable<IReaction>());
        }
        [Test]
        public void GetAllCategories_ReturnsCategories()
        {
            var categories = mockTsqlRepository.GetAllCategories();

            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
        }
        [Test]
        public void GetTagsOfQuestion_ReturnsTags()
        {
            const long expectedQuestionId = 1;
            
            var tags = mockTsqlRepository.GetTagsOfQuestion(expectedQuestionId);

            Assert.That(tags, Is.Not.Null);
            Assert.That(tags, Is.InstanceOf<IEnumerable<ITag>>());
        }
        [Test]
        public void GetAllQuestions_ReturnsQuestions()
        {
            var questions = mockTsqlRepository.GetAllQuestions();

            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
        }
        [Test]
        public void GetAnswersOfUser_ReturnsAnswers()
        {
            const long expectedUserId = 1;
            
            var answers = mockTsqlRepository.GetAnswersOfUser(expectedUserId);

            Assert.That(answers, Is.Not.Null);
            Assert.That(answers, Is.InstanceOf<IEnumerable<IAnswer>>());
        }
        [Test]
        public void GetCommentsOfUser_ReturnsComments()
        {
            const long expectedUserId = 1;
            
            var comments = mockTsqlRepository.GetCommentsOfUser(expectedUserId);

            Assert.That(comments, Is.Not.Null);
            Assert.That(comments, Is.InstanceOf<IEnumerable<IComment>>());
        }
        [Test]
        public void GetQuestionsOfUser_ReturnsQuestions()
        {
            const long expectedUserId = 1;
            
            var questions = mockTsqlRepository.GetQuesitonsOfUser(expectedUserId);

            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
        }
        [Test]
        public void GetQuestion_ReturnsQuestion()
        {
            const long expectedQuestionId = 1;
            
            var question = mockTsqlRepository.GetQuestion(expectedQuestionId);

            Assert.That(question, Is.Not.Null);
            Assert.That(question, Is.InstanceOf<IQuestion>());
        }
        [Test]
        public void GetUser_ReturnsUser()
        {
            const long expectedUserId = 1;
            
            var user = mockTsqlRepository.GetUser(expectedUserId);

            Assert.That(user, Is.Not.Null);
            Assert.That(user, Is.InstanceOf<IUser>());
        }
        [Test]
        public void GetCategoryById_ReturnsCategory()
        {
            const long expectedCategoryId = 1;
            
            var category = mockTsqlRepository.GetCategoryById(expectedCategoryId);

            Assert.That(category, Is.Not.Null);
            Assert.That(category, Is.InstanceOf<ICategory>());
        }
        [Test]
        public void GetRepliesOfPost_ReturnsReplies()
        {
            const long expectedPostId = 1;
            
            var replies = mockTsqlRepository.GetRepliesOfPost(expectedPostId);

            Assert.That(replies, Is.Not.Null);
            Assert.That(replies, Is.InstanceOf<IEnumerable<IPost>>());
        }
        [Test]
        public void AddQuestion_SavesQuestionToDatabase()
        {
            var expectedQuestion = new Question
            {
                UserID = 1,
                Title = "Test Question",
                Content = "Test Content",
                Category = new Category { ID = 1 }
            };

            mockTsqlRepository.AddQuestion(expectedQuestion);

            var question = mockTsqlRepository.GetQuestionOfUser(1).Last();

            Assert.That(question, Is.Not.Null);
            Assert.That(question, Is.EqualTo(expectedQuestion));
        }
    }
}
