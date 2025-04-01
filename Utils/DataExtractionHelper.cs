namespace Bookish.Utils
{    public class DataExtractionHelper
    {
        public static List<string> SeedDataToList()
        {
            string filePath = "../BookishForked/RawData/Books.csv";
            StreamReader reader = null!;

            reader = new StreamReader(File.OpenRead(filePath));
            List<string> bookDetails = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line != null)
                    bookDetails.Add(line);
            }
            return bookDetails;
        }
    } 
}