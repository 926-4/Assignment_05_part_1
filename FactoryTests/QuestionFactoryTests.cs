using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.FactoryTests
{
    public class QuestionFactoryTests
    {
        public QuestionFactory mockFactory;
        [SetUp]
        public void Setup()
        {
            mockFactory = new QuestionFactory();
        }
        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockFactory.Begin();

            var answer = mockFactory.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IQuestion>());
        }

        [Test]
        public void SetId_SetIdTo1()
        {
            const long expectedId = 1;

            mockFactory.SetId(expectedId);
            var answer = mockFactory.End();

            Assert.That(answer.ID, Is.EqualTo(expectedId));
        }

        [Test]
        public void SetTitle_SetTitleToHello()
        {
            const string expectedTitle = "Hello";

            mockFactory.SetTitle(expectedTitle);
            var answer = mockFactory.End();

            Assert.That(answer.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void SetCategory_SetCategoryToDefaultCategory()
        {
            ICategory expectedCategory = new Category();

            mockFactory.SetCategory(expectedCategory);
            var answer = mockFactory.End();

            Assert.That(answer.Category, Is.EqualTo(expectedCategory));
            Assert.That(answer.Category, Is.InstanceOf<ICategory>());
        }

        [Test]
        public void SetTags_SetTagsToEmptyList()
        {
            List<ITag> expectedTags = [];

            mockFactory.SetTags(expectedTags);
            var answer = mockFactory.End();

            Assert.That(answer.Tags, Is.Empty);
            Assert.That(answer.Tags, Is.EqualTo(expectedTags));
        }

        [Test]
        public void SetUserId_SetUserIdTo1()
        {
            const long expectedUserId = 1;

            mockFactory.SetUserId(expectedUserId);
            var answer = mockFactory.End();

            Assert.That(answer.UserID, Is.EqualTo(expectedUserId));
        }

        [Test]
        public void SetContent_SetContentToHello()
        {
            const string expectedContent = "Hello";

            mockFactory.SetContent(expectedContent);
            var answer = mockFactory.End();

            Assert.That(answer.Content, Is.EqualTo(expectedContent));
        }

        [Test]
        public void SetPostTime_SetPostTimeTo_2023_02_02()
        {
            DateTime expectedDatePosted = DateTime.Parse("2023-02-02");

            mockFactory.SetPostTime(expectedDatePosted);
            var answer = mockFactory.End();

            Assert.That(answer.DatePosted, Is.EqualTo(expectedDatePosted));
        }

        [Test]
        public void SetEditTime_SetEditTimeTo_2023_02_02()
        {
            DateTime expectedDateOfLastEdit = DateTime.Parse("2023-02-02");

            mockFactory.SetEditTime(expectedDateOfLastEdit);
            var answer = mockFactory.End();

            Assert.That(answer.DateOfLastEdit, Is.EqualTo(expectedDateOfLastEdit));
        }

        [Test]
        public void SetReactions_SetReactionsToEmptyReactionsList()
        {
            var expectedReactionList = new List<IReaction>();

            mockFactory.SetReactions(expectedReactionList);
            var answer = mockFactory.End();

            Assert.That(answer.Reactions, Is.Empty);
            Assert.That(answer.Reactions, Is.EqualTo(expectedReactionList));
        }

        [TearDown]
        public void TearDown()
        {
            mockFactory.End();
        }
    }
}
