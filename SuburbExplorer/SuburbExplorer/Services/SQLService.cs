using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SuburbExplorer.Models;

namespace SuburbExplorer.Services
{
    public class SQLService
    {
        SQLiteAsyncConnection database;
        public SQLService(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<Suburb>().Wait();

        }

  
    }
}
