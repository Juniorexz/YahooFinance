using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceDataAPI.Entidades
{
    [Table("employee")]
    public class Employee
    {
        [Key]
        public int id { get; private set; }
        public string name { get; private set; }

        public Employee() { }
        public Employee(string name)
        {
            this.name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
