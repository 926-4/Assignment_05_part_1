using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.FactoryTests
{
    [TestFixture]
    public class TextPostFactoryTests
    {
        public TextPostFactory mockFactory;
        [SetUp]
        public void Setup()
        {
            mockFactory = new TextPostFactory();
        }
        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockFactory.Begin();

            var answer = mockFactory.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IPost>());
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
        public void SetDatePosted_SetDatePostedTo_2023_02_02()
        {
            DateTime expectedDatePosted = DateTime.Parse("2023-02-02");

            mockFactory.SetDatePosted(expectedDatePosted);
            var answer = mockFactory.End();

            Assert.That(answer.DatePosted, Is.EqualTo(expectedDatePosted));
        }

        [Test]
        public void SetDateOfLastEdit_SetDateOfLastEditTo_2023_02_02()
        {
            DateTime expectedDateOfLastEdit = DateTime.Parse("2023-02-02");

            mockFactory.SetDateOfLastEdit(expectedDateOfLastEdit);
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
