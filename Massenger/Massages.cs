using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Massenger
{
    class Massages
    {
        [Key]
        public int Id					{ get; set; }
        public int SenderPhone			{ get; set; }
        public int RecepientPhone		{ get; set; }
        public DateTime DateOfSend      { get; set; }
        public DateTime TimeOfSend      { get; set; }
        public string TextMessage       { get; set; }

        [ForeignKey("SenderPhone")]
        public Users User               { get; set; }
        [ForeignKey("RecepientPhone")]
        public Recepients Recepient     { get; set; }
    }
}
