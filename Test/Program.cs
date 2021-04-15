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
            Print(s.AddBoard("Rafa@gmail.com", "semster B"));
            Print(s.Register("Rafa@gmail.com", "12345678"));
            Print(s.Register("Rafagmail.com", "Rf12345678"));
            Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            Print(s.AddBoard("Rada@gmail.com", "Semester B"));
            Print(s.AddBoard("Rafa@gmail.com", "Semester B"));
            Print(s.Login("Rafa@gmail.com", "Rf12345678"));
            Print(s.AddBoard("Rafa@gmail.com", "Semester B"));
            Print(s.AddBoard("Rafa@gmail.com", "board1"));
            Print(s.AddBoard("Rafa@gmail.com", "board1"));
            Print(s.AddTask("Rafa@gmail.com", "board1", "title1", "no description", DateTime.Now));
            Print(s.AddTask("Rafa@gmail.com", "board1", "title1", "no description", DateTime.Now));
        }


        static void Print(Response res)
        {
            string input = "";
            if (res.ErrorOccured)
                input = res.ErrorMessage;
            else
                input = "no error  "+ res.GetType().Name;
            Console.WriteLine($"\n    -  ----------   {input}   ----------   -\n");
        }
    }
}
