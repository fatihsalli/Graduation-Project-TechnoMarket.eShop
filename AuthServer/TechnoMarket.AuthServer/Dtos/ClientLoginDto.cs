namespace TechnoMarket.AuthServer.Dtos
{
    public class ClientLoginDto
    {
        //Kullanıcı olmayan durumlarda "ClientId" bir nevi "LoginDto" daki email, "ClientSecret" ise "Password" gibi düşünebiliriz.
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
