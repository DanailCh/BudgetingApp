using App.Core.Models.FeedbackMessage;

namespace App.Core.Contracts
{
    public  interface IFeedBackMessageService
    {
        Task<IEnumerable<FeedbackMessageViewModel>> GetAllMessagesAsync(string userId);
        Task<IEnumerable<FeedbackMessageViewModel>> AdminGetAllMessagesAsync();
        Task CreateMessageAsync(FeedbackMessageFormModel model,string userId);
        Task AddCommentToMessageAsync(int id,string comment);
        Task UpdateSeverityStatusOnMessageAsync(int messageId,int severityId);
        Task <FeedbackMessageFormModel>FindMessageByIdAsync(int id);
        Task <IEnumerable<SeverityTypeViewModel>> GetSeverityTypesAsync();
        Task<bool> MessageExistsAsync(int id);
        Task<bool> SeverityTypeExistsAsync(int id);
    }
}
