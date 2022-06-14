﻿using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public INavigation Navigation { get; set; }
    }
}
