namespace TechnoMarket.AuthServer.Dtos
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        //Token süresini veriyoruz. Bu payload içerisinde yer almaktadır. Encode edildiğinde ulaşılabilir ancak biz kolay ulaşabilmek adına yine de property olarak tekrar tanımladık.
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ResfreshTokenExpiration { get; set; }
    }
}
