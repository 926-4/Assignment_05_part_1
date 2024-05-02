using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Service;

namespace Team42Test.ServiceTests
{
    public class ServiceTests
    {
        public MemoryRepository mockMemoryRepository;
        public Service mockService;

        [SetUp]
        public void Setup()
        {
            mockMemoryRepository = new MemoryRepository();
            mockService = new Service(mockMemoryRepository);
        }

        [Test]
        public void GetUser_UserIdProvided_ReturnsUserWithId()
        {
            IUser user = new User();
            user.ID = 1337;

            mockMemoryRepository.AddUser(user);

            Assert.That(mockService.GetUser(user.ID), Is.EqualTo(user));
        }

        [Test]
        public void UpdatePost_OldPostNewPostProvided_ChecksPostContent()
        {
            IPost oldPost = new TextPost(1, "a");
            IPost newPost = new TextPost(1, "b");

            mockService.AddPost(oldPost);
            mockService.UpdatePost(oldPost, newPost);

            Assert.That(mockService.GetPost(oldPost.ID).Content, Is.EqualTo(newPost.Content));
        }

        [Test]
        public void AddQuestion_QuestionProvided_ReturnsQuestionsCount()
        {
            long oldQuestionsCount = mockService.GetAllQuestions().Count;

            mockService.AddQuestion("intrebare", "o intrebare", new UBB_SE_2024_Team_42.Domain.Category.Category());

            long newQuestionsCount = mockService.GetAllQuestions().Count;

            Assert.That(oldQuestionsCount + 1, Is.EqualTo(newQuestionsCount));
        }

        [Test]
        public void GetQuestion_QuestionIdProvided_ReturnsQuestion()
        {
            long questionId = 0;

            IQuestion question = mockService.GetQuestion(questionId);

            Assert.That(question, Is.Not.Null);
            Assert.That(question, Is.InstanceOf<IQuestion>());
            Assert.That(question.ID, Is.EqualTo(questionId));
        }

        [Test]
        public void GetAllQuestions_OnDefaultService_ReturnsQuestions()
        {
            List<IQuestion> questions = mockService.GetAllQuestions();

            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(questions, Is.Not.Empty);
        }

        [Test]
        public void GetRepliesOfPost_()
        {

        }

        [Test]
        public void GetQuestionsOfCategory_QuestionsProvided_ReturnsQuestionsByCategory()
        {
            string questionTitle1 = "Numar elemente lista C#";
            string questionContent1 = "Cum aflii numarul de elemente dintr-o lista in C#?";
            Category questionCategory1 = new UBB_SE_2024_Team_42.Domain.Category.Category();            
            questionCategory1.Name = "jmechereala la info";

            string questionTitle2 = "Mancare seara asta";
            string questionContent2 = "Ce comandam in seara asta de mancare?";
            Category questionCategory2 = new UBB_SE_2024_Team_42.Domain.Category.Category();
            questionCategory2.Name = "decizii";

            string questionTitle3 = "Locatie seara asta";
            string questionContent3 = "Unde mergem in seara asta?";
            Category questionCategory3 = new UBB_SE_2024_Team_42.Domain.Category.Category();
            questionCategory3.Name = "decizii";

            Category questionCategory4 = new Category();
            questionCategory4.Name = "Questionable";

            mockService.AddQuestion(questionTitle1, questionContent1, questionCategory1);
            mockService.AddQuestion(questionTitle2, questionContent2, questionCategory2);
            mockService.AddQuestion(questionTitle3, questionContent3, questionCategory3);

            List<IQuestion> questionsOfCategory4 = mockService.GetQuestionsOfCategory(questionCategory4);
            List<IQuestion> questionsOfCategory2 = mockService.GetQuestionsOfCategory(questionCategory2);

            Assert.That(questionsOfCategory4, Is.Empty);

            Assert.That(questionsOfCategory2.Count, Is.EqualTo(2));
            Assert.That(questionsOfCategory2[0].Title ?? "", Is.EqualTo(questionTitle2));
            Assert.That(questionsOfCategory2[0].Content, Is.EqualTo(questionContent2));
            Assert.That(questionsOfCategory2[1].Title ?? "", Is.EqualTo(questionTitle3));
            Assert.That(questionsOfCategory2[1].Content, Is.EqualTo(questionContent3));
        }

        [Test]
        public void GetQuestionsWithAtLeastOneAnswer_()
        {

        }

        [Test]
        public void FindQuestionsByPartialStringInAnyField_()
        {

        }

        [Test]
        public void GetQuestionsSortedByScoreAscending_()
        {

        }

        [Test]
        public void GetQuestionsSortedByScoreDescending_()
        {

        }

        [Test]
        public void GetReactionScore_()
        {
            
        }

        [Test]
        public void SortQuestionsByNumberOfAnswersAscending_()
        {

        }

