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
            // Initialize the database and create the suburb table
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<Suburb>().Wait();

        }

        public async Task<List<Suburb>> GetFavoriteSuburbsAsync()
        {
            return await database.Table<Suburb>().ToListAsync();
        }

        public async Task<int> AddFavoriteSuburbAsync(Suburb suburb)
        {
            if (suburb.Id != 0)
            {
                return await database.UpdateAsync(suburb);
            }
            else 
            {
                return await database.InsertAsync(suburb);
            }

        }
        
        /* ToDo
        public async Task<int> DeleteFavoriteSuburbAsync(string suburbName, string stateName)
        {
            
            //var suburbToDelete = await database.Table<Suburb>().Where(s => s.SuburbName == suburbName && s.StateName == stateName);
            //return await database.DeleteAsync(suburbToDelete);
        }
        */

        
  
    }
}
