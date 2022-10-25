using Banking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Helpers
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public ClientModel From { get; set; }
        public SupportModel To { get; set; }



    }
}
