namespace UserPortal.Application.Common.Exceptions
{
    /// <summary>
    /// Class representing Custom exception for NotFound.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
