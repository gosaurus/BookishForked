using Microsoft.EntityFrameworkCore;
using Bookish.Models;
using Bookish.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Bookish
{
    public static class SeedData
    {
        public static async Task Initialise(BookishDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "Dbcontext can't be null");
            }
            var Books = new List<Book>
            {
                new Book { Title="Educated", Author="Tara Westover" },
                new Book { Title="Unbearable lightness of being, The", Author="Milan Kundera" },
                new Book { Title="Orlando", Author="Virginia Woolf" },
                new Book { Title="Wolf Totem", Author="Jiang Rong" },
                new Book { Title="Black Swan", Author="Nicolas Taleb" },
                new Book { Title="Alice In Wonderland", Author="Lewis Carroll" },
            };

            var allBooks = await context.Book.ToListAsync();

            foreach (var book in Books)
            {
                if (checkIfBookInDB(allBooks, book))
                    context.Add(book);
            }

            if (!context.Item.Any())
            {
                var Items = new List<Item>
                { 
                    new Item { Book=Books[0] },
                    new Item { Book=Books[0] },
                    new Item { Book=Books[0] },
                    new Item { Book=Books[1]},
                    new Item { Book=Books[1]},
                    new Item { Book=Books[1]},
                    new Item { Book=Books[2]},
                    new Item { Book=Books[3]},
                    new Item { Book=Books[4]},
                    new Item { Book=Books[4]},
                    new Item { Book=Books[4]},
                    new Item { Book=Books[4]},
                    new Item { Book=Books[5]},
                    new Item { Book=Books[5]},
                };
                context.AddRange(Items);
                }
            if (!context.User.Any())
            {
                var Users = new List<User>
                {
                    new User { Name="Tmy Sonibor", Email="amy.r@btinternet.com"},
                    new User { Name="Vim Neppoc", Email="supervimuser@vim.org" },
                    new User { Name="Lawn Mower", Email="brummie@guy.com" },
                    new User { Name="Hamish Dawg", Email="moneybros@aol.net" },
                };
                context.AddRange(Users);
            }
            await context.SaveChangesAsync();
        }
        public static bool checkIfBookInDB(List<Book> books, Book book)
        {
            foreach (Book bookInDb in books)
            {
                if (bookInDb.Title == book.Title)
                        return false; 
            }
            return true;
        }
    }
}