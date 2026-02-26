using Microsoft.EntityFrameworkCore;
using Mission08_Team5.Models;

namespace Mission08_Team5.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Home" },
            new Category { CategoryId = 2, Name = "School" },
            new Category { CategoryId = 3, Name = "Work" },
            new Category { CategoryId = 4, Name = "Church" }
        );

        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem
            {
                TaskId = 1,
                TaskDescription = "Clean the kitchen",
                Quadrant = 1,
                CategoryId = 1,
                Completed = false
            },
            new TaskItem
            {
                TaskId = 2,
                TaskDescription = "Finish IS 413 assignment",
                Quadrant = 2,
                CategoryId = 2,
                Completed = false
            },
            new TaskItem
            {
                TaskId = 3,
                TaskDescription = "Prepare for work presentation",
                Quadrant = 1,
                CategoryId = 3,
                Completed = false
            },
            new TaskItem
            {
                TaskId = 4,
                TaskDescription = "Plan Sunday lesson",
                Quadrant = 2,
                CategoryId = 4,
                Completed = false
            }
        );
    }
}

