using Microsoft.EntityFrameworkCore;

namespace SmartLibrary.API.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relationships
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId);

            modelBuilder.Entity<BorrowRecord>()
                .HasOne(br => br.Member)
                .WithMany(m => m.BorrowRecords)
                .HasForeignKey(br => br.MemberId);

            modelBuilder.Entity<BorrowRecord>()
                .HasOne(br => br.Book)
                .WithMany()
                .HasForeignKey(br => br.BookId);

            // Seed Data
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling", Biography = "British author", Nationality = "British" },
                new Author { Id = 2, Name = "George Orwell", Biography = "English novelist", Nationality = "British" },
                new Author { Id = 3, Name = "Leo Tolstoy", Biography = "Russian writer", Nationality = "Russian" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction" },
                new Category { Id = 2, Name = "Non-Fiction" },
                new Category { Id = 3, Name = "Science" }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, ISBN = "978-0439708180", Title = "Harry Potter", Description = "A wizard story", PublishedYear = 1997, Price = 25.99m, AvailableCopies = 5, IsAvailable = true, AuthorId = 1, CategoryId = 1 },
                new Book { Id = 2, ISBN = "978-0451524935", Title = "1984", Description = "Dystopian novel", PublishedYear = 1949, Price = 15.99m, AvailableCopies = 3, IsAvailable = true, AuthorId = 2, CategoryId = 1 },
                new Book { Id = 3, ISBN = "978-1400079988", Title = "War and Peace", Description = "Historical novel", PublishedYear = 1869, Price = 35.99m, AvailableCopies = 0, IsAvailable = false, AuthorId = 3, CategoryId = 1 },
                new Book { Id = 4, ISBN = "978-0451526342", Title = "Animal Farm", Description = "Political allegory", PublishedYear = 1945, Price = 12.99m, AvailableCopies = 7, IsAvailable = true, AuthorId = 2, CategoryId = 2 },
                new Book { Id = 5, ISBN = "978-0140449136", Title = "Anna Karenina", Description = "Romantic novel", PublishedYear = 1878, Price = 29.99m, AvailableCopies = 2, IsAvailable = true, AuthorId = 3, CategoryId = 1 }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member { Id = 1, Name = "Kasun Perera", Email = "kasun@gmail.com", Phone = "0771234567", MembershipDate = new DateTime(2024, 1, 15) },
                new Member { Id = 2, Name = "Nimali Silva", Email = "nimali@gmail.com", Phone = "0779876543", MembershipDate = new DateTime(2024, 3, 20) }
            );
        }
    }
}