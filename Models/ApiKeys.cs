namespace Rumrejsen.Models
{
    /// <summary>
    /// This should be in a database, with keys hashed and salted. 
    /// </summary>
    public static class ApiKeys
    {
        public static List<ApiKey> apiKeys = new List<ApiKey> {
            new ApiKey {
                Id = 0,
                ApiKeyValue = "ioH7QzFHJx4w46fIh5Uzi4RvtTwlEXp",
                ExpirationDate = DateTime.Now.AddDays(1),
                IsCaptain = true,
                RequestContainer = new List<DateTime>()
            },
            new ApiKey
            {
                Id = 1,
                ApiKeyValue = "tyH7QzFHJx4w46fIh5Uzi4RghTwlEXp",
                ExpirationDate = DateTime.Now.AddDays(1),
                IsCaptain = false,
                RequestContainer = new List<DateTime>()
            },
            new ApiKey
            {
                Id = 2,
                ApiKeyValue = "weH7QzFHJx4w46fIh5Uzi4RuiTwlEXp",
                ExpirationDate = DateTime.Now.AddDays(1),
                IsCaptain = false,
                RequestContainer = new List<DateTime>()
            }
        };
    }
}
