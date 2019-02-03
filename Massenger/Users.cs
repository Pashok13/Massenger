using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Massenger;

namespace Messenger
{
    public class User
    {
        [Key]
		public int UserId		{ get; set; }
		public string Name		{ get; set; }
		public string Password	{ get; set; }
		private string userPhone;
		[Required]
		public string UserPhone
		{
			get { return userPhone; }

			set
			{
				if (value != null && BackEndFunctions.IsValidPhone(value))
				{
					userPhone = value;
				}
				else
				{
					Console.WriteLine("Incorrect phone number!");
				}
			}
		}
		private string adress;
		[Required]
		public string Adress
		{
			get { return adress; }

			set
			{
				if (value != null && BackEndFunctions.IsValidEmail(value))
				{
					adress = value;
				}
				else
				{
					Console.WriteLine("Incorrect e-mail adress!");
				}
			}
		}

        public ICollection<Message> MessageCollection { get; set; }

        public User()
        {
            MessageCollection = new List<Message>();
        }
    }
}
