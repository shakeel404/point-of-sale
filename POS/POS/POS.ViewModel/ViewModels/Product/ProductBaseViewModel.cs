using System;
using System.Collections.Generic;
using System.Text;

namespace POS.ViewModel.ViewModels.Product
{
    public abstract class ProductBaseViewModel<IR> : DeletableViewModel<IR> where IR : class
    {

        protected string upc, name;
        protected int qty;
        protected double purchasePrice, salePrice;

        public virtual int UnitProfit
        {
            get
            {
                return (int)(SalePrice - PurchasePrice);
            }
        }


        public virtual int TotalProfit
        {
            get
            {
                return (int)(UnitProfit * Quantity);
            }
        } 

        public virtual string UPC
        {
            get
            {
                return upc;
            }
            set
            {
                if (upc != value)
                    SetProperty<string>(ref upc, value);
            }
        }

        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                    SetProperty<string>(ref name, value, OnModelChanged);
            }
        }

        public virtual int Quantity
        {
            get
            {
                return qty;
            }
            set
            {
                if (qty != value)
                    SetProperty<int>(ref qty, value, OnModelChanged);
            }
        }

        public virtual double PurchasePrice
        {
            get
            {
                return purchasePrice;
            }
            set
            {
                if (purchasePrice != value)
                    SetProperty<double>(ref purchasePrice, value, OnModelChanged);
            }
        }

        public virtual double SalePrice
        {
            get
            {
                return salePrice;
            }
            set
            {
                if (salePrice != value)
                    SetProperty<double>(ref salePrice, value, OnModelChanged);
            }
        }
    }
}
