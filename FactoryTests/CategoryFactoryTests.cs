using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.BuilderTests
{
    [TestFixture]
    public class CategoryBuilderTests
    {
        public CategoryBuilder mockBuilder;
        [SetUp]
        public void Setup()
        {
            mockBuilder = new CategoryBuilder();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<ICategory>());
        }

        [Test]
        public void SetId_SetIdTo1()
        {
            const long expectedId = 1;

            mockBuilder.SetID(expectedId);
            var answer = mockBuilder.End();

            Assert.That(answer.ID, Is.EqualTo(expectedId));
        }

        [Test]
        public void SetName_SetNameToHello()
        {
            const string expectedName = "Hello";

            mockBuilder.SetName(expectedName);
            var answer = mockBuilder.End();

            Assert.That(answer.Name, Is.EqualTo(expectedName));
        }

        [TearDown]
        public void TearDown()
        {
            mockBuilder.End();
        }
    }
}
