using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Messenger
{
	[DataContract]
	public class Message
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
		public User User           { get; set; }
        [ForeignKey("RecepientId")]
        public Recepient Recepient { get; set; }

		public Message()
		{
			DateOfSend = DateTime.Now.Date;
			TimeOfSend = DateTime.Now.TimeOfDay;
		}
    }
}
