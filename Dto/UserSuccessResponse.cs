namespace UploadandDowloadService.Services
{
    public class UserSuccessResponse
    {
        private readonly string email;
        private readonly string token;
        private readonly string username;

        public UserSuccessResponse(string Email, string Token, string Username)
       {
            email = Email;
            token = Token;
            username = Username;
        }

    
      public string Token
      {
          get { return token; }
      }

      public string Email
      {
          get { return email; }
      }

      public string UserName
      {
          get { return username; }
      }
      
    }
}