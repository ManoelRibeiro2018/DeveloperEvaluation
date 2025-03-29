namespace Ambev.DeveloperEvaluation.Domain.Util
{
    public static class ExtensionMethod
    {
        public static string GenerateOrderNumber(this string timestamp)
        {
            int randomNumber = Random.Shared.Next(1000, 9999);
            return $"ORD-{timestamp}-{randomNumber}";
        }
    }
}
