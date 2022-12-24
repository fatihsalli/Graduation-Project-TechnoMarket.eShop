namespace TechnoMarket.AuthServer.Settings
{
    //Bu classı AppUser kullanmadığımız Apiler için oluşturduk.
    public class Client
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        //Hangi Api'lere erişebileceğini seçmek için. //www.myapi1.com www.myapi2.com gibi. Payloadda Auidiences olarak göstereceğiz.
        public List<string> Audiences { get; set; }

    }
}
