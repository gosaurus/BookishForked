using Microsoft.EntityFrameworkCore;
using Bookish.Models;
using Bookish.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Bookish.Utils;

namespace Bookish
{
    public static class SeedData
    {
        
        public static async Task InitialiseBooks(BookishDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "Dbcontext can't be null");
            }

            if (context.Book.Any()) return;
            
            var bookDetails = DataExtractionHelper.SeedDataToList();
            List<Book> booksToAdd = [];

            for (var bookCount = 1; bookCount < bookDetails.Count; bookCount++)
            {
                List<string> bookRow = bookDetails[bookCount].Split(',').ToList();
                //This does not handle titles that have ", The", followed by Author! 
                if (bookRow.Count == 4 && bookRow[2] == " The")
                {
                    var newTitle = bookRow[1] + "," + bookRow[2];
                    bookRow[1] = newTitle;
                    bookRow.RemoveAt(2);
                    Console.WriteLine($"Book at index 2= {bookRow[2]}");
                }
                booksToAdd.Add(new Book {Title = bookRow[1].Trim(['"']), Author = bookRow[2].Trim(['"'])});
            };

            var allBooks = await context.Book.ToListAsync();

            foreach (var book in booksToAdd)
            {
                if (SeedDataHelpers.checkIfBookInDB(allBooks, book))
                    context.Add(book);
            
            await context.SaveChangesAsync();
            }
        }

        public static async Task InitialiseItems(BookishDbContext context) 
        {
            var allBooks = await context.Book.ToListAsync(); 

            if (!context.Item.Any())
            {
            
                List<Item> Items = new List<Item>();
                
                for (var index = 1; index <= 50; index++)
                {
                    Book randomBook = allBooks[Random.Shared.Next(1,26)];
                    if (randomBook != null)
                    {
                        Items.Add(new Item {BookId = randomBook.Id, Book = randomBook});
                    }
                }
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
       
    }
}