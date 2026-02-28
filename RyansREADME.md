Ryans Role 1 Summary (Models / Database / Setup)
================================================

Changes implemented:
- Created entity models in the `Models` folder:
  - `Category` with `CategoryId`, `Name`, and navigation collection of `Tasks`.
  - `TaskItem` with:
    - `TaskId` (PK)
    - `TaskDescription` (required task text)
    - `DueDate` (optional `DateTime?`)
    - `Quadrant` (required int, 1–4)
    - `CategoryId` (FK to `Category`, required)
    - `Category` (navigation property)
    - `Completed` (bool, default `false`)
- Added `AppDbContext` in the `Data` folder:
  - Exposes `DbSet<TaskItem> Tasks` and `DbSet<Category> Categories`.
  - Seeds four categories: Home, School, Work, Church.
  - Seeds a few example `TaskItem` rows for testing.
- Implemented the repository pattern:
  - `ITaskRepository` interface exposes:
    - `IQueryable<TaskItem> Tasks`
    - `GetTaskById(int taskId)`
    - `AddTask`, `UpdateTask`, `DeleteTask`, `SaveChanges`.
  - `EFTaskRepository` (in `Data`) uses `AppDbContext` and includes `Category` via `Include(...)`.
- Configured application startup:
  - Added a SQLite connection string `TaskConnection` pointing to `tasks.sqlite` in `appsettings.json`.
  - Registered `AppDbContext` with `UseSqlite` and `ITaskRepository` with DI in `Program.cs`.
  - Ensured `AddControllersWithViews()` is registered.
  - On startup, the app calls `Database.EnsureCreated()` to create the database and apply seed data automatically.

Resulting data structure:
- Database uses a simple relational schema with two tables:
  - `Categories`
    - Columns: `CategoryId` (PK), `Name`.
    - Seeded rows: Home, School, Work, Church.
  - `Tasks`
    - Columns: `TaskId` (PK), `TaskDescription`, `DueDate`, `Quadrant`, `CategoryId` (FK), `Completed`.
    - `CategoryId` references `Categories.CategoryId`.
- Controllers and views should access tasks through `ITaskRepository`:
  - To get active tasks only: query `_repo.Tasks.Where(t => !t.Completed)`.
  - Quadrants view can group tasks by `Quadrant` (1–4) and display them in the four Covey quadrants.

This setup gives the rest of the team a clean data layer (models + EF Core + repository) and a ready-to-use SQLite database with initial data.

