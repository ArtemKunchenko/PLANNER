using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PLANNER.Models
{
     public class Bankaccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bankaccount_id { get; set; }

        [ForeignKey("User")]
        public int user_id { get; set; }
        public User ?User { get; set; }

        [Required]
        public int balance_id { get; set; }


       

        

        public ICollection<Transaktion> Transaktions { get; set; }
    }
}
