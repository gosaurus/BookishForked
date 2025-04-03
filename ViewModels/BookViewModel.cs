using Microsoft.EntityFrameworkCore;
using Bookish.Models;

namespace Bookish.ViewModels;


public class BookViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string Author { get; set; }

    public virtual ICollection<Item>? Items { get; set; }

    public BookViewModel(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Author = book.Author;
        Items = book.Items;
    }
}