        [Test]
        public void SortQuestionsByNumberOfAnswersDescending_()
        {

        }

        [Test]
        public void SortQuestionsByDateAscending_QuestionsProvided_ReturnsSortedQuestionsByDateAscending()
        {
            IQuestion question1 = new Question();
            question1.DatePosted = DateTime.Now.AddHours(-5);
            IQuestion question2 = new Question();
            question2.DatePosted = DateTime.Now;
            IQuestion question3 = new Question();
            question3.DatePosted = DateTime.Now.AddHours(-1);

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);
            
            List<IQuestion> sortedQuestions = mockService.SortQuestionsByDateAscending();

            Assert.That(sortedQuestions, Is.Not.Null);
            Assert.That(sortedQuestions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(sortedQuestions, Is.EquivalentTo(new List<IQuestion> { question1, question3, question2 }));
        }

        [Test]
        public void SortQuestionsByDateDescending_QuestionsProvided_ReturnsSortedQuestionsByDateDescending()
        {
            IQuestion question1 = new Question();
            question1.DatePosted = DateTime.Now.AddHours(-5);
            IQuestion question2 = new Question();
            question2.DatePosted = DateTime.Now;
            IQuestion question3 = new Question();
            question3.DatePosted = DateTime.Now.AddHours(-1);

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> sortedQuestions = mockService.SortQuestionsByDateAscending();

            Assert.That(sortedQuestions, Is.Not.Null);
            Assert.That(sortedQuestions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(sortedQuestions, Is.EquivalentTo(new List<IQuestion> { question2, question3, question1 }));
        }

        [Test]
        public void GetAllCategories_OnDefaultService_RetursCategories()
        {
            List<ICategory> categories = mockService.GetAllCategories();

            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
            Assert.That(categories, Is.Not.Empty);
        }

        [Test]
        public void GetCurrentQuestions_QuestionsProvided_ReturnsCurrentQuestions()
        {
            // assuming that "current" means @Today
            IQuestion question1 = new Question();
            IQuestion question2 = new Question();
            question2.DatePosted = DateTime.Now.AddDays(-1);

            List<IQuestion> questions = mockService.GetCurrentQuestions();

            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(questions.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAnswersOfUser_()
        {

        }

        [Test]
        public void GetQuestionsOfUser_()
        {

        }

        [Test]
        public void GetCommentsOfUser_()
        {

        }

        [Test]
        public void GetTagsOfQuestion_QuestionsWithAndWithoutTagsProvided_ReturnsQuestionsTags()
        {
            ITag tag1 = new Tag();
            IQuestion question1 = new Question();
            List<ITag> question1Tags = new List<ITag> { tag1 };

            IQuestion question2 = new Question();

            List<ITag> receivedQuestion1Tags = mockService.GetTagsOfQuestion(question1.ID);
            List<ITag> receivedQuestion2Tags = mockService.GetTagsOfQuestion(question2.ID);

            Assert.That(receivedQuestion1Tags, Is.Not.Null);
            Assert.That(receivedQuestion1Tags, Is.InstanceOf<IEnumerable<ITag>>());
            Assert.That(receivedQuestion1Tags, Is.Not.Empty);

            Assert.That(receivedQuestion1Tags, Is.Not.Null);
            Assert.That(receivedQuestion1Tags, Is.InstanceOf<IEnumerable<ITag>>());
            Assert.That(receivedQuestion1Tags, Is.Empty);
        }

        [Test]
        public void GetBadgesOfUser_UsersWithAndWithoutBadgesProvided_ReturnsUsersBadges()
        {
            IBadge badge = new Badge();
            List<IBadge> user1Badges = new List<IBadge> { badge };
            IUser user1 = new User();
            user1.BadgeList = user1Badges;

            IUser user2 = new User();

            List<IBadge> receivedUser1Badges = mockService.GetBadgesOfUser(user1.ID);
            List<IBadge> receivedUser2Badges = mockService.GetBadgesOfUser(user2.ID);

            Assert.That(receivedUser1Badges, Is.Not.Null);
            Assert.That(receivedUser1Badges, Is.InstanceOf<IEnumerable<IBadge>>());
            Assert.That(receivedUser1Badges.Count, Is.EqualTo(1));

            Assert.That(receivedUser2Badges, Is.Not.Null);
            Assert.That(receivedUser2Badges, Is.InstanceOf<IEnumerable<IBadge>>());
            Assert.That(receivedUser2Badges, Is.Empty);
        }

        [Test]
        public void FilterQuestionsByLast7Days_()
        {

        }

        [Test]
        public void FilterQuestionsAnsweredThisMonth_()
        {

        }

        [Test]
        public void FilterQuestionsAnsweredLastYear_()
        {

        }
    }
}
