//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System.IO;

//namespace EduQuiz.Repository
//{
//    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("C:/Users/Mario/Documents/IKT-Project/edu-quiz-backend/EduQuizWebApplication/appsettings.json") // or appsettings.Development.json
//                .Build();

//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

//            return new ApplicationDbContext(optionsBuilder.Options);
//        }
//    }
//}
