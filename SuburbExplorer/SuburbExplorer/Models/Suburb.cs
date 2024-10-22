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
        public int SuburbCode {  get; set; }
        public string? SuburbName { get; set; }
        public int StateCode { get; set; }
        public string? StateName { get; set;}
        public bool IsFavorite { get; set; }
        public decimal SuburbScore { get; set; }

    }
}
