using System;
using XemsPasswordHash;

namespace PasswordHasherTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var hasher = new PasswordHashService();

            Console.Write("Password : ");

            var password = Console.ReadLine();
            var passwordHash = hasher.HashPassword(password);
            
            Console.WriteLine($"Password Hash: {passwordHash}");
            Console.ReadLine();
        }
    }
}
