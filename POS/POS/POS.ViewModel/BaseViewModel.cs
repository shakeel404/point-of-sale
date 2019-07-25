using MahApps.Metro.Controls.Dialogs;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;

namespace POS.ViewModel
{
    public abstract class BaseViewModel : BindableBase
    {
        protected IDialogCoordinator Dialoger { get; } 
        public IEventAggregator EA { get; set; }

        public BaseViewModel()
        {
            IsViewModelAttached = false; 
            Dialoger = DialogCoordinator.Instance;
            ViewModelAttachedCommand = new DelegateCommand(OnViewModelAttached);

        }

        public DelegateCommand ViewModelAttachedCommand { get; }
        protected virtual void OnViewModelAttached()
        {
            IsViewModelAttached = true;
            
        }
        protected bool IsViewModelAttached { get; private set; }

        


        protected MetroDialogSettings OkCancelMessageSettings
        {
            get
            {
                return DialogerSettings("Ok", "Cancel");
            }
        }
        protected MetroDialogSettings YesNoMessageSettings
        {
            get
            {
                return DialogerSettings("Yes", "No");
            }
        }

        protected virtual MetroDialogSettings DialogerSettings(string affirmative,string negative)
        {
            var settings = new MetroDialogSettings()
            {
                AffirmativeButtonText = affirmative,
                NegativeButtonText = negative,
                DefaultButtonFocus = MessageDialogResult.Negative,
                ColorScheme = MetroDialogColorScheme.Inverted,
                 
            };
            return settings;
            
        }

        protected abstract void OnModelChanged();  
    }
}
