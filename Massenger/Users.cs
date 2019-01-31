using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Messenger
{
    class Users
    {
        [Key]
		public int UserId		{ get; set; }
		public string Name		{ get; set; }
		public string Password	{ get; set; }
		private string userPhone;
		public string UserPhone
		{
			get
			{
				return userPhone;
			}

			set
			{ 
				Regex phone = new Regex(@"^\+\d{12}");

				if (phone.IsMatch(value))
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
		public string Adress
		{
			get 
			{
				return adress;
			}

			set	
			{
				Regex email = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
						@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

				if (email.IsMatch(value))
				{
					adress = value;
				}
				else
				{
					Console.WriteLine("Incorrect e-mail adress!");
				}
			}
		}

        public ICollection<Messages> MessageCollection { get; set; }

        public Users()
        {
            MessageCollection = new List<Messages>();
        }
    }
}
