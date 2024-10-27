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
            return await database.InsertAsync(suburb);

        }
        
        public async Task<Suburb> CheckDuplicateFavoriteSuburbAsync(int suburbCode)
        {
            return await database.Table<Suburb>().Where(s => s.SuburbCode == suburbCode).FirstOrDefaultAsync();
        } 

        
        public async Task<int> DeleteFavoriteSuburbAsync(string suburbName, string stateName)
        {
            
            var suburbToDelete = await database.Table<Suburb>().Where(s => s.SuburbName == suburbName && s.StateName == stateName).FirstOrDefaultAsync();
            if (suburbToDelete != null)
            {
                return await database.DeleteAsync(suburbToDelete);
            }
            return 0;
        }
        

        
  
    }
}
