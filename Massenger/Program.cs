using Massenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messenger
{

    public class Program
    {
		static int UserInterface(params string[] menuItems)
		{
			short curItem = 0, c;
			ConsoleKeyInfo key;

			do
			{
				Console.Clear();
				Console.WriteLine("Select an option . . .");
				for (c = 0; c < menuItems.Length; c++)
				{
					if (curItem == c)
					{
						Console.Write(">>");
						Console.WriteLine(menuItems[c]);
					}
					else
					{
						Console.WriteLine(menuItems[c]);
					}
				}

				key = Console.ReadKey(true);

				if (key.Key.ToString() == "DownArrow")
				{
					curItem++;
					if (curItem > menuItems.Length - 1) curItem = 0;
				}
				else if (key.Key.ToString() == "UpArrow")
				{
					curItem--;
					if (curItem < 0) curItem = Convert.ToInt16(menuItems.Length - 1);
				}
			} while (key.KeyChar != 13);

			Console.Clear();

			return curItem;
		}


		static void Main(string[] args)
        {
			DatabaseFunctions.MessendgerDB = new MessandgerContext();
			int callback;

			do
			{
				callback = UserInterface(" Autorization", " Registration", "Exit");

				switch (callback)
				{
					case 0 :
						DatabaseFunctions.Autorization();
						break;
					case 1 :
						DatabaseFunctions.AddUser();
						break;
					case 2:
						return;
				}
			}
			while (!DatabaseFunctions.isAutorize);

			do
			{
				callback = UserInterface(" New message", " Exit");

				switch (callback)
				{
					case 0:
						DatabaseFunctions.SendMessege();
						break;
					case 1:
						return;
				}
			}
			while (true);
        }
    }
}
