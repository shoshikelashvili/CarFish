namespace CarFish.Models
{
    public class MailRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }

        public string ToEmail = "carfishstore@gmail.com";
    }
}
