using Microsoft.EntityFrameworkCore;

namespace Bookish.Models;

[Index(nameof(Title), nameof(Author), IsUnique = true)]
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public virtual ICollection<Item>? Items { get; set; }
    public Book() {}
    public Book(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
    }
    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }

    public override string ToString()
    {
        return $"Books has Items Count: {Items?.Count}";
    } 
    
}
