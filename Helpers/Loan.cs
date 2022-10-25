using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models
{
    public class Loan
    {
        
        private int id;
        private DateTime _dateTimefrom;
        private DateTime _dateTimeto;
        private float _percent;
        private decimal _amount;
        private ClientModel _from;
        private ClientModel _to;
        private bool _isAccepted;

        [Key]
        public int Id { get => id; set => id = value; }
        public DateTime DateTimefrom { get => _dateTimefrom; set => _dateTimefrom = value; }
        public DateTime DateTimeto { get => _dateTimeto; set => _dateTimeto = value; }
        public float Percent { get => _percent; set => _percent = value; }
        public decimal Amount { get => _amount; set => _amount = value; }
        public bool IsAccepted { get => _isAccepted; set => _isAccepted = value; }
        public ClientModel From { get => _from; set => _from = value; }
        public ClientModel To { get => _to; set => _to = value; }
    }
}
