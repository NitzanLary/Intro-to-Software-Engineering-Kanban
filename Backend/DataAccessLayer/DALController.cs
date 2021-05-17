using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal abstract class DALController
    {


        private SQLiteConnection GetConnection()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));

            string connectionString = $"Data Source={path}; Version=3";
            return new SQLiteConnection(connectionString);

        }

        const string messageTableNAme = "tablename";
        const string IDColumnName = "ID";
        - using
        - SQLiteCommand command = new SQLiteCommand(null, connection);
        int res = -1;
        try{
            command.CommandText = $"INSERT INTO {tablename} ({IDColumn},...)"+
            $"VALUES (@idVal,...);"
            SQLiteParameter idParam = new SQLiteParameter(@"idVal", 1);

            SQLiteCommand.Paramenter.Add(idParam);
            command.Prepare();

            connection.Open();
            res = SQLiteCommand.ExcecuteNonQuery(); (ששאילתת כתיבה)
            }
    catch (Exception e){
        log error
    }finally
{
    command.Dispose()
    connecton.Close();
}
}
