using System;
namespace Blue_Harvest_Redis.CustomException
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException()
        {
        }

        public CustomerNotFoundException(string message)
            : base(message)
        {
        }

        public CustomerNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
