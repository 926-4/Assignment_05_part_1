using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Repository;
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
        public void GetQuestion_QuestionProvided_ReturnsQuestion()
        {
            Question question = new Question();
            question.ID = 1337;

            IQuestion receivedQuestion = mockService.GetQuestion(question.ID);

            Assert.That(receivedQuestion, Is.Not.Null);
            Assert.That(receivedQuestion, Is.InstanceOf<IQuestion>());
            Assert.That(receivedQuestion.ID, Is.EqualTo(question.ID));
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
        public void AddPostReply_PostReplyProvided_ReturnsPostReply()
        {
            IPost post1 = new TextPost();
            post1.ID = 2024;

            IPost post2 = new TextPost();
            post2.ID = 1337;

            IPost reply1 = new TextPost();

            mockService.AddPostReply(reply1, post1.ID);

            long post1RepliesCount = mockService.GetRepliesOfPost(post1.ID).Count;
            long post2RepliesCount = mockService.GetRepliesOfPost(post2.ID).Count;

            Assert.That(post1RepliesCount, Is.EqualTo(1));
            Assert.That(post2RepliesCount, Is.EqualTo(0));
        }

        [Test]
        public void GetRepliesOfPost_PostIdProvided_ReturnsRepliesOfPost()
        {
            IPost post1 = new TextPost();
            post1.ID = 2024;

            IPost post2 = new TextPost();
            post2.ID = 1337;

            IPost replyOfPost2 = new TextPost();

            mockService.AddPostReply(replyOfPost2, post2.ID);

            List<IPost> repliesOfPost1 = mockService.GetRepliesOfPost(post1.ID);
            List<IPost> repliesOfPost2 = mockService.GetRepliesOfPost(post2.ID);

            Assert.That(repliesOfPost1, Is.Not.Null);
            Assert.That(repliesOfPost1, Is.InstanceOf<IEnumerable<IPost>>());
            Assert.That(repliesOfPost1, Is.Empty);

            Assert.That(repliesOfPost2, Is.Not.Null);
            Assert.That(repliesOfPost2, Is.InstanceOf<IEnumerable<IPost>>());
            Assert.That(repliesOfPost2.Count, Is.EqualTo(1));
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
        public void GetAnswersOfUser_UsersAndAnswersProvided_ReturnsAnswersOfUsers()
        {
            IUser user1 = new User();
            user1.ID = 1337;
            IUser user2 = new User();
            user2.ID = 1338;

            IAnswer answer1ForUser1 = new Answer();
            answer1ForUser1.UserID = user1.ID;
            IAnswer answer2ForUser1 = new Answer();
            answer2ForUser1.UserID = user1.ID;
            IAnswer answer3ForUser1 = new Answer();
            answer2ForUser1.UserID = user1.ID;

            mockService.AddPost(answer1ForUser1);
            mockService.AddPost(answer2ForUser1);
            mockService.AddPost(answer3ForUser1);

            List<IAnswer> user1Answers = mockService.GetAnswersOfUser(user1.ID);
            List<IAnswer> user2Answers = mockService.GetAnswersOfUser(user2.ID);

            Assert.That(user1Answers, Is.Not.Null);
            Assert.That(user1Answers, Is.InstanceOf<IEnumerable<IAnswer>>());
            Assert.That(user1Answers, Is.Not.Empty);
            Assert.That(user1Answers.Count, Is.EqualTo(3));
            Assert.That(user2Answers, Is.Not.Null);
            Assert.That(user2Answers, Is.InstanceOf<IEnumerable<IAnswer>>());
            Assert.That(user2Answers, Is.Empty);
        }

        [Test]
        public void GetQuestionsOfUser_UsersAndQuestionsProvided_ReturnsQuestionsOfUsers()
        {
            User user1 = new User();
            user1.ID = 1337;
            User user2 = new User();
            user2.ID = 1338;

            Question question1 = new Question();
            question1.UserID = user1.ID;
            Question question2 = new Question();
            question2.UserID = user1.ID;
            Question question3 = new Question();
            question3.UserID = user1.ID;

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> user1Questions = mockService.GetQuestionsOfUser(user1.ID);
            List<IQuestion> user2Questions = mockService.GetQuestionsOfUser(user2.ID);

            Assert.That(user1Questions, Is.Not.Null);
            Assert.That(user1Questions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(user1Questions, Is.Not.Empty);
            Assert.That(user1Questions.Count, Is.EqualTo(3));
            Assert.That(user2Questions, Is.Not.Null);
            Assert.That(user2Questions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(user2Questions, Is.Empty);
        }

        [Test]
        public void GetCommentsOfUser_UsersAndCommentsProvided_ReturnsCommentsOfUsers()
        {
            User user1 = new User();
            user1.ID = 1337;
            User user2 = new User();
            user2.ID = 1338;

            Comment comment1 = new Comment();
            comment1.UserID = user1.ID;
            Comment comment2 = new Comment();
            comment2.UserID = user1.ID;
            Comment comment3 = new Comment();
            comment3.UserID = user1.ID;

            mockService.AddPost(comment1);
            mockService.AddPost(comment2);
            mockService.AddPost(comment3);

            List<IComment> user1Comments = mockService.GetCommentsOfUser(user1.ID);
            List<IComment> user2Comments = mockService.GetCommentsOfUser(user2.ID);

            Assert.That(user1Comments, Is.Not.Null);
            Assert.That(user1Comments, Is.InstanceOf<IEnumerable<IComment>>());
            Assert.That(user1Comments, Is.Not.Empty);
            Assert.That(user1Comments.Count, Is.EqualTo(3));
            Assert.That(user2Comments, Is.Not.Null);
            Assert.That(user2Comments, Is.InstanceOf<IEnumerable<IComment>>());
            Assert.That(user2Comments, Is.Empty);
        }

        [Test]
        public void GetTagsOfQuestion_QuestionsWithAndWithoutTagsProvided_ReturnsQuestionsTags()
        {
            ITag tag1 = new Tag();
            IQuestion question1 = new Question();
            List<ITag> question1Tags = new List<ITag> { tag1 };
            question1.Tags = question1Tags;

            IQuestion question2 = new Question();

            List<ITag> receivedQuestion1Tags = mockService.GetTagsOfQuestion(question1.ID);
            List<ITag> receivedQuestion2Tags = mockService.GetTagsOfQuestion(question2.ID);

            Assert.That(receivedQuestion1Tags, Is.Not.Null);
            Assert.That(receivedQuestion1Tags, Is.InstanceOf<IEnumerable<ITag>>());
            Assert.That(receivedQuestion1Tags, Is.Not.Empty);

            Assert.That(receivedQuestion2Tags, Is.Not.Null);
            Assert.That(receivedQuestion2Tags, Is.InstanceOf<IEnumerable<ITag>>());
            Assert.That(receivedQuestion2Tags, Is.Empty);
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
        public void FilterQuestionsByLast7Days_QuestionsProvided_ReturnsQuestionsByLast7DaysCount()
        {
            IQuestion question1 = new Question();
            question1.DatePosted = DateTime.Now;

            IQuestion question2 = new Question();
            question2.DatePosted = DateTime.Now;

            IQuestion question3 = new Question();
            question3.DatePosted = DateTime.Now.AddDays(-8);

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            int questionsByLast7DaysCount = mockService.FilterQuestionsByLast7Days();

            Assert.That(questionsByLast7DaysCount, Is.Not.Empty);
            Assert.That(questionsByLast7DaysCount, Is.GreaterThanOrEqualTo(2));
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
