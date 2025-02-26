using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mission08_Group2_14.Models;

namespace Mission08_Group2_14.Controllers;

public class HomeController : Controller
{

    private QuadrantContext _context;

    public HomeController(QuadrantContext context)
    {
        _context = context;
    }

     public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    public IActionResult AddTask()
    {
        ViewBag.Categories = new SelectList(_context.Categories.OrderBy(c => c.CategoryName), "CategoryId", "CategoryName");

        return View("AddTask", new Task());

    }

    [HttpPost]
    public IActionResult AddTask(Task response)
    {
        ViewBag.Categories = new SelectList(_context.Categories.OrderBy(c => c.CategoryName), "CategoryId", "CategoryName");

        if (ModelState.IsValid)
        {
            _context.Tasks.Add(response); //Add record to the DB
            _context.SaveChanges(); //Save changes to the DB

            return View("AddTask");
        }
        else
        {

            return View("AddTask", response);
        }
    }

    public IActionResult QuadrantView()
    {
        var tasks = _context.Tasks.Include(m => m.Category)
            .Where(t => t.Completed).ToList();

        return View(tasks);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var recordToEdit = _context.Tasks.Single(x => x.TaskId == id);

        ViewBag.Categories = new SelectList(_context.Categories.OrderBy(c => c.CategoryName), "CategoryId", "CategoryName");

        return View("AddTask", recordToEdit);
    }

    [HttpPost]
    public IActionResult Edit(Task updatedInfo)
    {
        _context.Update(updatedInfo);
        _context.SaveChanges();

        return RedirectToAction("QuadrantView");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var recordToDelete = _context.Tasks.Single(x => x.TaskId == id);

        return View(recordToDelete);
    }

    [HttpPost]
    public IActionResult Delete(Task specTask)
    {
        _context.Tasks.Remove(specTask);
        _context.SaveChanges();

        return RedirectToAction("QuadrantView");
    }

}
