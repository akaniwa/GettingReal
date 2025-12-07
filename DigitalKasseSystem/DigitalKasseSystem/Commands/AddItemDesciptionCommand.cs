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

        // Method that always returns true, since this always need to be able to be executed
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        // This is what happeds when command is executed - parameter being the datacontext here
        public void Execute(object? parameter)
        {
            if (parameter is MainAssortmentViewModel mainAssortmentViewModel)
            {
                // Makes new ItemDesciption with default settings and adds it to repo
                ItemDescription defaultItemDesciption = new ItemDescription();
                mainAssortmentViewModel.AddNewItemDescription(defaultItemDesciption);
            }
        }
    }
}
