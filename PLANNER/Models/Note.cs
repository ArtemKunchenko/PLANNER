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
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int note_id { get; set; }

        [ForeignKey("Transaktion")]
        public int transaktion_id { get; set; }
        public Transaktion Transaktion { get; set; }

        [StringLength(1000)]
        public string content { get; set; }
    }
}
