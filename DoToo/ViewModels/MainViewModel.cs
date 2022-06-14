using System;
using System.Threading.Tasks;
using DoToo.Repositories;
using System.Windows.Input;
using DoToo.Views;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using DoToo.Models;

namespace DoToo.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly TodoItemRepository repository;

        public ObservableCollection<TodoItemViewModel> Items { get; set; }


        public ICommand AddItem => new Command(async ()
            =>
        {
            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync(itemView);
        });

        public string FilterText => ShowAll ? "All" : "Active";

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });

        public TodoItemViewModel SelectedItem
        {
            get { return null; }

            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private async Task NavigateToItem(TodoItemViewModel item)
        {
            if (item == null)
            {
                return;
            }



            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item.Copy();

            await Navigation.PushAsync(itemView);
            //come

        }

        public MainViewModel(TodoItemRepository repository)
        {

            repository.onItemAdded += (sender, item) => Items.Add(CreateTodoItemviewModel(item));
            repository.onItemUpdated += (sender, item) => Task.Run(async () => await LoadData());


            this.repository = repository;
            Task.Run(async () => await LoadData());
        }

        private async Task LoadData()
        {


            var items = await repository.GetItems();

            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }

            var itemViewModels = items.Select(i =>
            CreateTodoItemviewModel(i));
            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
            
        }

        private TodoItemViewModel CreateTodoItemviewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is TodoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }

                Task.Run(async () => await repository.UpdateItem(item.Item));
            }
        }

        public bool ShowAll { get; set; }

    }
}
