namespace Common.Constants
{
    public class StringPattern
    {
        #region User
        public const string UserFirstName = @"^[a-zA-Z]([ a-zA-Z]{0,49})$";
        public const string UserLastName = @"^[a-zA-Z]([ a-zA-Z]{0,49})$";
        public const string UserPassword = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,16}$";
        public const string UserName = @"^[a-zA-Z]([a-zA-Z0-9]{5,49})$";
        #endregion
    }
}
