using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTickles.Models;

public class CartController : Controller {

  private readonly ApplicationDbContext _context;

  public CartController(ApplicationDbContext context) {
    _context = context;

  }

    [HttpPost]
    public IActionResult Checkout()
    {
        String email = HttpContext.User.Identity.Name;
        var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();  
        var accountId = user1.AccountId;
        
        var cart = _context.Cart.Include(c => c.SubscriptionPlan).FirstOrDefault(c => c.AccountId == accountId);

        
        return View(cart);
    }

    public IActionResult RemoveFromCart(int planId)
    {
        String email = HttpContext.User.Identity.Name;
        var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();  
        var accountId = user1.AccountId;

        var cart = _context.Cart.FirstOrDefault(c => c.AccountId == accountId && c.PlanId == planId);

        if (cart != null)
        {
            _context.Cart.Remove(cart);
            _context.SaveChanges();
        }

        return RedirectToAction("Cart", "Home");
    }
}

