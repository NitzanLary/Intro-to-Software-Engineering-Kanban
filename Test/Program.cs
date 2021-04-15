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
            init(s);
            Task(s);

           ////Print(s.AddBoard("Rafa@gmail.com", "semster B"));
           // //Print(s.Register("Rafa@gmail.com", "12345678"));
           // //Print(s.Register("Rafagmail.com", "Rf12345678"));
           // Print(s.Register("Rafa@gmail.com", "Rf12345678"));
           // //Print(s.Register("Rafa@gmail.com", "Rf12345678"));
           // //Print(s.Register("Rafa111111@gmail.com", "R12345678"));
           // //Print(s.Register("Rafa111111@gmail.com", "ff12345678"));
           // //Print(s.Register("Rafa111111@gmail.com", "RF12345678"));
           // //Print(s.Register("Rafa111111@gmail.com", "Rf2"));
           // //Print(s.Register("Rafa111111@gmail.com", "R12345678222222222222222222222222222222222f"));
           // //Print(s.Register("Rafa111111@gmail.com", "R12345678ש"));
           // Print(s.Register("Rafa111111@gmail.com", "R12345678f"));
           // //Print(s.Login("Rafa@gmail.com", "R12345678f"));
           // Print(s.AddBoard("Rafa@gmail.com", "semester B"));
           // Print(s.Login("Rafa@gmail.com", "Rf12345678"));
           // //Print(s.Login("Rafa123@gmail.com", "Rf12345678"));
           // Print(s.Login("Rafa111111@gmail.com", "R12345678f"));
           // //Print(s.Logout("Rafa@gmailcom"));
           // //Print(s.Logout("Rafa@gmail.com"));

           // //Print(s.Logout("Rafagmail.com"));
           // //Print(s.Logout("Rada@gmail.com"));
           // //Print(s.Logout("Rafa111111@gmail.com"));

           // // --------------------- AddBoard Tests -----------------------
           // Print(s.AddBoard("Rafa@gmail.com", "semester B"));
           // Print(s.AddBoard("Rafa@gmail.com", "semester A"));
           // Print(s.AddBoard("Rafa111111@gmail.com", "semester B"));

           // // --------------------- AddTask Tests -----------------------

           // Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));
           // //Console.WriteLine(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)).Value.Id);
           // Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "", new DateTime(2021, 5, 14, 0, 0, 0))); // possible the same name for task???
           // //Console.WriteLine(s.AddTask("Rafa@gmail.com", "semester B", "SE", "", new DateTime(2021, 5, 14, 0, 0, 0)).Value.Id);
           // Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 2", new DateTime(2021, 5, 14, 0, 0, 0)));
           // //Console.WriteLine(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 2", new DateTime(2021, 5, 14, 0, 0, 0)).Value.Id);
           // Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 3", new DateTime(2021, 5, 14, 0, 0, 0))); 
           // Print(s.AddTask("Rafa@gmail.com", "semester A", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0))); 
           // Print(s.AddTask("Rafa111111@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));
           // //Print(s.AddTask("a1@gmail.com", "semester B", "SE", "test", new DateTime(2021, 5, 14, 0, 0, 0)));
           // //Print(s.AddTask("Rafa111111@gmail.com", "semester C", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));

           // // --------------------- AdvenceTask Tests -----------------------
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 0, 10)); // EROR ID not exsit
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 0, 1));
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 1, 2));
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 1, 1));
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 1, 1));
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 2, 1));
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester B", 0, 3));
           // //Print(s.AdvanceTask("Rafa@gmail.com", "semester A", 0, 5));


           // //// --------------------- UpdateTask Tests ----------------
           // ////Print(s.UpdateTaskDescription("Rafa@gmail.com", "semester B", 0, 2, "1.1"));
           // //////Print(s.UpdateTaskDueDate("Rafa@gmail.com", "semester B", 0, 2, new DateTime(2021, 13, 14, 0, 0, 0))); // EROR invalid data 
           // ////Print(s.UpdateTaskDueDate("Rafa@gmail.com", "semester B", 0, 2, new DateTime(2021, 4, 14, 0, 0, 0))); // EROR passed date
           // ////Print(s.UpdateTaskDueDate("Rafa@gmail.com", "semester B", 0, 2, new DateTime(2021, 6, 15, 0, 0, 0)));
           // ////Print(s.UpdateTaskTitle("Rafa@gmail.com", "semester B", 0, 2, "")); // EROR canot be empty title
           // ////Print(s.UpdateTaskTitle("Rafa@gmail.com", "semester B", 0, 2, "semseter C"));

           // //// --------------------- InProgress Tests ----------------

           // //Console.WriteLine(s.InProgressTasks("Rafa@gmail.com").Value[0].Id);
           // //Console.WriteLine(s.InProgressTasks("Rafa@gmail.com").Value[1].Id);
           // ////Console.WriteLine(s.InProgressTasks("Rafa@gmail.com").Value[2].Id); // EROR 









           // //Console.WriteLine(s.AddBoard("Rada@gmail.com", "Semester B").ErrorMessage);
           // //Console.WriteLine(s.AddBoard("Rafa@gmail.com", "Semester B").ErrorMessage);
           // //Console.WriteLine(s.Login("Rafa@gmail.com", "Rf12345678").ErrorMessage);
           // //Console.WriteLine(s.AddBoard("Rafa@gmail.com", "Semester B").ErrorMessage);

            //// ------------- Asaf Tests -------------
            Console.WriteLine(" \n\n Asaf Tests \n\n");
            Print(s.Register("asafs@gmail.com", "Aa123"));
            Console.WriteLine("\t\tshould print ok\n");
            Print(s.Login("asafs@gmail.com", "Aa123"));
            Console.WriteLine("\t\tshould print ok\n");
            Print(s.Login("asafs@gmail.com", "Aa123"));
            Console.WriteLine("\t\tshould print not ok\n");
            Print(s.Login("asafs@gmail.com", "Aa1234"));
            Console.WriteLine("\t\tshould print not ok\n");
            Print(s.AddBoard("asafs@gmail.com",""));
            Console.WriteLine("\t\tshould print not ok\n");
            Print(s.AddBoard("asafs@gmail.com", ""));
            Console.WriteLine("\t\tshould print not ok\n");
            Print(s.AddBoard("asafs@gmail.com", "b1"));
            Console.WriteLine("\t\tshould print ok\n");
            Print(s.AddBoard("asafs@gmail.com", "b1"));
            Console.WriteLine("\t\tshould print not ok\n");
            Print(s.AddBoard("asafs@gmail.com", "b2"));
            Console.WriteLine("\t\tshould print ok\n");
            Print(s.AddTask("asafs@gmail.com", "", "", "", new DateTime()));
            Console.WriteLine("\t\tshould print not ok\n");
            Print(s.AddTask("asafs@gmail.com","b1", "", "", new DateTime()));
            Console.WriteLine("\t\tshould print not ok\n");
            Print(s.AddTask("asafs@gmail.com", "b1","title1","", DateTime.Parse("4-20-2021")));
            Console.WriteLine("\t\tshould print ok\n");
            Print(s.AddTask("asafs@gmail.com", "b1", "title1", "123", DateTime.Parse("4-20-2021")));
            Console.WriteLine("\t\tshould print ok\n");
            Print(s.)

        }
        static void Print(Response res)
        {
            string input = "";
            if (res.ErrorOccured)
                input = res.ErrorMessage;
            else
                input = "no error  " + res.GetType().Name;
            Console.WriteLine($"\n    -  ----------   {input}   ----------   -\n");
        }

        static void init(Service s)
        {
            s.Register("Rafa@gmail.com", "Rf12345678");
            s.Login("Rafa@gmail.com", "Rf12345678");
        }

        static void Register(Service s)
        {

        }

        static void Task(Service s)
        {
            DateTime date = new DateTime(2021, 5, 16, 0, 0, 0);
            string mail = "Rafa@gmail.com";
            validation(s.AddBoard("Rafa@gmail.com", null), true);
            validation(s.AddBoard("Rafa@gmail.com", "myBoard"), false);
            validation(s.AddBoard("Rafa@gmail.com", "myBoard"), true);
            validation(s.AddBoard("Rafa@gmail.com", "myBoard2"), false);
            validation(s.AddTask(mail, null, "myTitle", "myDesc", date), true);
            validation(s.AddTask(mail, "myBoard", null, "myDesc", date), true);
            Response<Task> rB1T1 = s.AddTask(mail, "myBoard", "myTitle1", "myDesc1", date);
            Response<Task> rB1T2 = s.AddTask(mail, "myBoard", "myTitle2", "myDesc2", date);
            Response<Task> rB1T3 = s.AddTask(mail, "myBoard", "myTitle3", "myDesc3", date);
            Response<Task> rB2T1 = s.AddTask(mail, "myBoard2", "myTitle1", "myDesc1", date);
            validation(rB1T1, false); validation(rB1T2, false); validation(rB1T3, false); validation(rB2T1, false);
            validation(s.LimitColumn(mail, "myBoard", 0, 1), true);
            validation(s.LimitColumn(mail, "myBoard", 0, 3), false);
            validation(s.AddTask(mail, "myBoard", "myTitle4", "myDesc4", date), true);



        }

        static void Column(Service s)
        {

        }

        static void validation(Response r, bool expected)
        {
            if (r.ErrorOccured != expected)
                throw new Exception();
        }
    }
}
