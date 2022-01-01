using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Common.Library.Responses
{
    /// <summary>
    /// The API Exceptiion Response
    /// </summary>
    [DataContract]
    public class ApiExceptionResponse
    {

        /// <summary>
        /// Response message
        /// </summary>
        [JsonPropertyName("Message")]
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Details about response
        /// </summary>
        [JsonPropertyName("Details")]
        public string ResponseDetails { get; set; }

        public const int MaxValuePerPage = 1000;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        public ApiExceptionResponse()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseCode">To initialize ResponseCode</param>
        /// <param name="responseMessage">To initialize ResponseMessage</param>
        public ApiExceptionResponse(string responseMessage)
        {
            this.ResponseMessage = responseMessage;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="responseCode">To initialize ResponseCode</param>
        /// <param name="responseMessage">To initialize ResponseMessage</param>
        /// <param name="responseDetails">To initialize ResponseDetails</param>
        public ApiExceptionResponse(string responseMessage, string responseDetails)
        {
            this.ResponseMessage = responseMessage;
            this.ResponseDetails = responseDetails;
        }

        /// <summary>
        /// Initializes ApiExceptiionResponse object
        /// </summary>
        /// <param name="apiResponse">ApiExceptiionResponse object</param>
        public ApiExceptionResponse(ApiExceptionResponse apiResponse)
        {
            this.ResponseMessage = apiResponse.ResponseMessage;
            this.ResponseDetails = apiResponse.ResponseDetails;
        }
    }
}
