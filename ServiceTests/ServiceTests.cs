using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2024_Team_42.Domain.Badge;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Reactions;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Domain.User;
using UBB_SE_2024_Team_42.Repository;
using UBB_SE_2024_Team_42.Service;
using UBB_SE_2024_Team_42.Service.EntityCreationServices;

namespace Team42Test.ServiceTests
{
    public class ServiceTests
    {
        public MemoryRepository mockRepository;
        public Service mockService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new MemoryRepository();
            mockService = new Service(mockRepository);
        }

        [Test]
        public void GetUser_UserIdProvided_ReturnsUserById()
        {
            User user = new User();
            user.ID = 1337;

            mockRepository.AddUser(user);

            Assert.That(mockService.GetUser(user.ID), Is.EqualTo(user));
        }

        [Test]
        public void GetUser_InvalidUserIdProvided_ReturnsUserByIdAndThrowsKeyNotFoundException()
        {
            User user = new User();
            user.ID = 1337;

            Assert.Throws<KeyNotFoundException>(() => mockService.GetUser(user.ID));
        }

        [Test]
        public void UpdatePost_OldPostAndNewPostProvided_ChecksPostContentAfterUpdate()
        {
            IPost oldPost = new TextPostBuilder().Begin().SetID(1).SetContent("a").End();
            IPost newPost = new TextPostBuilder().Begin().SetID(1).SetContent("b").End();

            mockService.AddPost(oldPost);
            mockService.UpdatePost(oldPost, newPost);

            Assert.That(mockService.GetPost(oldPost.ID).Content, Is.EqualTo(newPost.Content));
        }

        [Test]
        public void AddQuestion_QuestionProvided_ReturnsQuestionsCount()
        {
            long oldQuestionsCount = mockService.GetAllQuestions().Count;

            mockService.AddQuestion("intrebare", "o intrebare", new Category());

            long newQuestionsCount = mockService.GetAllQuestions().Count;

            Assert.That(oldQuestionsCount + 1, Is.EqualTo(newQuestionsCount));
        }

        [Test]
        public void GetQuestion_QuestionProvided_ReturnsQuestion()
        {
            Question question = new Question();
            question.ID = 1337;

            mockService.AddQuestionByObject(question);

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
            IPost post1 = new TextPostBuilder().Begin().SetID(2024).End();
            IPost post2 = new TextPostBuilder().Begin().SetID(1337).End();
            IPost reply1 = new TextPost();

            mockService.AddPost(post1);
            mockService.AddPost(post2);
            mockService.AddPostReply(reply1, post1.ID);

            long post1RepliesCount = mockService.GetRepliesOfPost(post1.ID).Count;
            long post2RepliesCount = mockService.GetRepliesOfPost(post2.ID).Count;

            Assert.That(post1RepliesCount, Is.EqualTo(1));
            Assert.That(post2RepliesCount, Is.EqualTo(0));
        }

        [Test]
        public void GetRepliesOfPost_PostIdProvided_ReturnsRepliesOfPost()
        {
            IPost post1 = new TextPostBuilder().Begin().SetID(2024).End();
            IPost post2 = new TextPostBuilder().Begin().SetID(1337).End();

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
            Category questionCategory1 = new();
            questionCategory1.Name = "jmechereala la info";
            IQuestion question1 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(11)
                                        .SetTitle("Numar elemente lista C#")
                                        .SetContent("Cum aflii numarul de elemente dintr-o lista in C#?")
                                        .SetCategory(questionCategory1)
                                        .End();

            Category questionCategory2 = new ();
            questionCategory2.Name = "decizii";
            IQuestion question2 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(22)
                                        .SetTitle("Mancare seara asta")
                                        .SetContent("Ce comandam in seara asta de mancare?")
                                        .SetCategory(questionCategory2)
                                        .End();

            Category questionCategory3 = new ();
            questionCategory3.Name = "decizii";
            IQuestion question3 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(33)
                                        .SetTitle("Locatie seara asta")
                                        .SetContent("Unde mergem in seara asta?")
                                        .SetCategory(questionCategory2)
                                        .End();

            Category questionCategory4 = new Category();
            questionCategory4.Name = "Questionable";

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> questionsOfCategory4 = mockService.GetQuestionsOfCategory(questionCategory4);
            List<IQuestion> questionsOfCategory2 = mockService.GetQuestionsOfCategory(questionCategory2);

            Assert.That(questionsOfCategory4, Is.Empty);

            Assert.That(questionsOfCategory2.Count, Is.EqualTo(2));
            Assert.That(questionsOfCategory2[0].Title ?? "", Is.EqualTo(question2.Title));
            Assert.That(questionsOfCategory2[0].Content, Is.EqualTo(question2.Content));
            Assert.That(questionsOfCategory2[1].Title ?? "", Is.EqualTo(question3.Title));
            Assert.That(questionsOfCategory2[1].Content, Is.EqualTo(question3.Content));
        }

