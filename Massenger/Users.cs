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
		public string UserPhone
		{
			get { return userPhone; }

			set
			{
				if (BackEndFunctions.IsValidPhone(value))
				{
					userPhone = value;
				}
				else
				{
					Console.WriteLine("Incorrect phone number!");
					userPhone = null;
				}
			}
		}
		private string adress;
		public string Adress
		{
			get { return adress; }

			set
			{
				if (BackEndFunctions.IsValidEmail(value))
				{
					adress = value;
				}
				else
				{
					Console.WriteLine("Incorrect e-mail adress!");
					adress = null;
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
