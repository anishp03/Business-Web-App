using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTickles.Models;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public HomeController(ApplicationDbContext context) 
    {
        _context = context;
    }

    public IActionResult Index()
    {
        
        var features = _context.Features.ToList();

        return View(features);
    }

    public IActionResult Services()
    {
        var subscriptionPlans = _context.SubscriptionPlans.Include(s => s.Feature).ToList();
        return View(subscriptionPlans);
    }

    public IActionResult ViewReviews()
    {
        var sortedReviews = _context.Reviews.OrderByDescending(r => r.Date).Include(r => r.Customer).ToList();
        return View(sortedReviews);
    }

    public IActionResult AboutUs()
    {
        return View();
    }
    public IActionResult News()
    {
        return View();
    }
    
    [HttpGet]
    public async Task<IActionResult> Account()
    {
        String email = HttpContext.User.Identity.Name;
        var user1 = await _context.Customers.Include(r => r.reviews).Include(c => c.cardInfo).Include(c => c.Subscription).ThenInclude(s => s.Plan).FirstOrDefaultAsync(x => x.Email == email);

        return View(user1); 
    }

    [Authorize]
    public IActionResult Review()
    {
        return View();
    }


    public IActionResult FAQ()
    {
        return View();
    }

    [Authorize]
    public IActionResult Cart()
    {
        // Retrieve the account ID from the session and checking if null, if null then redirect to login page
        String email = HttpContext.User.Identity.Name;
        var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();  

        var accountId = user1.AccountId;

        //reteriving cart information from the database using the account ID and checking if it exists
        var cart = _context.Cart.Include(c => c.SubscriptionPlan).FirstOrDefault(c => c.AccountId == accountId);

        return View(cart);

    }
}
