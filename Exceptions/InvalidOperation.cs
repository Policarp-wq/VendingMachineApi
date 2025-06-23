namespace VendingMachineApi.Exceptions
{
    public class InvalidOperation : ServerException
    {
        public InvalidOperation(string message) : base(message, StatusCodes.Status403Forbidden)
        {
        }
    }
}
