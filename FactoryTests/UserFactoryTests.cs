using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Notification;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.BuilderTests
{
    public class UserBuilderTests
    {
        public UserBuilder mockBuilder;
        [SetUp]
        public void Setup()
        {
            mockBuilder = new UserBuilder();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IUser>());
        }

        [Test]
        public void SetName_SetNameToHello()
        {
            const string expectedName = "Hello";

            mockBuilder.SetName(expectedName);
            var answer = mockBuilder.End();

            Assert.That(answer.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void SetNotificationList_SetNotificationListToEmptyList()
        {
            List<INotification> expectedNotificationlist = [];

            mockBuilder.SetNotificationList(expectedNotificationlist);
            var answer = mockBuilder.End();

            Assert.That(answer.NotificationList, Is.Empty);
            Assert.That(answer.NotificationList, Is.EqualTo(expectedNotificationlist));
        }

        [Test]
        public void SetCategoriesModeratedList_SetCategoriesModeratedListToEmptyList()
        {
            List<ICategory> expectedCategories = [];

            mockBuilder.SetCategoriesModeratedList(expectedCategories);
            var answer = mockBuilder.End();

            Assert.That(answer.CategoriesModeratedList, Is.Empty);
            Assert.That(answer.CategoriesModeratedList, Is.EqualTo(expectedCategories));
        }

        [Test]
        public void SetBadgeList_SetBadgeListToEmptyList()
        {
            List<IBadge> expectedBadgelist = [];

            mockBuilder.SetBadgeList(expectedBadgelist);
            var answer = mockBuilder.End();

            Assert.That(answer.BadgeList, Is.Empty);
            Assert.That(answer.BadgeList, Is.EqualTo(expectedBadgelist));
        }

        [TearDown]
        public void TearDown()
        {
            mockBuilder.End();
        }
    }
}
