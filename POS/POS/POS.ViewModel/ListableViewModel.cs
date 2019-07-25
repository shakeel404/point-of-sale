using AutoMapper;
using POS.Repository;
using POS.ViewModel.Utils;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace POS.ViewModel
{
    public abstract class ListableViewModel<AddEvent,RemoveEvent,T,TVM> : ListableBaseViewModel
      where T:class where AddEvent:PubSubEvent<T>,new () where RemoveEvent :PubSubEvent<T>,new()
    {
        private readonly string _orderProperty;
        private readonly string _matchProperty;
        private readonly Type _matchType;
        

        public ListableViewModel(IRepository<T> repository,IEventAggregator ea, TypeCode matchType=TypeCode.Int32, string matchProperty = "Id",string orderProperty="Id" )
        {
             EA = ea;
            _orderProperty = orderProperty;
            _matchProperty = matchProperty;  
            _matchType = Type.GetType("System."+Enum.GetName(typeof(TypeCode),matchType));
            Repository = repository;
            EA.GetEvent<AddEvent>().Subscribe(OnAdded);
            EA.GetEvent<RemoveEvent>().Subscribe(OnRemoved);
            Items = new ObservableCollection<TVM>();

            
        }

       

        public IRepository<T> Repository { get; }
        public ObservableCollection<TVM> Items { get; }

        

        protected virtual void OnAdded(T item)
        {
            var itemVM = Mapper.Map<TVM>(item);
            Items.Insert(0, itemVM);
        }
        protected virtual void OnRemoved(T item)
        { 

            var itemVM = Items.FirstOrDefault(i =>
                               i.GetType().GetProperty(_matchProperty,_matchType).GetValue(i).ToString()
                               ==
                              item.GetType().GetProperty(_matchProperty,_matchType).GetValue(item).ToString());

            Items.Remove(itemVM);
        }
        protected virtual void Get()
        {
            GetItems(Page, Size);
            
        }

        protected override void GetItems(int topage, int ofsize)
        {
            Total = Repository.Get().Count();

            Items.Clear();
            var items = Repository.Get(topage, ofsize).OrderByDescending(t => t.GetType().GetProperty(_orderProperty)).Select(Mapper.Map<T, TVM>);

            foreach (var item in items)
                Items.Add(item);

            OnModelChanged();
        }


        protected override void OnModelChanged()
        {
            NextCommand.RaiseCanExecuteChanged();
            PreviousCommand.RaiseCanExecuteChanged();
            FirstCommand.RaiseCanExecuteChanged();
            LastCommand.RaiseCanExecuteChanged();

            RaisePropertyChanged(nameof(TotalPages));
        }
    }
}
