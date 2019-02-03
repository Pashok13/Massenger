using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Massenger;

namespace Messenger
{
	[DataContract]
	public class Recepient
    {
        [Key] [DataMember]
		public int RecepientId  { get; set; }
		[DataMember]
		public string Name      { get; set; }
		private string recepientPhone;
		[DataMember] [Required]
		public string RecepientPhone
		{
			get {return recepientPhone;}

			set
			{
				if (value != null &&BackEndFunctions.IsValidPhone(value))
				{
					recepientPhone = value;
				}
				else
				{
					Console.WriteLine("Incorrect phone number!");
				}
			}
		}
		private string adress;
		[DataMember]
		public string Adress
		{
			get {return adress;}

			set
			{
				if (value != null && BackEndFunctions.IsValidEmail(value))
				{
					adress = value;
				}
				else
				{
					adress = null;
				}
			}
		}

		public ICollection<Message> MassageCollection { get; set; }

        public Recepient()
        {
            MassageCollection = new List<Message>();
        }
    }
}
