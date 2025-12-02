using DigitalKasseSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DigitalKasseSystem.Commands
{
    public class DeleteItemDesciptionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            bool active = true;
            //if (parameter is MainAssortmentViewModel mainAssortment)
            //{
            //    if (mainAssortment.SelectedItemDescriptionVM == null)
            //    {
            //        active = false;
            //    }
            //}
            return active;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainAssortmentViewModel mainAssortment)
            {
                mainAssortment.DeleteItemDescription();
            }
        }
    }
}
