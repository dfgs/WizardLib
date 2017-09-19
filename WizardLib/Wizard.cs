using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace WizardLib
{
	public class Wizard<DataType> :DependencyObject, IWizard,INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public static readonly DependencyProperty PagesProperty = DependencyProperty.Register("Pages", typeof(ObservableCollection<WizardPage<DataType>>), typeof(Wizard<DataType>));
		public ObservableCollection<WizardPage<DataType>> Pages
		{
			get { return (ObservableCollection<WizardPage<DataType>>)GetValue(PagesProperty); }
			set { SetValue(PagesProperty, value); }
		}

		IEnumerable IWizard.Pages
		{
			get	{return Pages;}
		}

		private WizardPage<DataType> selectedPage;
		public WizardPage<DataType> SelectedPage
		{
			get { return selectedPage; }
			private set
			{
				if (selectedPage != null) selectedPage.IsSelected = false;
				selectedPage = value;
				if (selectedPage != null)
				{
					selectedPage.Data = Data;
					selectedPage.IsSelected = true;
				}
				OnPropertyChanged("SelectedPage");
			}
		}


		public static readonly DependencyProperty SelectedPageIndexProperty = DependencyProperty.Register("SelectedPageIndex", typeof(int), typeof(Wizard<DataType>),new FrameworkPropertyMetadata(SelectedPageIndexPropertyChanged) );
		public int SelectedPageIndex
		{
			get { return (int)GetValue(SelectedPageIndexProperty); }
			set { SetValue(SelectedPageIndexProperty, value); }
		}

		public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(DataType), typeof(Wizard<DataType>));


		public DataType Data
		{
			get { return (DataType)GetValue(DataProperty); }
			set { SetValue(DataProperty, value); }
		}

		

		public Wizard()
		{
			Pages = new ObservableCollection<WizardPage<DataType>>();
			Pages.CollectionChanged += Pages_CollectionChanged;
		}

		private void Pages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (Pages.Count == 0) SelectedPage = null;
			else if (Pages.Count == 1) SelectedPage = Pages[0];
			for(int t=0;t<Pages.Count;t++)
			{
				Pages[t].Index = t;
			}
			
		}

		private static void SelectedPageIndexPropertyChanged(DependencyObject sender,DependencyPropertyChangedEventArgs e)
		{
			Wizard<DataType> wizard;
			wizard = sender as Wizard<DataType>;
			if (wizard == null) return;
			wizard.OnSelectedPageIndexChanged();
		}

		protected virtual void OnSelectedPageIndexChanged()
		{
			if ((Pages != null) && (SelectedPageIndex >= 0) && (SelectedPageIndex < Pages.Count)) SelectedPage = Pages[SelectedPageIndex];
		}

		public bool CanFinish()
		{
			return (SelectedPage != null) && (SelectedPageIndex==Pages.Count-1) && (SelectedPage.CanGoNext());
		}

		public bool CanGoNext()
		{
			return (SelectedPage != null) && (SelectedPageIndex < Pages.Count - 1) && (SelectedPage.CanGoNext());
		}

		public bool CanGoPrevious()
		{
			return (SelectedPage != null) && (SelectedPageIndex >0) && (SelectedPage.CanGoPrevious());
		}

		public bool CanRestart()
		{
			return OnCanRestart();
		}

		public virtual bool OnCanRestart()
		{
			return (SelectedPage != null) && (SelectedPageIndex > 0);
		}

		public virtual void Finish()
		{
			
		}

		public void GoNext()
		{
			if (CanGoNext())
			{
				SelectedPage.Leave(true);
				SelectedPage.IsDone = true;
				SelectedPageIndex++;
				SelectedPage.Enter(true);
			}
		}

		public void GoPrevious()
		{
			if (CanGoPrevious())
			{
				SelectedPage.Leave(false);
				SelectedPage.IsDone = false;
				SelectedPageIndex--;
				SelectedPage.Enter(false);
			}
		}
		public void Restart()
		{
			OnRestart();
		}

		protected virtual void OnRestart()
		{
			SelectedPageIndex = 0;
		}

		protected virtual void OnPropertyChanged(string PropertyName)
		{
			if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
		}
		
	}
}
