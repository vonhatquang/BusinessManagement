namespace WebApp.Helpers
{
    public class PasswordHelper:IPasswordHelper
    {
        private readonly IStringHelper _stringHelper;
        public PasswordHelper(IStringHelper stringHelper){
            this._stringHelper = stringHelper;
        }
        public string EncodePassword(string originPassword){
            return this._stringHelper.EncodeString(this._stringHelper.EncodeString(originPassword));
        }

        public bool ComparePassword(string password, string dbPassword){
            return password.Equals(dbPassword);
        }
    }
}