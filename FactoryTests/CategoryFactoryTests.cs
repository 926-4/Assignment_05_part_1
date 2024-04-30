using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.FactoryTests
{
    [TestFixture]
    public class CategoryFactoryTests
    {
        public CategoryFactory mockFactory;
        [SetUp]
        public void Setup()
        {
            mockFactory = new CategoryFactory();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockFactory.Begin();

            var answer = mockFactory.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<ICategory>());
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
        public void SetName_SetNameToHello()
        {
            const string expectedName = "Hello";

            mockFactory.SetName(expectedName);
            var answer = mockFactory.End();

            Assert.That(answer.Name, Is.EqualTo(expectedName));
        }

        [TearDown]
        public void TearDown()
        {
            mockFactory.End();
        }
    }
}
