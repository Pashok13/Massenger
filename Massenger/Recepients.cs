using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Massenger
{
    class Recepients
    {
        [Key]
        public int RecepientID  { get; set; }
        public string Name      { get; set; }
        public string Adress    { get; set; }

        public ICollection<Massages> MassageCollection { get; set; }

        public Recepients()
        {
            MassageCollection = new List<Massages>();
        }
    }
}
