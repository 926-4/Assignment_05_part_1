using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.BuilderTests
{
    [TestFixture]
    public class TextPostBuilderTests
    {
        public TextPostBuilder mockBuilder;
        [SetUp]
        public void Setup()
        {
            mockBuilder = new TextPostBuilder();
        }
        [Test]
        public void Begin_FactoryIsStarted_InitializesNewIPostInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IPost>());
        }

        [Test]
        public void SetId_IdProvidedIs1_SetIdTo1()
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
        public void SetDatePosted_DateProvidedIs20230202_SetDatePostedTo_2023_02_02()
        {
            DateTime expectedDatePosted = DateTime.Parse("2023-02-02");

            mockBuilder.SetDatePosted(expectedDatePosted);
            var answer = mockBuilder.End();

            Assert.That(answer.DatePosted, Is.EqualTo(expectedDatePosted));
        }

        [Test]
        public void SetDateOfLastEdit_DateProvidedIs20230202_SetDateOfLastEditTo_2023_02_02()
        {
            DateTime expectedDateOfLastEdit = DateTime.Parse("2023-02-02");

            mockBuilder.SetDateOfLastEdit(expectedDateOfLastEdit);
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
