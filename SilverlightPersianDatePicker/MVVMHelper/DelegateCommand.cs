using System;
using System.Windows.Input;

namespace SilverlightPersianDatePicker.MVVMHelper
{
	// From http://johnpapa.net/silverlight/5-simple-steps-to-commanding-in-silverlight/
	/// <summary>
	/// Simplifies SL4 commanding.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class DelegateCommand<T> : ICommand
	{
		readonly Func<T, bool> _canExecute;
		readonly Action<T> _executeAction;
		bool _canExecuteCache;

		public DelegateCommand(Action<T> executeAction,
			Func<T, bool> canExecute)
		{
			_executeAction = executeAction;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			var temp = _canExecute((T)parameter);

			if (_canExecuteCache != temp)
			{
				_canExecuteCache = temp;

				if (CanExecuteChanged != null)
				{
					CanExecuteChanged(this, new EventArgs());
				}
			}

			return _canExecuteCache;
		}

		public event EventHandler CanExecuteChanged;
		public void Execute(object parameter)
		{
			_executeAction((T)parameter);
		}
	}
}
