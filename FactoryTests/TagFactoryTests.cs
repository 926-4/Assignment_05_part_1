using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.FactoryTests
{
    [TestFixture]
    public class TagFactoryTests
    {
        public TagFactory mockFactory;
        [SetUp]
        public void Setup()
        {
            mockFactory = new TagFactory();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockFactory.Begin();

            var answer = mockFactory.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<ITag>());
        }

        [Test]
        public void SetId_SetIdTo1()
        {
            const long expectedId = 1;

            mockFactory.SetID(expectedId);
            var answer = mockFactory.End();

            Assert.That(answer.Id, Is.EqualTo(expectedId));
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
