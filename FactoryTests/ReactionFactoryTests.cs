using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.FactoryTests
{
    [TestFixture]
    public class ReactionFactoryTests
    {
        public ReactionFactory mockFactory;
        [SetUp]
        public void Setup()
        {
            mockFactory = new ReactionFactory();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockFactory.Begin();

            var answer = mockFactory.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IReaction>());
        }

        [Test]
        public void SetReactionValue_SetReactionValueTo1()
        {
            const int expectedReactionValue = 1;

            mockFactory.SetReactionValue(expectedReactionValue);
            var answer = mockFactory.End();

            Assert.That(answer.Value, Is.EqualTo(expectedReactionValue));
        }

        [Test]
        public void SetReacterUserId_SetReacterUserIdTo1()
        {
            const int expectedUserId = 1;

            mockFactory.SetReacterUserId(expectedUserId);
            var answer = mockFactory.End();

            Assert.That(answer.UserID, Is.EqualTo(expectedUserId));
        }

        [TearDown]
        public void TearDown()
        {
            mockFactory.End();
        }
    }
}
