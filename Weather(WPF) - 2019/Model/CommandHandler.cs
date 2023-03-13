using System;
using System.Windows.Input;

namespace Weather
{
    public class CommandHandler : ICommand
    {
        private Action execute;

        private Func<bool> canExecute;

        private event EventHandler CanExecuteChangedInternal;

        public CommandHandler(Action execute) : this(execute, DefaultCanExecute)
        {
        }

        public CommandHandler(Action execute, Func<bool> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                this.CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute != null && this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.execute();
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        private static bool DefaultCanExecute()
        {
            return true;
        }
    }
}
