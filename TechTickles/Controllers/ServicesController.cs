using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechTickles.Models;

public class ServicesController : Controller {

  private readonly ApplicationDbContext _context;

    public ServicesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
  [Authorize]
  [HttpPost]
  public IActionResult AddToCart(int planId)
    {
        //getting accountId from session to store the cart
        String email = HttpContext.User.Identity.Name;
        var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();  

        var accountId = user1.AccountId;

        //creates a plan variable to store the planId
        var plan = _context.SubscriptionPlans.FirstOrDefault(p => p.PlanId == planId);

        //getting customer information
        var customer = _context.Customers.FirstOrDefault(c => c.AccountId == accountId);

        //creates a cart variable to store the cartId and checking if it exists
        var cart = _context.Cart.FirstOrDefault(c => c.AccountId == accountId);

        //if cart exits, update it
        //if not then make a new cart and add planId and accountId
        if (cart == null)
        {
            cart = new Cart
            {
                AccountId = customer.AccountId,
                PlanId = plan.PlanId,
                SubscriptionPlan = plan,
                Customer = customer
            };
            _context.Cart.Add(cart);
        }
        else
        {
            cart.PlanId = plan.PlanId;
            cart.SubscriptionPlan = plan;
            _context.Cart.Update(cart);
        }

        _context.SaveChanges();
        
        return RedirectToAction("Cart", "Home");
    }
}