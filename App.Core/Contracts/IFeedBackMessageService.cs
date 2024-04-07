using App.Core.Models.Archive.MemberSalary;
using App.Core.Models.FeedbackMessage;

namespace App.Core.Contracts
{
    public  interface IFeedBackMessageService
    {
        Task<IEnumerable<FeedbackMessageViewModel>> GetAllMessagesAsync(string userId);
        Task<FeedbackQueryModel> AdminGetAllMessagesAsync(AllFeedbackQueryModel model);
        Task CreateMessageAsync(FeedbackMessageFormModel model,string userId);
        Task RemoveMessageAsync(int id);
        
        Task SetSeverityTypeOnMessageAsync(int messageId,int severityId);
        Task SetDoneStatusOnMessageAsync(int messageId);
        
        Task<bool> MessageExistsAsync(int id);
        Task<bool> SeverityTypeExistsAsync(int id);

        Task<IEnumerable<StatusViewModel>> GetStatusesAsync();
        Task<IEnumerable<SeverityTypeViewModel>> GetSeverityTypesAsync();

        string GetTextColor(string input);
       
    }
}
