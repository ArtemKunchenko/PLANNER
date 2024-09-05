using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ListTransactionForTablecs
    {
        public decimal amount { get; set; }

        public string ?incomeOrExpence {  get; set; }
        public string note { get; set; }
    
        public DateTime transaktion_date { get; set; }
      
        public Category Category { get; set; }

    }
}
