using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.BuilderTests
{
    [TestFixture]
    public class ReactionBuilderTests
    {
        public ReactionBuilder mockBuilder;
        [SetUp]
        public void Setup()
        {
            mockBuilder = new ReactionBuilder();
        }

        [Test]
        public void Begin_InitializeNewInstance()
        {
            mockBuilder.Begin();

            var answer = mockBuilder.End();
            Assert.That(answer, Is.Not.Null);
            Assert.That(answer, Is.InstanceOf<IReaction>());
        }

        [Test]
        public void SetReactionValue_SetReactionValueTo1()
        {
            const int expectedReactionValue = 1;

            mockBuilder.SetReactionValue(expectedReactionValue);
            var answer = mockBuilder.End();

            Assert.That(answer.Value, Is.EqualTo(expectedReactionValue));
        }

        [Test]
        public void SetReacterUserId_SetReacterUserIdTo1()
        {
            const int expectedUserId = 1;

            mockBuilder.SetReacterUserId(expectedUserId);
            var answer = mockBuilder.End();

            Assert.That(answer.UserID, Is.EqualTo(expectedUserId));
        }

        [TearDown]
        public void TearDown()
        {
            mockBuilder.End();
        }
    }
}
