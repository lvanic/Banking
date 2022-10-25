using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models
{
    public class BillModel
    {
        [Key]
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public ClientModel Client { get; set; }
        
    }
}
