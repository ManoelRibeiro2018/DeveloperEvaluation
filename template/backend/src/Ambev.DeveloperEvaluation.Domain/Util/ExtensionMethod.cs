namespace Ambev.DeveloperEvaluation.Domain.Util
{
    public static class ExtensionMethod
    {
        public static string GenerateNumberOrder()
        {
            Random random = new(DateTime.Now.Millisecond);
            int randomNumber = random.Next(1000, 9999);
            return randomNumber.ToString();
        }
    }
}
