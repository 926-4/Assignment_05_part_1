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

namespace Team42Test.BuilderTests
{
    public class QuestionBuilderTests
    {
        public QuestionBuilder mockBuilder;
        [SetUp]
        public void Setup()
        {
            mockBuilder = new QuestionBuilder();
        }
        [Test]
        public void Begin_FactoryIsStarted_InitializesNewIQuestionInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IQuestion>());
        }

        [Test]
        public void SetId_IdProvidedIs1_SetIdTo1()
        {
            const long expectedId = 1;

            mockBuilder.SetId(expectedId);
            var answer = mockBuilder.End();

            Assert.That(answer.ID, Is.EqualTo(expectedId));
        }

        [Test]
        public void SetTitle_TitleProvidedIsHello_SetTitleToHello()
        {
            const string expectedTitle = "Hello";

            mockBuilder.SetTitle(expectedTitle);
            var answer = mockBuilder.End();

            Assert.That(answer.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public void SetCategory_CategoryIsDefaultCategory_SetCategoryToDefaultCategory()
        {
            ICategory expectedCategory = new Category();

            mockBuilder.SetCategory(expectedCategory);
            var answer = mockBuilder.End();

            Assert.That(answer.Category, Is.EqualTo(expectedCategory));
            Assert.That(answer.Category, Is.InstanceOf<ICategory>());
        }

        [Test]
        public void SetTags_TagLisProvididedIsEmpty_SetTagsToEmptyList()
        {
            List<ITag> expectedTags = [];

            mockBuilder.SetTags(expectedTags);
            var answer = mockBuilder.End();

            Assert.That(answer.Tags, Is.Empty);
            Assert.That(answer.Tags, Is.EqualTo(expectedTags));
        }

        [Test]
        public void SetUserId_UserIdProvidedIs1_SetUserIdTo1()
        {
            const long expectedUserId = 1;

            mockBuilder.SetUserId(expectedUserId);
            var answer = mockBuilder.End();

            Assert.That(answer.UserID, Is.EqualTo(expectedUserId));
        }

        [Test]
        public void SetContent_ContentNameProvidedIsHello_SetContentToHello()
        {
            const string expectedContent = "Hello";

            mockBuilder.SetContent(expectedContent);
            var answer = mockBuilder.End();

            Assert.That(answer.Content, Is.EqualTo(expectedContent));
        }

        [Test]
        public void SetPostTime_DateProvidedIs20230202_SetPostTimeTo_2023_02_02()
        {
            DateTime expectedDatePosted = DateTime.Parse("2023-02-02");

            mockBuilder.SetPostTime(expectedDatePosted);
            var answer = mockBuilder.End();

            Assert.That(answer.DatePosted, Is.EqualTo(expectedDatePosted));
        }

        [Test]
        public void SetEditTime_DateProvidedIs20230202_SetEditTimeTo_2023_02_02()
        {
            DateTime expectedDateOfLastEdit = DateTime.Parse("2023-02-02");

            mockBuilder.SetEditTime(expectedDateOfLastEdit);
            var answer = mockBuilder.End();

            Assert.That(answer.DateOfLastEdit, Is.EqualTo(expectedDateOfLastEdit));
        }

        [Test]
        public void SetReactions_ReactionListProvidedIsEmpty_SetReactionsToEmptyReactionsList()
        {
            var expectedReactionList = new List<IReaction>();

            mockBuilder.SetReactions(expectedReactionList);
            var answer = mockBuilder.End();

            Assert.That(answer.Reactions, Is.Empty);
            Assert.That(answer.Reactions, Is.EqualTo(expectedReactionList));
        }

        [TearDown]
        public void TearDown()
        {
            mockBuilder.End();
        }
    }
}
