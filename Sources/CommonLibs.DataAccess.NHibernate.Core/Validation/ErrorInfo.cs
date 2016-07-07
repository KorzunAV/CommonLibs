namespace CommonLibs.DataAccess.NHibernate.Common.Validation
{
    public class ErrorInfo
    {
        public ErrorInfo() : this(string.Empty) { }

        public ErrorInfo(string errorMessage) : this(string.Empty, errorMessage)
        { }

        public ErrorInfo(string key, string errorMessage)
        {
            Key = key;
            ErrorMessage = errorMessage;
        }

        public string Key { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return string.Format("Key: {0}, ErrorMessage: {1}", Key, ErrorMessage);
        }
    }
}