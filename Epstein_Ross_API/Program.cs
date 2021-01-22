using System;

namespace Epstein_Ross_API
{
    class Program
    {
        static void Main()
        {
            Application app = new Application();
        }

        public static void Exit()
        {
            if (Application.hasQuit == true) 
            {
                Console.WriteLine("You have executed Order 66.  Have a great day!");
                return;
            }
        }
    }
}
