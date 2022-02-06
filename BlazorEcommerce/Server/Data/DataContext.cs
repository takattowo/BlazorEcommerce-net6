namespace BlazorEcommerce.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Book1",
                    Description = "dsfsdfsdfdsxzfdfsdf",
                    ImageUrl = "https://livedemo00.template-help.com/wt_prod-19881/images/book-1-210x325.png",
                    Price = 10
                },

                new Product
                {
                    Id = 2,
                    Title = "Book2",
                    Description = "4f324f32sdsasef",
                    ImageUrl = "https://livedemo00.template-help.com/wt_prod-19881/images/book-2-210x325.png",
                    Price = 20
                },

                new Product
                {
                    Id = 3,
                    Title = "Book3",
                    Description = "432f4fjghjfgrtrtet",
                    ImageUrl = "https://livedemo00.template-help.com/wt_prod-19881/images/book-3-210x325.png",
                    Price = 30
                }
            );
            
        }

        public DbSet<Product> Products { get; set; }
    }
}
