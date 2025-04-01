using Bookish.Models;
namespace Bookish.Utils
{
    public class SeedDataHelpers 
    {
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