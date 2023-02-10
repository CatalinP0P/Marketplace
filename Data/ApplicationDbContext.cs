using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Marketplace.Models;

namespace Marketplace.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Anunt> Anunturi { get; set; }
    public DbSet<Categorie> Categories { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
