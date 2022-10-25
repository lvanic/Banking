using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models
{
    public class Message//добавить свойства
    {
        [Key]
        public int Id { get; set; }
       
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public ClientModel Client { get; set; }
        public SupportModel Support { get; set; }
        public bool IsActive { get; set; }
    }
}
