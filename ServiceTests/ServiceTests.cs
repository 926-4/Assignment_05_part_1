using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
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
        public void GetUser_CreateAndGetARandomUser()
        {
            IUser user = new User();
            user.ID = 1337;

            mockMemoryRepository.AddUser(user);

            Assert.Equals(mockService.GetUser(user.ID), user);
        }

        [Test]
        public void UpdatePost_()
        {
            IPost oldPost = new TextPost(1, "a");
            IPost newPost = new TextPost(1, "b");

            mockService.AddPost(oldPost);
            
            Assert.Equals(oldPost, mockService.GetPost(oldPost.ID));

            mockService.UpdatePost(oldPost, newPost);

            Assert.Equals(mockService.GetPost(oldPost.ID).Content, newPost.Content);
        }

        [Test]
        public void AddQuestion_()
        {
            long oldQuestionsCount = mockService.GetAllQuestions().Count;

            mockService.AddQuestion("intrebare", "o intrebare", new UBB_SE_2024_Team_42.Domain.Category.Category());

            long newQuestionsCount = mockService.GetAllQuestions().Count;

            Assert.Equals(oldQuestionsCount + 1, newQuestionsCount);
        }

        [Test]
        public void GetQuestion_()
        {
            
        }

        [Test]
        public void GetAllQuestions_()
        {

        }

        [Test]
        public void GetRepliesOfPost_()
        {

        }

        [Test]
        public void GetQuestionsOfCategory_()
        {
            long oldQuestionsCount = mockService.GetAllQuestions().Count;

            string questionTitle1 = "Numar elemente lista C#";
            string questionContent1 = "Cum aflii numarul de elemente dintr-o lista in C#?";
            Category questionCategory1 = new UBB_SE_2024_Team_42.Domain.Category.Category();
            
            questionCategory1.Name = "jmechereala la info";

            mockService.AddQuestion(questionTitle1, questionContent1, questionCategory1);

            string questionTitle2 = "Mancare seara asta";
            string questionContent2 = "Ce comandam in seara asta de mancare?";
            Category questionCategory2 = new UBB_SE_2024_Team_42.Domain.Category.Category();

            questionCategory2.Name = "decizii";

            mockService.AddQuestion(questionTitle2, questionContent2, questionCategory2);

            string questionTitle3 = "Locatie seara asta";
            string questionContent3 = "Unde mergem in seara asta?";
            Category questionCategory3 = new UBB_SE_2024_Team_42.Domain.Category.Category();

            questionCategory3.Name = "decizii";

            mockService.AddQuestion(questionTitle3, questionContent3, questionCategory3);

            long newQuestionsCount = mockService.GetAllQuestions().Count;

            Assert.Equals(oldQuestionsCount + 3, newQuestionsCount);

            List<IQuestion> questionsOfCategory1 = mockService.GetQuestionsOfCategory(questionCategory1);

            Assert.Equals(questionsOfCategory1.Count, 1);
            Assert.Equals(questionsOfCategory1[0].Title ?? "", questionTitle1);
            Assert.Equals(questionsOfCategory1[0].Content, questionContent1);

            List<IQuestion> questionsOfCategory2 = mockService.GetQuestionsOfCategory(questionCategory2);

            Assert.Equals(questionsOfCategory2.Count, 2);
            Assert.Equals(questionsOfCategory2[0].Title ?? "", questionTitle2);
            Assert.Equals(questionsOfCategory2[0].Content, questionContent2);
            Assert.Equals(questionsOfCategory2[1].Title ?? "", questionTitle3);
            Assert.Equals(questionsOfCategory2[1].Content, questionContent3);
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
        public void SortQuestionsByDateAscending_()
        {

        }

        [Test]
        public void SortQuestionsByDateDescending_()
        {

        }


        [Test]
        public void GetAllCategories_()
        {

        }

        [Test]
        public void GetCurrentQuestions_()
        {

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
        public void GetTagsOfQuestion_()
        {

        }

        [Test]
        public void GetBadgesOfUser_()
        {

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
