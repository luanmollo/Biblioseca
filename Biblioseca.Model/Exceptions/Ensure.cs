namespace Biblioseca.Model.Exceptions
{
    public static class Ensure
    {
        public static void IsTrue(bool condition, string message)
        {
            if (!condition)
            {
                ThrowException(message);
            }
        }

        public static void NotNull(object reference, string message)
        {
            if (reference == null)
            {
                ThrowException(message);
            }
        }

        private static void ThrowException(string message)
        {
            throw new BusinessRuleException(message);
        }
    }
}