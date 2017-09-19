using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WizardLib
{
	public abstract class WizardPage<DataType>:DependencyObject
	{
		public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(WizardPage<DataType>));
		public string ErrorMessage
		{
			get { return (string)GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}

		public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof(int), typeof(WizardPage<DataType>));
		public int Index
		{
			get { return (int)GetValue(IndexProperty); }
			internal set {SetValue(IndexProperty, value);}
		}

		public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(WizardPage<DataType>));
		public bool IsSelected
		{
			get { return (bool)GetValue(IsSelectedProperty); }
			internal set{SetValue(IsSelectedProperty, value);}
		}

		public static readonly DependencyProperty IsDoneProperty = DependencyProperty.Register("IsDone", typeof(bool), typeof(WizardPage<DataType>));
		public bool IsDone
		{
			get { return (bool)GetValue(IsDoneProperty); }
			internal set{SetValue(IsDoneProperty, value);}
		}

		public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(DataType), typeof(WizardPage<DataType>));
		public DataType Data
		{
			get { return (DataType)GetValue(DataProperty); }
			internal set { SetValue(DataProperty, value); }
		}

		public abstract string Header
		{
			get;
		}

		public bool CanGoNext()
		{
			return OnCanGoNext();
		}
		public bool CanGoPrevious()
		{
			return OnCanGoPrevious();
		}

		
		public abstract bool OnCanGoNext();
		public abstract bool OnCanGoPrevious();

		protected virtual void OnEnter(bool Forward)
		{

		}
		protected virtual void OnLeave(bool Forward)
		{

		}	


		internal void Enter(bool Forward)
		{
			OnEnter(Forward);
		}
		internal void Leave(bool Forward)
		{
			OnLeave(Forward);
		}

	}
}
