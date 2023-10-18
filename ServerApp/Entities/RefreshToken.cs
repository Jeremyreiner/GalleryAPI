namespace GalleryAPI.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public User User { get; set; }
    }
}
