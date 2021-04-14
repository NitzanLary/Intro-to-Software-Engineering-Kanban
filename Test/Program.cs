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
            print(s.AddBoard("Rafa@gmail.com", "semster B"));
            print(s.Register("Rafa@gmail.com", "12345678"));
            print(s.Register("Rafagmail.com", "Rf12345678"));
            print(s.Register("Rafa@gmail.com", "Rf12345678"));
            print(s.AddBoard("Rada@gmail.com", "Semester B"));
            print(s.AddBoard("Rafa@gmail.com", "Semester B"));
            print(s.Login("Rafa@gmail.com", "Rf12345678"));
            print(s.AddBoard("Rafa@gmail.com", "Semester B"));
            print(s.AddBoard("Rafa@gmail.com", "board1"));
            print(s.AddBoard("Rafa@gmail.com", "board1"));
            print(s.AddTask("Rafa@gmail.com", "board1", "title1", "no description", DateTime.Now));
            print(s.AddTask("Rafa@gmail.com", "board1", "title1", "no description", DateTime.Now));
        }


        static void print(Response res)
        {
            string input = "";
            if (res.ErrorOccured)
                input = res.ErrorMessage;
            else
                input = "passed  "+ res.GetType().Name;
            Console.WriteLine($"\n    -  ----------   {input}   ----------   -\n");
        }
    }
}
