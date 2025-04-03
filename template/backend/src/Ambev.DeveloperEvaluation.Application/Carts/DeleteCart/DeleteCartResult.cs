namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartResult
    {
        public string Message { get; set; }

        public DeleteCartResult(string message)
        {
            Message = message;
        }
    }
}
