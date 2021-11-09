namespace ThirdPartyApi.DTOs
{
    public class Login
    {
        public Login(string userName)
        {
            UserName = userName;
        }

        public int ID { get; set; }
        public string UserName { get; set; }
    }
}