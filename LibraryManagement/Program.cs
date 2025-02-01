using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement
{
    
    public class Book
    {
        public Guid Id { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsAvailable { get; set; }

        public Book(string title, string author, string isbn)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            ISBN = isbn;
            IsAvailable = true;
        }
    }

    
    public class Library
    {
        private List<Book> books;

        public Library()
        {
            books = new List<Book>();
        }

       
        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to the library.");
        }

        
        public List<Book> SearchBooks(string searchTerm)
        {
            return books.Where(b =>
                b.Title.IndexOf(searchTerm.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                b.Author.IndexOf(searchTerm.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                b.ISBN.IndexOf(searchTerm.Trim(), StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();
        }

        
        public bool BorrowBook(string title)
        {
            Book book = books.FirstOrDefault(b => b.Title.Contains(title.Trim(), StringComparison.OrdinalIgnoreCase) && b.IsAvailable);

            if (book != null)
            {
                book.IsAvailable = false;
                Console.WriteLine($"Book '{book.Title}' has been borrowed.");
                return true;
            }
            Console.WriteLine($"Book with title containing '{title}' is not available or not found.");
            return false;
        }

        
        public bool ReturnBook(string title)
        {
            Book book = books.FirstOrDefault(b => b.Title.Contains(title.Trim(), StringComparison.OrdinalIgnoreCase) && !b.IsAvailable);

            if (book != null)
            {
                book.IsAvailable = true;
                Console.WriteLine($"Book '{book.Title}' has been returned.");
                return true;
            }
            Console.WriteLine($"Book with title containing '{title}' was not borrowed or not found.");
            return false;
        }
    }

    
    public class Program
    {
        public static void Main()
        {
            Library library = new Library();

           
            library.AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "9780743273565"));
            library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
            library.AddBook(new Book("1984", "George Orwell", "9780451524935"));


            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1 - Search for a book");
                Console.WriteLine("2 - Borrow a book");
                Console.WriteLine("3 - Return a book");
                Console.WriteLine("4 - Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter search term: ");
                        string searchTerm = Console.ReadLine();
                        List<Book> results = library.SearchBooks(searchTerm);
                        if (results.Any())
                        {
                            Console.WriteLine("Search results:");
                            foreach (var book in results)
                            {
                                Console.WriteLine($"- {book.Title} by {book.Author} (Available: {book.IsAvailable})");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No matching books found.");
                        }
                        break;
                    case "2":
                        Console.Write("Enter the title of the book to borrow: ");
                        string titleToBorrow = Console.ReadLine();
                        library.BorrowBook(titleToBorrow);
                        break;
                    case "3":
                        Console.Write("Enter the title of the book to return: ");
                        string titleToReturn = Console.ReadLine();
                        library.ReturnBook(titleToReturn);
                        break;
                    case "4":
                        Console.WriteLine("Exiting the program. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
