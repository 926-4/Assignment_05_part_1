using UBB_SE_2024_Team_42.Service.EntityCreationServices;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.Badge;
namespace Team42Test.BuilderTests
{
    [TestFixture]
    public class BadgeBuilderTests
    {
        public BadgeBuilder mockBuilder;

        [SetUp]
        public void Setup()
        {
            mockBuilder = new BadgeBuilder();
        }

        [Test]
        public void Begin_FactoryIsStarted_InitializesNewIBadgeInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IBadge>());
        }

        [Test]
        public void SetName_NameProvidedIsHello_SetNameToHello()
        {
            const string expectedName = "Hello";

            mockBuilder.Begin().SetName(expectedName);
            var answer = mockBuilder.End();
            
            Assert.That(answer.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void SetDescription_DescriptionProvidedIsHello_SetDescriptionToHello()
        {
            const string expectedDescription = "Hello";

            mockBuilder.Begin().SetDescription(expectedDescription);
            var answer = mockBuilder.End();

            Assert.That(answer.Description, Is.EqualTo(expectedDescription));
        }

        [Test]
        public void SetImage_ImageProvidedIsNull_SetImageToNull()
        {
            mockBuilder.Begin().SetImage(null);
            var answer = mockBuilder.End();

            Assert.That(answer.Image, Is.Null);
        }

        [TearDown] public void TearDown()
        {
            mockBuilder.End();
        }
    }
}
