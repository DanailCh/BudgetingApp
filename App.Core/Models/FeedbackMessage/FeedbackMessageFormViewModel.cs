namespace App.Core.Models.FeedbackMessage
{
    public class FeedbackMessageFormViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; }
        public string SenderUsername { get; set; } = string.Empty;
        public int SeverityId { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
        public int StatusId { get; set; }

        public IEnumerable<SeverityTypeViewModel> SeverityTypes { get; set; } = Enumerable.Empty<SeverityTypeViewModel>();
        public IEnumerable<StatusViewModel> Statuses { get; set; } = Enumerable.Empty<StatusViewModel>();

    }
}
