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

        // Method choosing wether or not this command can be executed
        public bool CanExecute(object? parameter)
        {
            //Returns true always right not - could not make it work in time

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

        // This is what happeds when command is executed - parameter being the datacontext here
        public void Execute(object? parameter)
        {
            // Runs method DeleteItemDesciption, that deletes the selected ItemDesciption
            if (parameter is MainAssortmentViewModel mainAssortment)
            {
                mainAssortment.DeleteItemDescription();
            }
        }
    }
}
