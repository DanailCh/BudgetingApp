using App.Core.Contracts;
using App.Core.Enum;
using App.Core.Models.FeedbackMessage;
using App.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Test.UnitTests
{
    [TestFixture]
    public class FeedbackMessageTests : UnitTestsBase
    {
        private IFeedBackMessageService feedbackMessageService;

        [OneTimeSetUp]
        public void SetUp() => feedbackMessageService = new FeedbackMessageService(_data);

        [Test]
        public async Task MessageExists_ShouldReturnCorrectBool()
        {
            var result = await feedbackMessageService.MessageExistsAsync(Feedback1.Id);
            var resultFalse = await feedbackMessageService.MessageExistsAsync(1000);
            Assert.IsTrue(result);
            Assert.IsFalse(resultFalse);
        }
        [Test]
        public async Task GetAllMessages_ShouldReturnCorrectMessages()
        {

            var result = await feedbackMessageService.GetAllMessagesAsync(Guest2.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            var message = result.FirstOrDefault(m => m.Id == Feedback5.Id);
            Assert.That(message, Is.Not.Null);

        }

        [Test]
        public async Task AdminGetAllMessages_ShouldReturnCorrectMessagesCount()
        {
            AllFeedbackQueryModel model = new AllFeedbackQueryModel();
            var result = await feedbackMessageService.AdminGetAllMessagesAsync(model);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.MessagesCount, Is.EqualTo(5));
            Assert.That(result.Messages.Count, Is.EqualTo(5));

            AllFeedbackQueryModel model2 = new AllFeedbackQueryModel()
            {
                SeverityTypeId = 3,
                StatusId = 1,
            };

            var result2 = await feedbackMessageService.AdminGetAllMessagesAsync(model2);
            Assert.That(result2, Is.Not.Null);
            Assert.That(result2.MessagesCount, Is.EqualTo(1));
            Assert.That(result2.Messages.Count, Is.EqualTo(1));

            AllFeedbackQueryModel model3 = new AllFeedbackQueryModel()
            {
                SeverityTypeId = 2,
                StatusId = 2,
            };

            var result3 = await feedbackMessageService.AdminGetAllMessagesAsync(model3);
            Assert.That(result3, Is.Not.Null);
            Assert.That(result3.MessagesCount, Is.EqualTo(0));
            Assert.That(result3.Messages.Count, Is.EqualTo(0));

            AllFeedbackQueryModel model4 = new AllFeedbackQueryModel()
            {
                SeverityTypeId = 0,
                StatusId = 2,
            };

            var result4 = await feedbackMessageService.AdminGetAllMessagesAsync(model4);
            Assert.That(result4, Is.Not.Null);
            Assert.That(result4.MessagesCount, Is.EqualTo(2));
            Assert.That(result4.Messages.Count, Is.EqualTo(2));
        }
        [Test]
        public async Task AdminGetAllMessages_ShouldReturnMessagesInCorrectOrder()
        {
            AllFeedbackQueryModel model = new AllFeedbackQueryModel()
            {
                Sorting = MessageSorting.DateAscending
            };
            var result = await feedbackMessageService.AdminGetAllMessagesAsync(model);
            Assert.That(result, Is.Not.Null);
            var ms = result.Messages.Last();
            Assert.That(ms.Id, Is.EqualTo(1));


            AllFeedbackQueryModel model2 = new AllFeedbackQueryModel()
            {
                Sorting = MessageSorting.DateDescending
            };
            var result2 = await feedbackMessageService.AdminGetAllMessagesAsync(model2);
            Assert.That(result2, Is.Not.Null);
            var ms2 = result2.Messages.First();
            Assert.That(ms2.Id, Is.EqualTo(1));


        }

        [Test]
        public async Task CreateMessage_ShouldCreateMesssage()
        {
            var messageModel = new FeedbackMessageFormModel()
            {
                Title="TitleTest",
                Content="ContentTest",                
            };
            var msCountBefore = _data.FeedbackMessages.Count();
            await feedbackMessageService.CreateMessageAsync(messageModel, Guest.Id);
            var msCountAfter = _data.FeedbackMessages.Count();
            Assert.That(msCountBefore, Is.LessThan(msCountAfter));
            var ms = _data.FeedbackMessages.Where(ms => ms.Title == "TitleTest").FirstOrDefault();
            Assert.That(ms, Is.Not.Null);
            Assert.That(ms.SenderId,Is.EqualTo(Guest.Id));
            Assert.That(ms.Content, Is.EqualTo("ContentTest"));
            Assert.That(ms.Comment, Is.EqualTo(String.Empty));
            Assert.That(ms.SeverityTypeId, Is.Null);
            Assert.That(ms.StatusId, Is.EqualTo(1));
            Assert.That(ms.IsReadByUser,Is.EqualTo(true));

        }

        [Test]
        public async Task SetSeverityType_ShouldSetCorrectType()
        {
            

        }
    }
}
