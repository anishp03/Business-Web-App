using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTickles.Models;

public class CheckoutController : Controller
{
    private readonly ApplicationDbContext _context;

    public CheckoutController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public IActionResult Checkout(CreditCard card)
    {

        String email = HttpContext.User.Identity.Name;
        var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();  
        var accountId = user1.AccountId;
        
        var customer = _context.Customers.Include(c => c.cardInfo).Include(s => s.Subscription).FirstOrDefault(c => c.AccountId == accountId);
        var cart = _context.Cart.Include(c => c.Customer).Include(s => s.SubscriptionPlan).FirstOrDefault(c => c.AccountId == accountId);

        if(customer.cardInfo == null)
        {
             card.AccountId = customer.AccountId;
            _context.CreditCards.Add(card);
            customer.cardInfo = card;
        }
        else
        {
          customer.cardInfo.CardNumber = card.CardNumber;
          customer.cardInfo.Expiration = card.Expiration;
          customer.cardInfo.Type = card.Type;
          customer.cardInfo.CVV = card.CVV;
          _context.CreditCards.Update(customer.cardInfo);
        }

          customer.Subscription = new CustomerSubscription
          {
            AccountId = customer.AccountId,
            PlanId = cart.PlanId,
            StartDate = DateTime.Now,
            IsActive = true,
            EndDate = DateTime.Now.AddYears(1)
          };
          
          _context.Customers.Update(customer);
          _context.Cart.Remove(cart);        
          _context.SaveChanges();

        return RedirectToAction("Account", "Home");
    }
}