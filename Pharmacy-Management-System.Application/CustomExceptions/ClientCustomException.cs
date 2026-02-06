namespace Pharmacy_Management_System.Application.CustomExceptions
{
    public class ClientCustomException(
            string message,
            Dictionary<string, object>? errorData = null
        ) : Exception(message)
    {
        private readonly Dictionary<string, object> _errorData = errorData ?? [];

        public Dictionary<string, object> GetErrorData()
        {
            return _errorData;
        }
    }
}
