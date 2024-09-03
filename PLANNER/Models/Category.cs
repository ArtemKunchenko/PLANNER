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
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int category_id { get; set; }

        [Required]
        [StringLength(50)]
        public string name_category { get; set; }

        [StringLength(255)]
        public string ?description { get; set; }

        public ICollection<Transaktion> Transaktions { get; set; }
    }
}
