using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.FactoryTests
{
    [TestFixture]
    public class NotificationFactoryTests
    {
        public NotificationFactory mockFactory;
        [SetUp]
        public void Setup()
        {
            mockFactory = new NotificationFactory();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockFactory.Begin();

            var answer = mockFactory.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<INotification>());
        }

        [Test]
        public void SetId_SetIdTo1()
        {
            const long expectedId = 1;

            mockFactory.SetID(expectedId);
            var answer = mockFactory.End();

            Assert.That(answer.ID, Is.EqualTo(expectedId));
        }

        [Test]
        public void SetText_SetTextToHello()
        {
            const string expectedText = "hello";

            mockFactory.SetText(expectedText);
            var answer = mockFactory.End();

            Assert.That(answer.Text, Is.EqualTo(expectedText));
        }

        [Test]
        public void SetPostId_SetPostIdTo1()
        {
            const long expectedPostId = 1;

            mockFactory.SetPostID(expectedPostId);
            var answer = mockFactory.End();

            Assert.That(answer.PostID, Is.EqualTo(expectedPostId));
        }

        [Test]
        public void SetBadgeId_SetBadgeIdTo1()
        {
            const long expectedBadgeId = 1;

            mockFactory.SetBadgeId(expectedBadgeId);
            var answer = mockFactory.End();

            Assert.That(answer.BadgeID, Is.EqualTo(expectedBadgeId));
        }

        [TearDown]
        public void TearDown()
        {
            mockFactory.End();
        }
    }
}
