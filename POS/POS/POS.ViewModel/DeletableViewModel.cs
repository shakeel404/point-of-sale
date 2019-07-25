using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel
{
    public abstract class DeletableViewModel<IT>:BaseViewModel where IT:class
    {
        public IT Repository { get; set; }
        public DeletableViewModel()
        { 
            DeleteCommand = new DelegateCommand(Delete, CanDelete); 
        }

        public DelegateCommand DeleteCommand { get;}

        public virtual int Id { get; set; }
        protected virtual bool CanDelete()
        {
          return  Id > 0;
        }

        protected abstract void Delete();
    }
}
