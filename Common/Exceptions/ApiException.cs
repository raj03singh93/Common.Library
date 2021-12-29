using Common.Responses;

using System;
using System.Net;

namespace Common.Exceptions
{
    /// <summary>
    /// Base Api Exception class
    /// </summary>
    public class ApiException : Exception
    {
        #region Public Properties

        /// <summary>
        /// Api response object
        /// </summary>
        public ApiExceptionResponse Error { get; }

        /// <summary>
        /// Status code that can be used in the response body.
        /// Defaults to Status Code 422 - Unprocessable Entity
        /// </summary>
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.UnprocessableEntity;

        #endregion

        #region Constructor

        public ApiException(string message)
           : base(message)
        {

        }

        public ApiException(string message, HttpStatusCode statusCode)
           : base(message)
        {
            this.StatusCode = statusCode;
        }

        public ApiException(ApiExceptionResponse error) : base(error.ResponseMessage)
        {
            this.Error = error;
        }

        public ApiException(ApiExceptionResponse error, params object?[] args) : base(string.Format(error.ResponseMessage, args))
        {
            Error = new ApiExceptionResponse(error);

            this.Error.ResponseMessage = string.Format(Error.ResponseMessage, args);
            this.Error.ResponseDetails = string.IsNullOrEmpty(Error.ResponseDetails) ? null : string.Format(Error.ResponseDetails, args);
        }

        public ApiException(ApiExceptionResponse error, HttpStatusCode statusCode) : base(error.ResponseMessage)
        {
            this.Error = error;
            this.StatusCode = statusCode;
        }

        public ApiException(ApiExceptionResponse error, HttpStatusCode statusCode, params object?[] args) : base(string.Format(error.ResponseMessage, args))
        {
            this.StatusCode = statusCode;
            this.Error = new ApiExceptionResponse(error);

            this.Error.ResponseMessage = string.Format(Error.ResponseMessage, args);
            this.Error.ResponseDetails = string.IsNullOrEmpty(Error.ResponseDetails) ? null : string.Format(Error.ResponseDetails, args);
        }

        #endregion
    }
}
