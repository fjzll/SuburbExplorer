using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SuburbExplorer.Models
{
    public class Suburb
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Suburb_Code {  get; set; }
        public string Suburb_Name { get; set; }
        public int State_Code { get; set; }
        public string State_Name { get; set;}

        // public int Postcode { get; set; }
        // public bool IsFavorite { get; set; }
        // public int Score { get; set; }

    }
}
