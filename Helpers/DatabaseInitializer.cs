using waves_server.Models;

namespace waves_server.Helpers;

public static class DatabaseInitializer {
  public static void SeedData(IServiceProvider serviceProvider) {
    using (var scope = serviceProvider.CreateScope()) {
      var context = scope.ServiceProvider.GetService<DatabaseContext>();

      if (context == null) {
        throw new ArgumentNullException(nameof(context), "Null DatabaseContext");
      }
      SeedUsers(context);
    }
  }

  private static void SeedUsers(DatabaseContext context) {
    if (context.Users.Any())
      return;
    context.Users.AddRange(
      new User
      {
        UserId = Guid.NewGuid(),
        Username = "admin",
        Password = BCrypt.Net.BCrypt.HashPassword("admin123"),
        LegalName = "Admin",
        Email = "admin@gmail.com",
        MobileNumber = "1234567890",
        Country = "IND",
        Type = "Admin",
      },
      new User
      {
        UserId = Guid.NewGuid(),
        Username = "admin2",
        Password = BCrypt.Net.BCrypt.HashPassword("admin456"),
        LegalName = "Admin2",
        Email = "admin2@gmail.com",
        MobileNumber = "2345678901",
        Country = "USA",
        Type = "Admin",
      },
      new User
      {
        UserId = Guid.NewGuid(),
        Username = "user",
        Password = BCrypt.Net.BCrypt.HashPassword("user123"),
        LegalName = "User",
        Email = "user@gmail.com",
        MobileNumber = "3456789012",
        Country = "GBR",
        Type = "User",
      },
      new User
      {
        UserId = Guid.NewGuid(),
        Username = "user2",
        Password = BCrypt.Net.BCrypt.HashPassword("user456"),
        LegalName = "User2",
        Email = "user2@gmail.com",
        MobileNumber = "4567890123",
        Country = "SGP",
        Type = "User",
      }
    );

    context.SaveChanges();
  }
}
