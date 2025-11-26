using DigitalKasseSystem.Models;
using DigitalKasseSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DigitalKasseSystem.Commands
{
    public class AddItemDesciptionCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainAssortmentViewModel mainAssortmentViewModel)
            {
                ItemDescription defaultItemDesciption = new ItemDescription();
                mainAssortmentViewModel.AddNewItemDescription(defaultItemDesciption);
                mainAssortmentViewModel.SelectedItemDescriptionVM = new ItemDescriptionViewModel(defaultItemDesciption);
            }
        }
    }
}
