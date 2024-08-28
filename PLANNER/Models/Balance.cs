using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class Balance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int balance_id { get; set; }

        [ForeignKey("Bankaccount")]
        public int bankaccount_id { get; set; }
        public Bankaccount Bankaccount { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal total_amount { get; set; }

       

        public DateTime created_at { get; set; }
    }
}
