namespace App.Core.Models.FeedbackMessage
{
    public class FeedbackMessageFormModel
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Content { get; set; }
        public int SeverityId { get; set; }
        public string? Comment { get; set; }

        public IEnumerable<SeverityTypeViewModel> SeverityTypes { get; set; } = Enumerable.Empty<SeverityTypeViewModel>();

    }
}
