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
            var expectedMessage = _data.FeedbackMessages.Where(m => m.SenderId == Guest2.Id && m.IsReadByUser == false).FirstOrDefault(m => m.Id == Feedback5.Id);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            var resultMessage = result.FirstOrDefault(m => m.Id == Feedback5.Id);
            Assert.That(resultMessage, Is.Not.Null);
            Assert.That(resultMessage.Content,Is.EqualTo(expectedMessage.Content));
            Assert.That(resultMessage.Comment, Is.EqualTo(expectedMessage.Comment));
            Assert.That(resultMessage.Title, Is.EqualTo(expectedMessage.Title));
            Assert.That(resultMessage.Date, Is.EqualTo(expectedMessage.Date));


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
                Title = "TitleTest",
                Content = "ContentTest",
            };
            var msCountBefore = _data.FeedbackMessages.Count();
            await feedbackMessageService.CreateMessageAsync(messageModel, Guest.Id);
            var msCountAfter = _data.FeedbackMessages.Count();
            Assert.That(msCountBefore, Is.LessThan(msCountAfter));
            var ms = _data.FeedbackMessages.Where(ms => ms.Title == "TitleTest").FirstOrDefault();
            Assert.That(ms, Is.Not.Null);
            Assert.That(ms.Title, Is.EqualTo("TitleTest"));
            Assert.That(ms.SenderId, Is.EqualTo(Guest.Id));
            Assert.That(ms.Content, Is.EqualTo("ContentTest"));
            Assert.That(ms.Comment, Is.EqualTo(String.Empty));
            Assert.That(ms.SeverityTypeId, Is.Null);
            Assert.That(ms.StatusId, Is.EqualTo(1));
            Assert.That(ms.IsReadByUser, Is.EqualTo(true));

        }

        [Test]
        public async Task SetSeverityType_ShouldSetCorrectType()
        {

            await feedbackMessageService.SetSeverityTypeOnMessageAsync(1, 2);
            var messageAfter = _data.FeedbackMessages.Find(1);
            Assert.That(messageAfter.SeverityTypeId, Is.EqualTo(2));
            Assert.That(messageAfter.StatusId, Is.EqualTo(2));
            Assert.That(messageAfter.Comment, Is.Not.EqualTo(String.Empty));
            Assert.That(messageAfter.IsReadByUser, Is.False);
        }
        [Test]
        public async Task SetSeverityType_ShouldntDoAnything()
        {
            var messageBefore = _data.FeedbackMessages.Find(2);
            await feedbackMessageService.SetSeverityTypeOnMessageAsync(2, 2);
            var messageAfter = _data.FeedbackMessages.Find(2);
            Assert.That(messageAfter, Is.EqualTo(messageBefore));
        }

        [Test]
        public async Task GetSeverityTypes_ShouldReturnAllTypes()
        {
            var expectedResult = _data.SeverityTypes.ToList();
            var expectedSeverity = expectedResult.Where(s => s.Id == 2).First();
            var result = await feedbackMessageService.GetSeverityTypesAsync();
            var severity = result.Where(s => s.Id == 2).First();
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));
            Assert.That(severity.Name, Is.EqualTo(expectedSeverity.Name));
        }

        [Test]
        public async Task SeverityTypeExists_ShouldReturnCorrectBool()
        {
            bool result = await feedbackMessageService.SeverityTypeExistsAsync(1);
            bool result2 = await feedbackMessageService.SeverityTypeExistsAsync(10);
            Assert.That(result, Is.True);
            Assert.That(result2, Is.False);
        }
        [Test]
        public async Task GetStatuses_ShouldReturnAllStatuses()
        {

            var result = await feedbackMessageService.GetStatusesAsync();
            var status = result.Where(s => s.Id == 2).First();
            var expectedResult = _data.Statuses.ToList();
            var expectedStatus = expectedResult.Where(s => s.Id == 2).First();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(expectedResult.Count));
            Assert.That(status.Name, Is.EqualTo(expectedStatus.Name));
        }

        [Test]
        public async Task SetDoneStatusOnMessage_ShouldSetCorrectStatus()
        {
            int statusIdBefore = _data.FeedbackMessages.Where(m=>m.Id==2).Select(m=>m.StatusId).First();            
           await feedbackMessageService.SetDoneStatusOnMessageAsync(2);
            int statusIdAfter = _data.FeedbackMessages.Where(m => m.Id == 2).Select(m => m.StatusId).First();
            Assert.That(statusIdBefore,Is.Not.EqualTo(statusIdAfter));
        }
        [Test]
        public async Task SetDoneStatusOnMessage_ShouldDoNothing()
        {
            int statusIdBefore = _data.FeedbackMessages.Where(m => m.Id == 4).Select(m => m.StatusId).First();
            await feedbackMessageService.SetDoneStatusOnMessageAsync(4);
            int statusIdAfter = _data.FeedbackMessages.Where(m => m.Id == 4).Select(m => m.StatusId).First();
            Assert.That(statusIdBefore, Is.EqualTo(statusIdAfter));
        }

        [Test]
        public async Task RemoveMessage_ShouldSetCorrectBoolForCorrectMessage()
        {
            bool msBefore = _data.FeedbackMessages.Where(m=>m.Id==2).Select(m=>m.IsReadByUser).First();
            Assert.IsFalse(msBefore);
            await feedbackMessageService.RemoveMessageAsync(2);
            bool msAfter = _data.FeedbackMessages.Where(m => m.Id == 2).Select(m => m.IsReadByUser).First();
           Assert.IsTrue(msAfter);
        }

        [Test]
        public void GetTextColor_ShouldRetirnCorrectColor()
        {
            var expected = "info";
            var result=feedbackMessageService.GetTextColor("Low");
            Assert.That(result, Is.EqualTo(expected));
            var expected2 = "warning";
            var result2 = feedbackMessageService.GetTextColor("Medium");
            Assert.That(result2, Is.EqualTo(expected2));
            var expected3 = "danger";
            var result3 = feedbackMessageService.GetTextColor("High");
            Assert.That(result3, Is.EqualTo(expected3));
            var expected4 = "secondary";
            var result4 = feedbackMessageService.GetTextColor("New");
            Assert.That(result4, Is.EqualTo(expected4));
            var expected5 = "primary";
            var result5 = feedbackMessageService.GetTextColor("In Progress");
            Assert.That(result5, Is.EqualTo(expected5));
            var expected6 = "success";
            var result6 = feedbackMessageService.GetTextColor("Done");
            Assert.That(result6, Is.EqualTo(expected6));
            var expected7 = String.Empty;
            var result7 = feedbackMessageService.GetTextColor("other");
            Assert.That(result7, Is.EqualTo(expected7));
        }
    }
}
