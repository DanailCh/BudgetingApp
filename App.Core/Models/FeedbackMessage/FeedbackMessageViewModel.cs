namespace App.Core.Models.FeedbackMessage
{
    public class FeedbackMessageViewModel
    {
        public string SenderUsername { get; set; }=string.Empty;
        public string Title { get; set; }=string.Empty;
        public string Content { get; set; }=string.Empty;
        public DateTime Date { get; set; }
        public string Severity { get; set; }=string.Empty;
        public string? Comment { get; set; } 
    }
}
