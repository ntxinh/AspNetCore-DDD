namespace DDD.Domain.Services
{
    public class MailOptions
    {
        #region Variables
        public string from { get; set; }
        public string fromName { get; set; }
        public string to { get; set; }
        public string toName { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public string isPlainText { get; set; }

        #endregion
        public MailOptions()
        {
        }
    }
}
