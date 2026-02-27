using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team5.Models;
using System.Linq;

namespace Mission08_Team5.Controllers;

public class HomeController : Controller
{
    private readonly ITaskRepository _repo;

    public HomeController(ITaskRepository repo)
    {
        _repo = repo;
    }

    public IActionResult Index()
    {
        // display all tasks (including completed? view filters)
        var tasks = _repo.Tasks.ToList();
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

    // Additional actions for edit/delete/complete
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var task = _repo.GetTaskById(id);
        if (task == null)
        {
            return NotFound();
        }
        ViewBag.Categories = _repo.Tasks.Select(t => t.Category).Distinct().ToList();
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
        ViewBag.Categories = _repo.Tasks.Select(t => t.Category).Distinct().ToList();
        return View(task);
    }

    [HttpPost]
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

    [HttpPost]
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
}