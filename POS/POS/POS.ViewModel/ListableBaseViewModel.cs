using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel
{
    public abstract class ListableBaseViewModel:BaseViewModel 
    {
        protected int page, size, total;

        public ListableBaseViewModel()
        {
            NextCommand = new DelegateCommand(GoNext, CanGoNext);
            PreviousCommand = new DelegateCommand(GoPrevious, CanGoPrevious);
            FirstCommand = new DelegateCommand(GoFirst, CanGoFirst);
            LastCommand = new DelegateCommand(GoLast, CanGoLast);
            Page = 1;
            Size = 20;
        }

        public int Page
        {
            get
            {
                return page;
            }
            set
            {
                SetProperty<int>(ref page, value, OnModelChanged);
            }
        }

        public int Size
        {
            get { return size; }
            set
            {
                SetProperty<int>(ref size, value, OnModelChanged);
            }
        }

        public int Total
        {
            get { return total; }
            set
            {
                SetProperty<int>(ref total, value, OnModelChanged);
            }
        }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((double)Total / Size); }
        }

        public DelegateCommand NextCommand { get; }
        public DelegateCommand PreviousCommand { get; }
        public DelegateCommand FirstCommand { get; }
        public DelegateCommand LastCommand { get; }

        protected virtual bool CanGoFirst()
        {
            return Page > 1;
        }

        protected virtual void GoFirst()
        {
            Page = 1;
            GetItems(Page, Size);
        }

        protected virtual bool CanGoLast()
        {
            return Page < TotalPages;
        }

        protected virtual void GoLast()
        {
            Page = TotalPages;

            GetItems(Page, Size);
        }

        protected virtual bool CanGoNext()
        {
            return Page < TotalPages;
        }

        protected virtual void GoNext()
        {
            Page += 1;
            GetItems(Page, Size);
        }

        protected virtual bool CanGoPrevious()
        {
            return Page > 1;
        }

        protected virtual void GoPrevious()
        {
            Page -= 1;
            GetItems(Page, Size);
        }

        protected abstract void GetItems(int page, int size);
    }
}
