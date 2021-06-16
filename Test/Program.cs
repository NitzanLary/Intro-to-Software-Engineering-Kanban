using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System.Collections.Generic;
using System.IO;

namespace Test
{
    class Program
    {
        private static int testCount = 0;
        private static Service s = new Service();
        private static string nitzanMail = "Nitzan@gmail.com";
        private static string nitzanBoard = "Nitzans Board";
        private static string rafaMail = "Rafa@gmail.com";
        private static string rafaBoard = "Rafas Board";
        static void Main(string[] args)
        {

            s.LoadData();
            //s.DeleteData();
            //Register();
            //Login();
            //AddBoard();
            //JoinBoard();
            //JoinBoardPersistence();
            //LimitColumn();
            //GetColumnLimit();
            //GetColumnName();
            //AddTask();
            AddTaskPersistence();
            //GetBoardNames();
        }

        
        static void Print(Response res)
        {
            string input = $"test {++testCount}  ";
            if (res.ErrorOccured)
                input += res.ErrorMessage;
            else
                input += "no error  " + res.GetType().Name;
            Console.WriteLine($"\n    -  ----------   {input}   ----------   -\n");
        }

        static void Register()
        {
            //Print(s.Register("Rafa@gmail.com", null));
            Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            Print(s.Register("Nitzan@gmail.com", "Rf12345678"));
        }

        static void Login()
        {
            Print(s.Login("Rafa@gmail.com", "Rf12345678"));
            Print(s.Login("Nitzan@gmail.com", "Rf12345678"));
        }

        static void AddBoard()
        {
            Print(s.AddBoard(nitzanMail, nitzanBoard));
            Print(s.AddBoard(rafaMail, rafaBoard));
            Print(s.AddBoard(nitzanMail, nitzanBoard)); // error
        }

        static void JoinBoard()
        {
            Print(s.JoinBoard(rafaMail, nitzanMail, nitzanBoard));
        }

        private static void JoinBoardPersistence()
        {
            Print(s.JoinBoard(nitzanMail, rafaMail, rafaBoard));
        }

        static void LimitColumn()
        {
            Print(s.LimitColumn(nitzanMail, nitzanMail, nitzanBoard, 0, 10));
            Print(s.LimitColumn(rafaMail, nitzanMail, nitzanBoard, 1, 20)); // error
        }

        static void GetColumnLimit()
        {
            Response<int> r = s.GetColumnLimit(nitzanMail, nitzanMail, nitzanBoard, 0);
            Print(r);
            Console.WriteLine(r.Value);
            r = s.GetColumnLimit(rafaMail, nitzanMail, nitzanBoard, 0);
            Print(r);
            Console.WriteLine(r.Value);
        }

        static void GetColumnName()
        {
            Response<string> r = s.GetColumnName(nitzanMail, nitzanMail, nitzanBoard, 0);
            Print(r);
            Console.WriteLine(r.Value);
            r = s.GetColumnName(rafaMail, nitzanMail, nitzanBoard, 0);
            Print(r);
            Console.WriteLine(r.Value);
        }

        static void AddTask()
        {
            Print(s.AddTask(nitzanMail, nitzanMail, nitzanBoard, "nitzans task", "desc", DateTime.Today.AddDays(1)));
            Print(s.AddTask(nitzanMail, nitzanMail, nitzanBoard, "nitzans task2", "desc", DateTime.Today.AddDays(1)));
            Print(s.AddTask(rafaMail, rafaMail, rafaBoard, "rafa task", "desc", DateTime.Today.AddDays(1)));
            Print(s.AddTask(rafaMail, nitzanMail, nitzanBoard, "rafa task in nitzans board", "desc", DateTime.Today.AddDays(1)));
        }

        static void AddTaskPersistence()
        {
            Print(s.AddTask(nitzanMail, nitzanMail, nitzanBoard, "nitzans task persistence", "id should be 4", DateTime.Today.AddDays(1)));
        }

        private static void GetBoardNames()
        {
            Response <IList<string>> response = s.GetBoardNames(rafaMail);
            Print(response);
            foreach (string s in response.Value)
                Console.WriteLine(s);

        }



        static void Task(Service s)
        {
            //DateTime date = new DateTime(2021, 5, 16, 0, 0, 0);
            //string mail = "Rafa@gmail.com";
            //validation(s.AddBoard("Rafa@gmail.com", null), true);
            //validation(s.AddBoard("Rafa@gmail.com", "myBoard"), false);
            //validation(s.AddBoard("Rafa@gmail.com", "myBoard"), true);
            //validation(s.AddBoard("Rafa@gmail.com", "myBoard2"), false);
            //validation(s.AddTask(mail, null, "myTitle", "myDesc", date), true);
            //validation(s.AddTask(mail, "myBoard", null, "myDesc", date), true);
            //Response<Task> rB1T1 = s.AddTask(mail, "myBoard", "myTitle1", "myDesc1", date);
            //Response<Task> rB1T2 = s.AddTask(mail, "myBoard", "myTitle2", "myDesc2", date);
            //Response<Task> rB1T3 = s.AddTask(mail, "myBoard", "myTitle3", "myDesc3", date);
            //Response<Task> rB2T1 = s.AddTask(mail, "myBoard2", "myTitle1", "myDesc1", date);
            //validation(rB1T1, false); validation(rB1T2, false); validation(rB1T3, false); validation(rB2T1, false);
            //validation(s.LimitColumn(mail, "myBoard", 0, 1), true);
            //validation(s.LimitColumn(mail, "myBoard", 0, 3), false);
            //validation(s.AddTask(mail, "myBoard", "myTitle4", "myDesc4", date), true);

        }

            //static void Column(Service s)
            //{

            //}

            //static void validation(Response r, bool expected)
            //{
            //    if (r.ErrorOccured != expected)
            //        throw new Exception();
            //}
        }
    }
