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
    class Recepients
    {
        [Key]
        public int RecepientId  { get; set; }
        public string Name      { get; set; }
		private string recepientPhone;
		private string RecepientPhone
		{
			get
			{
				return recepientPhone;
			}

			set
			{
				Regex phone = new Regex(@"^\+\d{12}");

				if (phone.IsMatch(value))
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

		public ICollection<Messages> MassageCollection { get; set; }

        public Recepients()
        {
            MassageCollection = new List<Messages>();
        }
    }
}
