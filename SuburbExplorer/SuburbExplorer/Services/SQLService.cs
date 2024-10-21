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
            database.CreateTableAsync<FavoriteSuburb>().Wait();

        }

        public async Task<List<FavoriteSuburb>> GetFavoriteSuburbsAsync()
        {
            return await database.Table<FavoriteSuburb>().ToListAsync();
        }

        public async Task<int> AddFavoriteSuburb(FavoriteSuburb suburb)
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

        public async Task<int> DeleteFavoriteSuburb(FavoriteSuburb suburb)
        {
            return await database.DeleteAsync(suburb);
        }

        
  
    }
}
