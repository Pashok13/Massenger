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
		static void Main(string[] args)
        {
			BackEndFunctions.MessendgerDB = new MessandgerContext();

			UserInferface.StartPage();

			if(BackEndFunctions.isAutorize)
				UserInferface.MainMenu();
        }
    }
}
