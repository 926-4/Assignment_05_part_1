using UBB_SE_2024_Team_42.Service.EntityCreationServices;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.Badge;
namespace Team42Test.FactoryTests
{
    [TestFixture]
    public class BadgeFactoryTests
    {
        public BadgeFactory mockFactory;

        [SetUp]
        public void Setup()
        {
            mockFactory = new BadgeFactory();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockFactory.Begin();

            var answer = mockFactory.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IBadge>());
        }

        [Test]
        public void SetName_SetNameToHello()
        {
            const string expectedName = "Hello";

            mockFactory.Begin().SetName(expectedName);
            var answer = mockFactory.End();
            
            Assert.That(answer.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void SetDescription_SetDescriptionToHello()
        {
            const string expectedDescription = "Hello";

            mockFactory.Begin().SetDescription(expectedDescription);
            var answer = mockFactory.End();

            Assert.That(answer.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void SetImage_SetImageToNull()
        {
            mockFactory.Begin().SetImage(null);
            var answer = mockFactory.End();

            Assert.That(answer.Image, Is.Null);
        }

        [TearDown] public void TearDown()
        {
            mockFactory.End();
        }
    }
}
