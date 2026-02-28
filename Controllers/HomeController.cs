using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team5.Data;
using Mission08_Team5.Models;

namespace Mission08_Team5.Controllers;

public class HomeController : Controller
{
    private readonly ITaskRepository _repo;
    private readonly AppDbContext _context;

    public HomeController(ITaskRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    /// <summary>
    /// Quadrants view - displays all incomplete tasks in the four Covey quadrants.
    /// </summary>
    public IActionResult Index()
    {
        var tasks = _repo.Tasks.Where(t => !t.Completed).ToList();
        return View(tasks);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #region Add Task

    [HttpGet]
    public IActionResult AddTask()
    {
        ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
        return View(new TaskItem());
    }

    [HttpPost]
    public IActionResult AddTask(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            _repo.AddTask(task);
            _repo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
        return View(task);
    }

    #endregion

    #region Edit Task

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = _repo.GetTaskById(id);
        if (task == null)
        {
            return NotFound();
        }
        ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
        return View(task);
    }

    [HttpPost]
    public IActionResult Edit(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            _repo.UpdateTask(task);
            _repo.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
        return View(task);
    }

    #endregion

    #region Delete & Complete

    // Note: Ideally these would be [HttpPost] only. The Quadrants view uses <a> links (GET).
    // For the app to work with current views, we accept GET. Person #3 should update to use
    // <form method="post"> for better practice.
    [HttpGet]
    public IActionResult Delete(int id)
    {
        var task = _repo.GetTaskById(id);
        if (task != null)
        {
            _repo.DeleteTask(task);
            _repo.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Complete(int id)
    {
        var task = _repo.GetTaskById(id);
        if (task != null)
        {
            task.Completed = true;
            _repo.UpdateTask(task);
            _repo.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    #endregion
}