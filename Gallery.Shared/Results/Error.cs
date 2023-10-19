using System.Net;

namespace Gallery.Shared.Results
{
    public class Error
    {
        public HttpStatusCode Code { get; set; }

        public static readonly Error None = new();
        public static readonly Error Deleted = new(HttpStatusCode.NoContent);


        public Error() => 
            (Code) = (HttpStatusCode.NoContent);

        public Error(HttpStatusCode code)
        {
            Code = code;
        }
    }
}
