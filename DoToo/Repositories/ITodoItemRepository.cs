using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoToo.Models;

namespace DoToo.Repositories
{
    public interface ITodoItemRepository
    {

        event EventHandler<TodoItem> onItemAdded;
        event EventHandler<TodoItem> onItemUpdated;

        Task<List<TodoItem>> GetItems();

        Task AddItem(TodoItem item);

        Task UpdateItem(TodoItem item);

        Task AddorUpdate(TodoItem item);

    }
}
