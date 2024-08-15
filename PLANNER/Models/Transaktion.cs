using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class Transaktion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transaktion_id { get; set; }

        [Required]
        public bool is_online { get; set; } = false;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal amount { get; set; }

        [ForeignKey("Currency")]
        public int currency_id { get; set; }
        public Currency Currency { get; set; }

        [ForeignKey("Note")]
        public int? note_id { get; set; }
        public Note Note { get; set; }

        [Required]
        public DateTime transaktion_date { get; set; }

        [ForeignKey("Category")]
        public int category_id { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Bankaccount")]
        public int bankaccount_id { get; set; }
        public Bankaccount Bankaccount { get; set; }
    }
}
