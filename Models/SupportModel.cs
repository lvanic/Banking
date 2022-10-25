using Banking.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models
{
    public class SupportModel
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<Request> Requestes { get; set; } = new List<Request>();



        public SupportModel()
        {
        }

    }
}
