using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
	[DataContract]
	public class Messages
    {
        [Key] [DataMember]
        public int Id				{ get; set; }
		[DataMember]
		public int UserId			{ get; set; }
		[DataMember]
		public int RecepientId		{ get; set; }
		public DateTime DateOfSend	{ get; set; } 
        public TimeSpan TimeOfSend  { get; set; }
		[DataMember]
		public string TextMessage   { get; set; }

        [ForeignKey("UserId")]
		public Users User           { get; set; }
        [ForeignKey("RecepientId")]
        public Recepients Recepient { get; set; }

		public Messages()
		{
			DateOfSend = DateTime.Now.Date;
			TimeOfSend = DateTime.Now.TimeOfDay;
		}
    }
}
