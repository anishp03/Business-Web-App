using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTickles.Models;

public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Review(Review model)
    {

        String email = HttpContext.User.Identity.Name;
        var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();

        var accountId = user1.AccountId;

        Review review = new Review();
        review.AccountId = accountId;
        review.Title = model.Title;
        review.Description = model.Description;
        review.Stars = model.Stars;

        if (model.Stars < 1 || model.Stars > 5)
        {
            ModelState.AddModelError("Stars", "Stars must be between 1 and 5.");
            return View(model);
        }

        try
        {
            _context.Reviews.Add(review);

            _context.SaveChanges();

            ModelState.Clear();
            ViewBag.Message = "Review Posted Successfully.";
        }
        catch (DbUpdateException e)
        {
            return View(model);
        }

        return View();
        
    }
}