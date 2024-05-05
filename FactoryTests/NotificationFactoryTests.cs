using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.BuilderTests
{
    [TestFixture]
    public class NotificationBuilderTests
    {
        public NotificationBuilder mockBuilder;
        [SetUp]
        public void Setup()
        {
            mockBuilder = new NotificationBuilder();
        }

        [Test]
        public void Begin_FactoryIsStarted_InitializesNewICommentInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<INotification>());
        }

        [Test]
        public void SetId_IdProvidedIs1_SetIdTo1()
        {
            const long expectedId = 1;

            mockBuilder.SetID(expectedId);
            var answer = mockBuilder.End();

            Assert.That(answer.ID, Is.EqualTo(expectedId));
        }

        [Test]
        public void SetText_TextProvidedIsHello_SetTextToHello()
        {
            const string expectedText = "hello";

            mockBuilder.SetText(expectedText);
            var answer = mockBuilder.End();

            Assert.That(answer.Text, Is.EqualTo(expectedText));
        }

        [Test]
        public void SetPostId_PostIdProvidedIs1_SetPostIdTo1()
        {
            const long expectedPostId = 1;

            mockBuilder.SetPostID(expectedPostId);
            var answer = mockBuilder.End();

            Assert.That(answer.PostID, Is.EqualTo(expectedPostId));
        }

        [Test]
        public void SetBadgeId_BadgeIdProvidedIs1_SetBadgeIdTo1()
        {
            const long expectedBadgeId = 1;

            mockBuilder.SetBadgeId(expectedBadgeId);
            var answer = mockBuilder.End();

            Assert.That(answer.BadgeID, Is.EqualTo(expectedBadgeId));
        }

        [TearDown]
        public void TearDown()
        {
            mockBuilder.End();
        }
    }
}
