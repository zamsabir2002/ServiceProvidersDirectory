//using Microsoft.EntityFrameworkCore;

//namespace ServiceProvidersDirectory.Data
//{
//    public static class RoleNameDataExtension
//    {
//        public static async Task MigrateDbAsync(this WebApplication app)
//        {
//            using var scope = app.Services.CreateScope(); // can use to request the service container of asp.net core to give us instance to some services regiestered in this app

//            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//            await dbContext.Database.MigrateAsync();
//        }
//    }
//}
