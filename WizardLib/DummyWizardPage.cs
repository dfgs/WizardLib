using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardLib
{
	class DummyWizardPage : WizardPage<int>
	{
		private string header;
		public override string Header
		{
			get { return header; }
		}

		public DummyWizardPage(string Header)
		{
			this.header = Header;
		}

		public override bool OnCanGoNext()
		{
			return true;
		}

		public override bool OnCanGoPrevious()
		{
			return true;
		}
	}
}
