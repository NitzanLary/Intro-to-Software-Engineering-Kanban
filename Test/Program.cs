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
            validation(s.LimitColumn(mail, "myBoard", 0, -1), true);
            Response<Task> rB1T1 = s.AddTask(mail, "myBoard", "myTitle1", "myDesc1", date);
            Task t = rB1T1.Value;
            assertTrue(t.Title, "myTitle1");
            assertTrue(t.Description, "myDesc1");
            assertTrue(t.DueDate, date);
            Response <Task> rB1T2 = s.AddTask(mail, "myBoard", "myTitle2", "myDesc2", date);
            Response<Task> rB1T3 = s.AddTask(mail, "myBoard", "myTitle3", "myDesc3", date);
            Response<Task> rB2T1 = s.AddTask(mail, "myBoard2", "myTitle1", "myDesc1", date);
            validation(rB1T1, false); validation(rB1T2, false); validation(rB1T3, false); validation(rB2T1, false);
            validation(s.LimitColumn(mail, "myBoard", 0, 1), true);
            validation(s.LimitColumn(mail, "myBoard", 0, 3), false);
            validation(s.LimitColumn(mail, "myBoard", 1, 1), false);
            validation(s.LimitColumn(mail, "myBoard", 2, 1), false);
            validation(s.AddTask(mail, "myBoard", "myTitle4", "myDesc4", date), true);
            assertTrue(s.GetColumnLimit(mail, "myBoard", 0).Value, 3);
            assertTrue(s.GetColumnLimit(mail, "myBoard", 1).Value, 1);
            assertTrue(s.GetColumnLimit(mail, "myBoard", 2).Value, 1);
            assertTrue(s.GetColumnName(mail, "myBoard", 0).Value, "backlog");
            assertTrue(s.GetColumnName(mail, "myBoard", 1).Value, "in progress");
            assertTrue(s.GetColumnName(mail, "myBoard", 2).Value, "done");
            validation(s.AdvanceTask(mail, "myBoard", 1, rB1T1.Value.Id), true);
            validation(s.AdvanceTask(mail, "myBoard", 2, rB1T1.Value.Id), true);
            validation(s.AdvanceTask(mail, "myBoard2", 1, rB2T1.Value.Id), true);
            validation(s.AdvanceTask(mail, "myBoard", 0, rB1T1.Value.Id), false);
            validation(s.AdvanceTask(mail, "myBoard", 0, rB1T2.Value.Id), true);
            validation(s.AdvanceTask(mail, "myBoard", 1, rB1T1.Value.Id), false);
            validation(s.AdvanceTask(mail, "myBoard", 0, rB1T2.Value.Id), false);
            validation(s.UpdateTaskDueDate(mail, "myBoard", 0, rB1T1.Value.Id, date.AddDays(1)), true);
            validation(s.UpdateTaskDueDate(mail, "myBoard", 0, rB1T2.Value.Id, date.AddDays(1)), true);
            validation(s.UpdateTaskDueDate(mail, "myBoard", 0, rB1T3.Value.Id, new DateTime()), true);
            validation(s.UpdateTaskDueDate(mail, "myBoard", 0, rB1T3.Value.Id+100, date.AddDays(1)), true);
            validation(s.UpdateTaskDueDate(mail, "myBoard", 0, rB1T3.Value.Id, date.AddDays(1)), false);
            validation(s.UpdateTaskTitle(mail, "myBoard", 0, rB1T1.Value.Id, ""), true);

        }

        static void Column(Service s)
        {

        }

        static void validation(Response r, bool expected)
        {
            if (r.ErrorOccured != expected)
                throw new Exception();
        }

        static void assertTrue(Object b1, Object b2)
        {
            if (!b1.Equals(b2))
                throw new Exception();
        }
    }
}
