using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.BuilderTests
{
    [TestFixture]
    public class TagBuilderTests
    {
        public TagBuilder mockBuilder;
        [SetUp]
        public void Setup()
        {
            mockBuilder = new TagBuilder();
        }

        [Test]
        public void Begin_FactoryIsStarted_InitializesNewITagInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<ITag>());
        }

        [Test]
        public void SetId_IdProvidedIs1_SetIdTo1()
        {
            const long expectedId = 1;

            mockBuilder.SetID(expectedId);
            var answer = mockBuilder.End();

            Assert.That(answer.Id, Is.EqualTo(expectedId));
        }

        [Test]
        public void SetName_NameProvidedISHello_SetNameToHello()
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
