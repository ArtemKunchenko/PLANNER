using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
   public class Exchange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int exchanges { get; set; }

        [ForeignKey("CurrencyFrom")]
        public int currency_from { get; set; }
        public Currency CurrencyFrom { get; set; }

        [ForeignKey("CurrencyTo")]
        public int currency_to { get; set; }
        public Currency CurrencyTo { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 6)")]
        public decimal exchange_rate { get; set; }
    }
}
