using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Massenger
{
    class Users
    {
        [Key]
        public int PhoneId      { get; set; }
        public string Password  { get; set; }
        public string Adress    { get; set; }

        public ICollection<Massages> MassageCollection { get; set; }

        public Users()
        {
            MassageCollection = new List<Massages>();
        }
    }
}
