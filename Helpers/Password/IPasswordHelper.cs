namespace WebApp.Helpers
{
    public interface IPasswordHelper
    {
         string EncodePassword(string originPassword);
         //string DecodePassword(string encodePassword);
         bool ComparePassword(string password, string dbPassword);
    }
}