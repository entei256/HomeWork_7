using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace HomeWork_7
{
    //Реализация комманд для MVVM
    class CommandForm : ICommand
    {
        //Вспомогательные делегады. Не совсем понял для чего они. Пособие с metanit не разьесняет.
        private Action<object> execute;
        private Func<object, bool> canExecute;

        //Управление событиями комманд из формы
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null || this.canExecute(parameter))
                return true;                                                  //Возвращаем true если делегат пустой.
            return false;
        }



        public void Execute(object parameter)
        {
            this.execute(parameter);      //Самовыполнение команды
        }

        //констррукто для вызова из вне.
        public CommandForm(Action<object> execute,Func<object,bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
    }
}
