using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WizardLib
{
	/// <summary>
	/// Logique d'interaction pour WizardControl.xaml
	/// </summary>
	public partial class WizardControl : UserControl
	{
		public static readonly DependencyProperty WizardProperty = DependencyProperty.Register("Wizard", typeof(IWizard), typeof(WizardControl));
		public IWizard Wizard
		{
			get { return (IWizard)GetValue(WizardProperty); }
			set { SetValue(WizardProperty, value); }
		}

		public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(WizardControl));
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		public event EventHandler WizardFinished;
		public event EventHandler WizardRestated;

		public WizardControl()
		{
			InitializeComponent();
			Wizard = new DummyWizard();
		}


		private void NextCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Wizard!= null) && (Wizard.CanGoNext());
		}
		private void NextCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Wizard.GoNext();
		}

		private void PreviousCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Wizard != null) && (Wizard.CanGoPrevious());
		}
		private void PreviousCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Wizard.GoPrevious();
		}

		private void RestartCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Wizard != null) && (Wizard.CanGoPrevious());
		}
		private void RestartCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Wizard.Restart();
			if (WizardRestated != null) WizardRestated(this, EventArgs.Empty);
		}

		private void FinishCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (Wizard != null) && (Wizard.CanFinish());
		}
		private void FinishCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			Wizard.Finish();
			if (WizardFinished != null) WizardFinished(this, EventArgs.Empty);
		}

		

	}
}
