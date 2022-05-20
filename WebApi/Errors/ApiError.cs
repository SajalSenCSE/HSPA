using System.Text.Json;

namespace WebApi.Errors
{
    public class ApiError
    {
        public ApiError(int errorCode, string messahe, string errorDetails=null)
        {
            ErrorCode = errorCode;
            Messahe = messahe;
            ErrorDetails = errorDetails;
        }

        public int ErrorCode { get; set; }
        public string Messahe { get; set; }
        public string ErrorDetails { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}