namespace App.Core.Models.FeedbackMessage
{
    public class FeedbackMessageViewModel
    {    
        public int Id { get; set; }
        public string Title { get; set; }=string.Empty;
        public string Content { get; set; }=string.Empty;
        public DateTime Date { get; set; }       
        public string? Comment { get; set; } 
    }
}
