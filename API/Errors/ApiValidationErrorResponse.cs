using System.Collections.Generic;

namespace API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        // Here we are just passing the status code into the base class. It is a fixed status code as
        // that is the exact code that we need for this exception
        public ApiValidationErrorResponse() : base(400)
        {

        }

         public IEnumerable<string> Errors { get; set; }
    }
}