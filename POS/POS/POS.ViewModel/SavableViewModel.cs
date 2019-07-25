using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel
{
    public abstract class SavableViewModel<IT> : BaseViewModel where IT:class
    {
        public IT Repository { get; set; }
        public SavableViewModel()
        { 
            SaveCommand = new DelegateCommand(Save, CanSave); 
        }
        public DelegateCommand SaveCommand { get; }


        protected override void OnModelChanged()
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        protected abstract bool CanSave();

        protected abstract void Save();
    }
}
