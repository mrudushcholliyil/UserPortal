namespace UserPortal.Application.Common.Exceptions
{
    /// <summary>
    /// Class representing Custom exception for BadRequest.
    /// </summary>
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
