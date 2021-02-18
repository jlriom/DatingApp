namespace API.Interfaces
{
    public class ImageUploadResult
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
        public ErrorResult Error { get; set; }
    }
}