using System;
using System.Collections.Generic;
using DoToo.ViewModels;
using Xamarin.Forms;
using System.Windows.Input;

namespace DoToo.Views
{
    public partial class ItemView : ContentPage
    {
        public ItemView(ItemViewModel viewModel)
        {
            InitializeComponent();
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
        }
    }
}
