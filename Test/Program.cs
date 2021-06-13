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

            //Service s = new Service();
            //Print(s.AddBoard("Rafa@gmail.com", "semster B"));
            //Print(s.Register("Rafa@gmail.com", "12345678"));
            //Print(s.Register("Rafagmail.com", "Rf12345678"));
            //Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            ////Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            ////Print(s.Register("Rafa111111@gmail.com", "R12345678"));
            ////Print(s.Register("Rafa111111@gmail.com", "ff12345678"));
            ////Print(s.Register("Rafa111111@gmail.com", "RF12345678"));
            ////Print(s.Register("Rafa111111@gmail.com", "Rf2"));
            ////Print(s.Register("Rafa111111@gmail.com", "R12345678222222222222222222222222222222222f"));
            ////Print(s.Register("Rafa111111@gmail.com", "R12345678ש"));
            //Print(s.Register("Rafa111111@gmail.com", "R12345678f"));
            ////Print(s.Login("Rafa@gmail.com", "R12345678f"));
            //Print(s.AddBoard("Rafa@gmail.com", "semester B"));
            //Print(s.Login("Rafa@gmail.com", "Rf12345678"));
            ////Print(s.Login("Rafa123@gmail.com", "Rf12345678"));
            //Print(s.Login("Rafa111111@gmail.com", "R12345678f"));

            ////Print(s.Register("Rafa@...@", "1234567Rfgmlks"));
            ////Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            ////Print(s.Logout("Rafa@gmail.com"));
            ////Print(s.Logout("Rafa@gmail.com"));
            ////Print(s.Register("Rafa@gmail.com", "Rf12345678"));
            ////Print(s.Login("Rafa@gmail.com", "Rf12345678"));
            ////Print(s.Login("Rafa@gmail.com", "Rf12345678"));



            //// --------------------- AddBoard Tests -----------------------
            //Print(s.AddBoard("Rafa@gmail.com", "semester B"));

            //Print(s.AddBoard("Rafa@gmail.com", "semester B")); // ERR had board name already

            //Print(s.AddBoard("Rafa@gmail.com", "semester A"));
            //Print(s.AddBoard("Rafa111111@gmail.com", "semester B"));
            //Print(s.AddBoard("Rafa@gmail.com", "")); // ERR not legle board name

            //Print(s.RemoveBoard("Rafa@gmail.com", "semester B")); 
            //Print(s.RemoveBoard("Rafa@gmail.com", "semester B")); // ERR not had board name
            //Print(s.RemoveBoard("Rafdsada@gmail.com", "semester B")); // ERR not had email 
            //Print(s.RemoveBoard("Rafa@gmail.com", "semester A"));




            // --------------------- AddTask Tests -----------------------

            //Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));
            ////Console.WriteLine(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)).Value.Id);
            //Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "", new DateTime(2021, 5, 14, 0, 0, 0))); // possible the same name for task???
            ////Console.WriteLine(s.AddTask("Rafa@gmail.com", "semester B", "SE", "", new DateTime(2021, 5, 14, 0, 0, 0)).Value.Id);
            //Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 2", new DateTime(2021, 5, 14, 0, 0, 0)));
            ////Console.WriteLine(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 2", new DateTime(2021, 5, 14, 0, 0, 0)).Value.Id);
            //Print(s.AddTask("Rafa@gmail.com", "semester B", "SE", "task 3", new DateTime(2021, 5, 14, 0, 0, 0)));
            //Print(s.AddTask("Rafa@gmail.com", "semester A", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));
            //Print(s.AddTask("Rafa111111@gmail.com", "semester B", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));
            //Print(s.AddTask("a1@gmail.com", "semester B", "SE", "test", new DateTime(2021, 5, 14, 0, 0, 0)));
            //Print(s.AddTask("Rafa111111@gmail.com", "semester C", "SE", "task 1", new DateTime(2021, 5, 14, 0, 0, 0)));

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
            //Console.WriteLine(" \n\n Asaf Tests \n\n");
            //Print(s.Register("asafs@gmail.com", "Aa123"));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.Login("asafs@gmail.com", "Aa123"));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.Login("asafs@gmail.com", "Aa123"));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.Login("asafs@gmail.com", "Aa1234"));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.AddBoard("asafs@gmail.com",""));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.AddBoard("asafs@gmail.com", ""));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.AddBoard("asafs@gmail.com", "b1"));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.AddBoard("asafs@gmail.com", "b1"));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.AddBoard("asafs@gmail.com", "b2"));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.AddTask("asafs@gmail.com", "", "", "", new DateTime()));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.AddTask("asafs@gmail.com","b1", "", "", new DateTime()));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.AddTask("asafs@gmail.com", "b1","title1","", DateTime.Parse("4-20-2021")));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.AddTask("asafs@gmail.com", "b1", "title1", "123", DateTime.Parse("4-20-2021")));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.LimitColumn("asafs@gmail.com", "b11", 0, 2));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.LimitColumn("asafs@gmail.com", "b1", 0, 2));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.AddTask("asafs@gmail.com", "b1", "title1", "1234", DateTime.Parse("4-20-2021")));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.LimitColumn("asafs@gmail.com", "b1", 0, 3));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.AddTask("asafs@gmail.com", "b1", "title1", "1234", DateTime.Parse("4-20-2021")));
            //Console.WriteLine("\t\tshould print ok\n");
            //Console.WriteLine("\n\n\n\n" + s.InProgressTasks("asafs@gmail.com").Value.Count + "\n\n\n\n");
            //Print(s.AdvanceTask("asafs@gmail.com", "b1", 1, 4));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Console.WriteLine("\n\n\n\n" + s.InProgressTasks("asafs@gmail.com").Value.Count + "\n\n\n\n");
            //Print(s.AdvanceTask("asafs@gmail.com", "b1", 0, 4));
            //Console.WriteLine("\t\tshould print ok\n");
            //Console.WriteLine("\n\n\n\n"+s.InProgressTasks("asafs@gmail.com").Value.Count+"\n\n\n\n");
            //Print(s.AdvanceTask("asafs@gmail.com", "b1", 0, 1));
            //Console.WriteLine("\t\tshould print ok\n");
            //Console.WriteLine("\n\n\n\n" + s.InProgressTasks("asafs@gmail.com").Value.Count + "\n\n\n\n");
            //Print(s.AdvanceTask("asafs@gmail.com", "b1", 1, 1));
            //Console.WriteLine("\t\tshould print ok\n");
            //Console.WriteLine("\n\n\n\n" + s.InProgressTasks("asafs@gmail.com").Value.Count + "\n\n\n\n");
            //Print(s.GetColumnName("asafs@gmail.com", "b11", 1));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.GetColumnName("asafs@gmail.com", "b1", 3));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.GetColumnName("asafs@gmail.com", "b1", 1));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.GetColumn("asafs@gmail.com", "b11", 2));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Console.WriteLine("\n\n\n\n" + s.GetColumn("asafs@gmail.com","b1",1).Value.Count + "\n\n\n\n");

            ////////////// starting asaf register tests
            //Print(s.Register("a@a.co.il", "Aa12"));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.Register("aa@a.co.il", "Aa1"));
            //Console.WriteLine("\t\tshould print not ok\n");
            //Print(s.Register("aa@a.co.il", "Aaa1"));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.Register("aaa@a.co.il", "0Aaa1"));
            //Console.WriteLine("\t\tshould print ok\n");
            //Print(s.Register("a^a_?a@a.co.il", "0Aaa1"));
            //Console.WriteLine("\t\tshould print not ok\n");


            //////   starting dal test!    /////
            //UserDALController udc = new();
            //BoardDALController bdc = new();
            //ColumnDALController cdc = new();
            //TaskDALController tdc = new();
            //UserDTO u1 = new UserDTO("yanay", "123");
            //UserDTO u2 = new UserDTO("nitzan", "123");
            //TaskDTO t1 = new TaskDTO("bb", "yanay", 0, 1, "t1", "desc1", "asaf", "12-2-2030", "12-2-2022");
            //ColumnDTO c0 = new ColumnDTO("yanay", "bb", 0, 10,  new List<TaskDTO>() { t1 });
            //ColumnDTO c1 = new ColumnDTO("yanay", "bb", 1, 10, new List<TaskDTO>());
            //ColumnDTO c2 = new ColumnDTO("yanay", "bb", 2, 10, new List<TaskDTO>());
            //BoardDTO bb = new BoardDTO("yanay", "bb", new List<string>(), new List<ColumnDTO>() { c0, c1, c2 });

            //Console.WriteLine(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "geyanay.db")));
            //udc.InsertNewUser(u1);
            //bdc.InsertNewBoard(bb);
            //cdc.InsertNewColumn(c0);
            //cdc.InsertNewColumn(c1);
            //cdc.InsertNewColumn(c2);
            //tdc.InsertNewTask(t1);
            //u2.Insert();
            //u1.Insert();
            //bb.Insert();
            //c0.Insert();
            //c1.Insert();
            //c2.Insert();
            //t1.Insert();
            //bdc.InsertNewBoardMember(bb, u2.Email);

            //u1.Delete();
            //Console.WriteLine("starting deletes");
            //Service s = new Service();
            //s.DeleteData();

            //udc.DeleteAllData();
            //bdc.DeleteAllData();
            //Console.WriteLine(bb.Delete());
            //c0.Delete();
            //c1.Delete();
            //c2.Delete();
            //t1.Delete();
            //Console.WriteLine("deletes completed");


            //List<BoardDTO> lb = bdc.SelectAllBoards();
            //Console.WriteLine(lb[0].Boardname == bb.Boardname && lb[0].Creator == bb.Creator && lb[0].Columns[0].Tasks[0].TaskID == t1.TaskID);
            //t1.ColumnOrdinal = 1;
            //Console.WriteLine(t1.ColumnOrdinal);
            //c0.MaxTasksNumber = 20;
            //Console.WriteLine(c0.MaxTasksNumber);
            //t1.Description = "okokok put guitars";
            //Console.WriteLine(t1.Description);
            //u1.Password = "sunny";
            //Console.WriteLine(u1.Password);
            //bb.Boardname = "YanaySunny";
            //Console.WriteLine(bb.Boardname);

            //s.DeleteData();
            //Register();
            //Login();
            //AddBoard();
            //JoinBoard();
            //LimitColumn();
            //GetColumnLimit();
            //GetColumnName();
            //AddTask();

            List<string> l = new List<string> { "a", "b", "c", "d", "e" };
            var t = l[1];
            l.RemoveAt(1);
            l.Insert(3, t);
            l.ForEach(Console.WriteLine);

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
            Print(s.Register("Rafa@gmail.com", null));
            Print(s.Register("Rafa@gmail.com", null));
            //Print(s.Register("Nitzan@gmail.com", "Rf12345678"));
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
