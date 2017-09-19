using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardLib
{
	public interface IWizard
	{
		IEnumerable Pages
		{
			get;
		}

		bool CanGoNext();
		bool CanGoPrevious();
		bool CanFinish();
		bool CanRestart();
		void GoNext();
		void GoPrevious();
		void Finish();
		void Restart();
	}
}
