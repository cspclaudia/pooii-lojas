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
                if (context.Loja.Any())
                {
                    return;   // DB has been seeded
                }

                // context.Loja.AddRange(
                //     new Loja
                //     {
                //         Title = "When Harry Met Sally",
                //         ReleaseDate = DateTime.Parse("1989-2-12"),
                //         Genre = "Romantic Comedy",
                //         Price = 7.99M
                //     },

                //     new Loja
                //     {
                //         Title = "Ghostbusters ",
                //         ReleaseDate = DateTime.Parse("1984-3-13"),
                //         Genre = "Comedy",
                //         Price = 8.99M
                //     },

                //     new Loja
                //     {
                //         Title = "Ghostbusters 2",
                //         ReleaseDate = DateTime.Parse("1986-2-23"),
                //         Genre = "Comedy",
                //         Price = 9.99M
                //     },

                //     new Loja
                //     {
                //         Title = "Rio Bravo",
                //         ReleaseDate = DateTime.Parse("1959-4-15"),
                //         Genre = "Western",
                //         Price = 3.99M
                //     }
                // );
                context.SaveChanges();
            }
        }
    }
}