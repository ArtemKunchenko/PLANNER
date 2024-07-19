using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public class ExchangingRateModel
    {
        public string cc { get; set; }
        public decimal rate { get; set; }
        public override string ToString()
        {
            return $"Currency: {cc} Rate: {rate}";
        }
    }
}
