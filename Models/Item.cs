using System.ComponentModel.DataAnnotations.Schema;

namespace Bookish.Models;

public class Item
{
    public int Id { get; set; }
    public required int BookId { get; set; }
    public required Book Book { get; set; }
    public User? User { get; set; }
}