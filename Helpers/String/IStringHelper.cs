namespace WebApp.Helpers
{
    public interface IStringHelper
    {
         string EncodeString(string originString);
         string DecodeString(string encodeString);
    }
}