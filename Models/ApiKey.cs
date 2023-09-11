namespace Rumrejsen.Models
{
    public class ApiKey
    {
        public int Id { get; set; }
        public string ApiKeyValue { get; set; }
        public bool IsCaptain { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<DateTime> RequestContainer { get; set; }
    }
}
