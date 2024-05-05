using System;
using NUnit.Framework;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.Reactions;
using System.Collections;
using System.Reflection;
using UBB_SE_2024_Team_42.Repository;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;

namespace Team42Test.RepositoryTests
{
    [TestFixture]
    public class TSQLRepositoryTests
    {
        public TSQLRepository mockTsqlRepository;
        [SetUp]
        public void Setup()
        {
            mockTsqlRepository = new TSQLRepository("Data Source=DESKTOP-2E72F19;" + "Initial Catalog=Team42DB;" +
                "Integrated Security=True;");
        }
        [Test]
        public void GetNotificationsOfUser_ValidUserIdProvided_ReturnsIEnumerableOfINotification()
        {
            const long expectedUserId = 1;

            var notifications = mockTsqlRepository.GetNotificationsOfUser(expectedUserId);

            Assert.That(notifications, Is.Not.Null);
            Assert.That(notifications, Is.InstanceOf<IEnumerable<INotification>>());
        }
        [Test]
        public void GetCategoriesModeratedByUser_ValidUserIdProvided_ReturnsIEnumerableOfICategory()
        {
            const long expectedUserId = 1;

            var categories = mockTsqlRepository.GetCategoriesModeratedByUser(expectedUserId);

            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
        }
        [Test]
        public void GetBadgesOfUser_ValidUserIdProvided_ReturnsIEnumerableOfIBadge()
        {
            const long expectedUserId = 1;

            var badges = mockTsqlRepository.GetBadgesOfUser(expectedUserId);

            Assert.That(badges, Is.Not.Null);
            Assert.That(badges, Is.InstanceOf<IEnumerable<IBadge>>());
        }
        [Test]
        public void GetAllUsers_OnDefaultTSQLRepository_ReturnsIEnumerableOfIUser()
        {
            var users = mockTsqlRepository.GetAllUsers();

            Assert.That(users, Is.Not.Null);
            Assert.That(users, Is.InstanceOf<IEnumerable<IUser>>());
        }
        [Test]
        public void GetReactionsOfPostByPostId_ValidPostIdProvided_ReturnsIEnumerableofIReaction()
        {
            const long expectedPostId = 1;

            var reactions = mockTsqlRepository.GetReactionsOfPostByPostID(expectedPostId);

            Assert.That(reactions, Is.Not.Null);
            Assert.That(reactions, Is.InstanceOf<IEnumerable<IReaction>>());
        }
        [Test]
        public void GetAllCategories_OnDefaultTSQLRepository_ReturnsIEnumerableOfICategory()
        {
            var categories = mockTsqlRepository.GetAllCategories();

            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
        }
        [Test]
        public void GetTagsOfQuestion_ValidQuestionIdProvided_ReturnsIEnumerableOfITag()
        {
            const long expectedQuestionId = 1;

            var tags = mockTsqlRepository.GetTagsOfQuestion(expectedQuestionId);

            Assert.That(tags, Is.Not.Null);
            Assert.That(tags, Is.InstanceOf<IEnumerable<ITag>>());
        }
        [Test]
        public void GetAllQuestions_OnDefaultTSQLRepository_ReturnsIEnumerableOfIQuestion()
        {
            var questions = mockTsqlRepository.GetAllQuestions();

            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
        }
        [Test]
        public void GetAnswersOfUser_ValidUserIdProvided_ReturnsIEnumerableOfIAnswer()
        {
            const long expectedUserId = 1;

            var answers = mockTsqlRepository.GetAnswersOfUser(expectedUserId);

            Assert.That(answers, Is.Not.Null);
            Assert.That(answers, Is.InstanceOf<IEnumerable<IAnswer>>());
        }
        [Test]
        public void GetCommentsOfUser_ValidUserIdProvided_ReturnsIEnumerableOfIComment()
        {
            const long expectedUserId = 1;

            var comments = mockTsqlRepository.GetCommentsOfUser(expectedUserId);

            Assert.That(comments, Is.Not.Null);
            Assert.That(comments, Is.InstanceOf<IEnumerable<IComment>>());
        }
        [Test]
        public void GetQuestionsOfUser_ValidUserIdProvided_ReturnsIEnumerableOfIQuestion()
        {
            const long expectedUserId = 1;

            var questions = mockTsqlRepository.GetQuestionsOfUser(expectedUserId);

            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
        }
        [Test]
        public void GetQuestion_ValidQuestionIdProvided_ReturnsIQuestion()
        {
            const long expectedQuestionId = 1;

            var question = mockTsqlRepository.GetQuestion(expectedQuestionId);

            Assert.That(question, Is.Not.Null);
            Assert.That(question, Is.InstanceOf<IQuestion>());
        }
        [Test]
        public void GetUser_ValidUserIdProvided_ReturnsIUser()
        {
            const long expectedUserId = 1;

            var user = mockTsqlRepository.GetUser(expectedUserId);

            Assert.That(user, Is.Not.Null);
            Assert.That(user, Is.InstanceOf<IUser>());
        }
        [Test]
        public void GetCategoryById_ValidCategoryIdProvided_ReturnsICategory()
        {
            const long expectedCategoryId = 1;

            var category = mockTsqlRepository.GetCategoryByID(expectedCategoryId);

            Assert.That(category, Is.Not.Null);
            Assert.That(category, Is.InstanceOf<ICategory>());
        }
        [Test]
        public void GetRepliesOfPost_ValidPostIdProvided_ReturnsIEnumerableOfIPost()
        {
            const long expectedPostId = 1;

            var replies = mockTsqlRepository.GetRepliesOfPost(expectedPostId);

            Assert.That(replies, Is.Not.Null);
            Assert.That(replies, Is.InstanceOf<IEnumerable<IPost>>());
        }
        [Test]
        public void AddQuestion_ValidQuestionProvided_SavesQuestionToDatabase()
        {
            var expectedQuestion = new Question
            {
                UserID = 33,
                Title = "Test Question",
                Content = "Test Content",
                Category = new Category { ID = 1 }
            };

            mockTsqlRepository.AddQuestion(expectedQuestion);

            Assert.That(mockTsqlRepository.GetQuestion(expectedQuestion.ID), Is.EqualTo(expectedQuestion));
        }
    }
}
