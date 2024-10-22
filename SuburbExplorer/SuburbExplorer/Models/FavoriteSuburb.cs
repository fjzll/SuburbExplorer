using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SuburbExplorer.Models
{
    public class FavoriteSuburb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string SuburbName { get; set; }
        public string StateName { get; set; }
        public decimal Score { get; set; }

    }
}