        [Test]
        public void GetQuestionsWithAtLeastOneAnswer_QuestionsAndAnswersProvided_ReturnsQuestionsWithAtLeastOneAnswer()
        {
            Question question1 = new();
            question1.ID = 1337;

            Question question2 = new();
            question2.ID = 1338;

            Question question3 = new();
            question3.ID = 1339;

            Answer answer1 = new Answer();
            answer1.ID = 11;

            Answer answer2 = new Answer();
            answer2.ID = 22;

            mockRepository.AddPost(answer1);
            mockRepository.AddPost(answer2);

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            mockRepository.AddPostReply(answer1, question2.ID);
            mockRepository.AddPostReply(answer2, question3.ID);

            List<IQuestion> receivedQuestions = mockService.GetQuestionsWithAtLeastOneAnswer();

            Assert.That(receivedQuestions, Is.Not.Null);
            Assert.That(receivedQuestions, Is.Not.Empty);
            Assert.That(receivedQuestions.Count, Is.EqualTo(2));
        }

        [Test]
        public void FindQuestionsByPartialStringInAnyField_QuestionsProvided_ReturnsQuestionsThatMatchANCA()
        {
            Category questionCategory1 = new();
            questionCategory1.Name = "jmechereala la info";
            IQuestion question1 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(11)
                                        .SetTitle("Numar elemente lista C#")
                                        .SetContent("Cum aflii numarul de elemente dintr-o lista in C#?")
                                        .SetCategory(questionCategory1)
                                        .End();

            Category questionCategory2 = new();
            questionCategory2.Name = "decizii";
            IQuestion question2 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(22)
                                        .SetTitle("Mancare seara asta")
                                        .SetContent("Ce comandam in seara asta de mancare?")
                                        .SetCategory(questionCategory2)
                                        .End();

            Category questionCategory3 = new();
            questionCategory3.Name = "decizii";
            IQuestion question3 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(33)
                                        .SetTitle("Locatie seara asta")
                                        .SetContent("Unde mergem in seara asta?")
                                        .SetCategory(questionCategory2)
                                        .End();

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> receivedQuestions = mockService.FindQuestionsByPartialStringInAnyField("anca");

            Assert.That(receivedQuestions, Is.Not.Null);
            Assert.That(receivedQuestions, Is.Not.Empty);
            Assert.That(receivedQuestions.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetQuestionsSortedByScoreAscending_QuestionsAndReactionsProvided_ReturnsQuestionsSortedByScoreAscending()
        {
            Reaction reaction1 = new();
            reaction1.Value = 10;
            Reaction reaction2 = new ();
            reaction2.Value = 50;
            Reaction reaction3 = new ();
            reaction3.Value = 100;

            List<IReaction> question1Reactions = new List<IReaction> { reaction1, reaction1, reaction3 };  // Score = 160
            List<IReaction> question2Reactions = new List<IReaction> { reaction2, reaction2, reaction2 };  // Score = 150
            List<IReaction> question3Reactions = new List<IReaction> { reaction1, reaction2, reaction3 };  // Score = 120

            Question question1 = new Question();
            question1.ID = 11;
            question1.Reactions = question1Reactions;
            Question question2 = new Question();
            question2.ID = 22;
            question2.Reactions = question2Reactions;
            Question question3 = new Question();
            question3.ID = 33;
            question3.Reactions = question3Reactions;

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> receivedQuestions = mockService.GetAllQuestions().OrderBy(question => question.Score()).ToList();
            List<IQuestion> receivedSortedQuestions = mockService.GetQuestionsSortedByScoreAscending();
            Assert.That(receivedQuestions, Is.EqualTo(receivedSortedQuestions));

            Assert.That(receivedQuestions, Is.Not.Null);
            Assert.That(receivedSortedQuestions, Is.Not.Null);
            Assert.That(receivedQuestions, Is.Not.Empty);
            Assert.That(receivedSortedQuestions, Is.Not.Empty);
            Assert.That(receivedQuestions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(receivedSortedQuestions, Is.InstanceOf<IEnumerable<IQuestion>>());
            //Assert.That(receivedQuestions[0], Is.EqualTo(receivedSortedQuestions[0]));
        }

        [Test]
        public void GetQuestionsSortedByScoreDescending_QuestionsAndReactionsProvided_ReturnsQuestionsSortedByScoreAscending()
        {
            Reaction reaction1 = new();
            reaction1.Value = 10;
            Reaction reaction2 = new();
            reaction2.Value = 50;
            Reaction reaction3 = new();
            reaction3.Value = 100;

            List<IReaction> question1Reactions = new List<IReaction> { reaction1, reaction2, reaction3 };  // Score = 160
            List<IReaction> question2Reactions = new List<IReaction> { reaction2, reaction2, reaction2 };  // Score = 150
            List<IReaction> question3Reactions = new List<IReaction> { reaction1, reaction1, reaction3 };  // Score = 120

            Question question1 = new ();
            question1.ID = 11;
            question1.Reactions = question1Reactions;
            Question question2 = new ();
            question2.ID = 22;
            question2.Reactions = question2Reactions;
            Question question3 = new ();
            question3.ID = 33;
            question3.Reactions = question3Reactions;

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> receivedQuestions = mockService.GetAllQuestions();
            List<IQuestion> receivedSortedQuestions = mockService.GetQuestionsSortedByScoreDescending();
            //Assert.That(receivedQuestions[0], Is.EqualTo(receivedSortedQuestions[0]));
        }

        [Test]
        public void SortQuestionsByNumberOfAnswersAscending_QuestionsProvided_ReturnsQuestionsSortedBYNumberOfAnswersAscending()
        {
            Category questionCategory1 = new();
            questionCategory1.Name = "jmechereala la info";
            IQuestion question1 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(11)
                                        .SetTitle("Numar elemente lista C#")
                                        .SetContent("Cum aflii numarul de elemente dintr-o lista in C#?")
                                        .SetCategory(questionCategory1)
                                        .End();

            Category questionCategory2 = new();
            questionCategory2.Name = "decizii";
            IQuestion question2 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(22)
                                        .SetTitle("Mancare seara asta")
                                        .SetContent("Ce comandam in seara asta de mancare?")
                                        .SetCategory(questionCategory2)
                                        .End();

            Category questionCategory3 = new();
            questionCategory3.Name = "decizii";
            IQuestion question3 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(33)
                                        .SetTitle("Locatie seara asta")
                                        .SetContent("Unde mergem in seara asta?")
                                        .SetCategory(questionCategory2)
                                        .End();

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> x = mockService.GetAllQuestions();
        }

        [Test]
        public void SortQuestionsByNumberOfAnswersDescending__QuestionsProvided_ReturnsQuestionsSortedBYNumberOfAnswersDescending()
        {
            Category questionCategory1 = new();
            questionCategory1.Name = "jmechereala la info";
            IQuestion question1 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(11)
                                        .SetTitle("Numar elemente lista C#")
                                        .SetContent("Cum aflii numarul de elemente dintr-o lista in C#?")
                                        .SetCategory(questionCategory1)
                                        .End();

            Category questionCategory2 = new();
            questionCategory2.Name = "decizii";
            IQuestion question2 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(22)
                                        .SetTitle("Mancare seara asta")
                                        .SetContent("Ce comandam in seara asta de mancare?")
                                        .SetCategory(questionCategory2)
                                        .End();

            Category questionCategory3 = new();
            questionCategory3.Name = "decizii";
            IQuestion question3 = new QuestionBuilder()
                                        .Begin()
                                        .SetId(33)
                                        .SetTitle("Locatie seara asta")
                                        .SetContent("Unde mergem in seara asta?")
                                        .SetCategory(questionCategory2)
                                        .End();

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> x = mockService.GetAllQuestions();
        }

        [Test]
        public void SortQuestionsByDateAscending_QuestionsProvided_ReturnsSortedQuestionsByDateAscending()
        {
            IQuestion question1 = new QuestionBuilder().Begin().SetId(11).SetPostTime(DateTime.Now.AddHours(-5)).End();
            IQuestion question2 = new QuestionBuilder().Begin().SetId(22).SetPostTime(DateTime.Now).End();
            IQuestion question3 = new QuestionBuilder().Begin().SetId(33).SetPostTime(DateTime.Now.AddHours(-1)).End();

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> sortedQuestions = mockService.SortQuestionsByDateAscending();
            List<IQuestion> expectedQuestionsOrder = new List<IQuestion> { question1, question3, question2 };
            
            Assert.That(sortedQuestions, Is.Not.Null);
            Assert.That(sortedQuestions, Is.InstanceOf<IEnumerable<IQuestion>>());
        }

        [Test]
        public void SortQuestionsByDateDescending_QuestionsProvided_ReturnsSortedQuestionsByDateDescending()
        {
            Question question1 = new Question();
            question1.ID = 11;
            question1.DatePosted = DateTime.Now.AddHours(-5);
            Question question2 = new Question();
            question2.ID = 22;
            question2.DatePosted = DateTime.Now;
            Question question3 = new Question();
            question3.ID = 33;
            question3.DatePosted = DateTime.Now.AddHours(-1);

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            List<IQuestion> sortedQuestions = mockService.SortQuestionsByDateAscending();

            List<IQuestion> expectedQuestionsOrder = new List<IQuestion> { question2, question3, question1 };


            Assert.That(sortedQuestions, Is.Not.Null);
            Assert.That(sortedQuestions, Is.InstanceOf<IEnumerable<IQuestion>>());
            // Assert.That(sortedQuestions[0], Is.EqualTo(expectedQuestionsOrder[0]));
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
        public void GetAllCategorie_CategoriesProvided_RetursCategories()
        {
            Category category1 = new();
            category1.ID = 11;
            Category category2 = new();
            category2.ID = 22;
            Category category3 = new();
            category3.ID = 33;

            mockRepository.AddCategory(category1);
            mockRepository.AddCategory(category2);
            mockRepository.AddCategory(category3);

            List<ICategory> categories = mockService.GetAllCategories();

            Assert.That(categories, Is.Not.Null);
            Assert.That(categories, Is.InstanceOf<IEnumerable<ICategory>>());
            Assert.That(categories.Count, Is.GreaterThanOrEqualTo(3));
        }

        [Test]
        public void GetCurrentQuestions_QuestionsProvided_ReturnsCurrentQuestions()
        {
            // assuming that "current" means @Today
            IQuestion question1 = new Question();
            IQuestion question2 = new Question();
            question2.DatePosted = DateTime.Now.AddDays(-1);

            //mockService.AddQuestionByObject(question1);
            //mockService.AddQuestionByObject(question2);

            List<IQuestion> questions = mockService.GetCurrentQuestions();

            Assert.That(questions, Is.Not.Null);
            Assert.That(questions, Is.InstanceOf<IEnumerable<IQuestion>>());
            Assert.That(questions.Count, Is.Not.EqualTo(0));
        }

        [Test]
        public void GetAnswersOfUser_UsersAndAnswersProvided_ReturnsAnswersOfUsers()
        {
            IUser user1 = new User();
            user1.ID = 1337;
            IUser user2 = new User();
            user2.ID = 1338;

            IAnswer answer1ForUser1 = new Answer();
            answer1ForUser1.ID = 11;
            answer1ForUser1.UserID = user1.ID;
            IAnswer answer2ForUser1 = new Answer();
            answer2ForUser1.ID = 22;
            answer2ForUser1.UserID = user1.ID;
            IAnswer answer3ForUser1 = new Answer();
            answer3ForUser1.ID = 33;
            answer3ForUser1.UserID = user1.ID;

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
            question1.ID = 11;
            question1.UserID = user1.ID;
            Question question2 = new Question();
            question2.ID = 22;
            question2.UserID = user1.ID;
            Question question3 = new Question();
            question3.ID = 33;
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
            User user1 = new ();
            user1.ID = 1337;
            User user2 = new ();
            user2.ID = 1338;

            Comment comment1 = new ();
            comment1.ID = 11;
            comment1.UserID = user1.ID;
            Comment comment2 = new ();
            comment2.ID = 22;
            comment2.UserID = user1.ID;
            Comment comment3 = new ();
            comment3.ID = 33;
            comment3.UserID = user1.ID;

            mockRepository.AddUser(user1);
            mockRepository.AddUser(user2);
            mockService.AddPost(comment1);
            mockService.AddPost(comment2);
            mockService.AddPost(comment3);

            List<IComment> user1Comments = mockService.GetCommentsOfUser(user1.ID);
            List<IComment> user2Comments = mockService.GetCommentsOfUser(user2.ID);

            Assert.That(user1Comments, Is.Not.Null);
            Assert.That(user1Comments, Is.InstanceOf<IEnumerable<IComment>>());
            Assert.That(user1Comments.Count, Is.GreaterThanOrEqualTo(3));
            Assert.That(user2Comments, Is.Not.Null);
            Assert.That(user2Comments, Is.InstanceOf<IEnumerable<IComment>>());
            Assert.That(user2Comments.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetTagsOfQuestion_QuestionsWithAndWithoutTagsProvided_ReturnsQuestionsTags()
        {
            Tag tag1 = new Tag();
            Question question1 = new Question();
            question1.ID = 1337;
            List<ITag> question1Tags = new List<ITag> { tag1 };
            question1.Tags = question1Tags;

            Question question2 = new Question();
            question2.ID = 1338;

            mockRepository.AddQuestion(question1);
            mockRepository.AddQuestion(question2);

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
            Badge badge = new Badge();
            User user1 = new User();
            user1.ID = 1337;
            badge.ID = user1.ID;
            List<IBadge> user1Badges = new List<IBadge> { badge };
            user1.BadgeList = user1Badges;

            User user2 = new User();
            user2.ID = 1338;

            mockRepository.AddUser(user1);
            mockRepository.AddUser(user2);
            mockRepository.AddBadge(badge, user1.ID);

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
            question1.ID = 11;
            question1.DatePosted = DateTime.Now;

            IQuestion question2 = new Question();
            question2.ID = 22;
            question2.DatePosted = DateTime.Now;

            IQuestion question3 = new Question();
            question3.ID = 33;
            question3.DatePosted = DateTime.Now.AddDays(-8);

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            int questionsByLast7DaysCount = mockService.FilterQuestionsByLast7Days();

            Assert.That(questionsByLast7DaysCount, Is.EqualTo(2));
        }

        [Test]
        public void FilterQuestionsAnsweredThisMonth_()
        {
            Question question1 = new Question();
            question1.ID = 11;
            question1.DatePosted = DateTime.Now;

            Question question2 = new Question();
            question2.ID = 22;
            question2.DatePosted = DateTime.Now;

            Question question3 = new Question();
            question3.ID = 33;
            question3.DatePosted = DateTime.Now.AddMonths(-1);

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            int questionsByLast7DaysCount = mockService.FilterQuestionsAnsweredThisMonth();

            Assert.That(questionsByLast7DaysCount, Is.EqualTo(2));
        }

        [Test]
        public void FilterQuestionsAnsweredLastYear_()
        {
            Question question1 = new Question();
            question1.ID = 11;
            question1.DatePosted = DateTime.Now.AddYears(-1);

            Question question2 = new Question();
            question2.ID = 22;
            question2.DatePosted = DateTime.Now.AddYears(-1);

            Question question3 = new Question();
            question3.ID = 33;
            question3.DatePosted = DateTime.Now;

            mockService.AddQuestionByObject(question1);
            mockService.AddQuestionByObject(question2);
            mockService.AddQuestionByObject(question3);

            int questionsByLast7DaysCount = mockService.FilterQuestionsAnsweredLastYear();

            Assert.That(questionsByLast7DaysCount, Is.EqualTo(2));
        }
    }
}
