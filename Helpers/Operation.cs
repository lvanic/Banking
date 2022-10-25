using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Models
{
    class Operation
    {
        public TypeOfOperation typeOfOperation;
        public DateTime dateTime;

        public Operation(TypeOfOperation typeOfOperation, DateTime dateTime)
        {
            this.typeOfOperation = typeOfOperation;
            this.dateTime = dateTime;
        }
    }
}
