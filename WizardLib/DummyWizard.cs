using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizardLib
{
	internal class DummyWizard:Wizard<int>
	{
		public DummyWizard()
		{
			Pages.Add(new DummyWizardPage("Step 1") { ErrorMessage = "No wizard defined" });
			Pages.Add(new DummyWizardPage("Step 2 with long text"));
			Pages.Add(new DummyWizardPage("Step 3"));
			Pages.Add(new DummyWizardPage("Step 4"));
			Pages.Add(new DummyWizardPage("Step 5"));

		}
	}
}
