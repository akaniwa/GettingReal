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
            if (parameter is MainAssortmentViewModel mainAssortment)
            {
                if (mainAssortment.SelectedItemDescriptionVM != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainAssortmentViewModel mainAssortment)
            {
                mainAssortment.ItemDescriptionsVM.Remove(mainAssortment.SelectedItemDescriptionVM);
                //mainAssortment.DeleteItemDescription(mainAssortment.SelectedItemDescriptionVM.ItemDescription);
            }
        }
    }
}
