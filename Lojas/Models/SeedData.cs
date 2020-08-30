using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Lojas.Data;
using System;
using System.Linq;

namespace Lojas.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LojasContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<LojasContext>>()))
            {
                // Look for any Lojas.
                // if (context.Loja.Any())
                // {
                //     return;   // DB has been seeded
                // }

                // context.Loja.AddRange(
                //     new Loja
                //     {
                //         Title = "When Harry Met Sally",
                //         ReleaseDate = DateTime.Parse("1989-2-12"),
                //         Genre = "Romantic Comedy",
                //         Price = 7.99M
                //     }
                // );
                context.SaveChanges();
            }
        }
    }
}