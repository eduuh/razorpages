namespace Kaizen.Models
{
    public class UserSuccessResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }

        public UserSuccessResponse(string Email, string Token, string Username)
        {
            this.Email = Email;
            this.Token = Token;
            this.Username = Username;
        }


    }
}