using IntroSE.Kanban.Backend.ServiceLayer;
using System;

namespace Test
{
    class Program
    {
        /*ervice service = new Service();*/

        static void Main(string[] args)
        {
            Service s = new Service();
            Console.WriteLine(s.AddBoard("Rafa@gmail.com", "semster B").ErrorMessage);
            Console.WriteLine(s.Register("Rafa@gmail.com", "12345678").ErrorMessage);
            Console.WriteLine(s.Register("Rafagmail.com", "Rf12345678").ErrorMessage);
            Console.WriteLine(s.Register("Rafa@gmail.com", "Rf12345678").ErrorMessage);
            Console.WriteLine(s.AddBoard("Rada@gmail.com", "Semester B").ErrorMessage);
            Console.WriteLine(s.AddBoard("Rafa@gmail.com", "Semester B").ErrorMessage);
            Console.WriteLine(s.Login("Rafa@gmail.com", "Rf12345678").ErrorMessage);
            Console.WriteLine(s.AddBoard("Rafa@gmail.com", "Semester B").ErrorMessage);






        }
    }
}
