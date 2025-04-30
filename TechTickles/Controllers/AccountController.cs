using System.Security.Claims;
using TechTickles.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace TechTickles.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer account = new Customer();
                account.Email = model.Email;
                account.FirstName = model.FirstName;
                account.LastName = model.LastName;
                account.Password = model.Password;
                account.UserName = model.UserName;

                Cart cart = new Cart
                
                {
                    AccountId = account.AccountId,
                    SubscriptionPlan = null // Initialize with null or set a default plan if needed
                };

                try
                {
                    _context.Customers.Add(account);
                    
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please login.";
                }
                catch (DbUpdateException e)
                {
                    ModelState.AddModelError("", "Please enter unique Email and Username");
                    return View(model);
                }

                return View();
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Customers.Where(x => (x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail) && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {

                    //Sucess, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.FirstName),
                        new Claim(ClaimTypes.Role, "User"),
                    };
                    
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    //Logs in user and creates a cookie

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email or Password Invalid");
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult UpdateCard(CreditCard card)
        {
            String email = HttpContext.User.Identity.Name;

            var user1 = _context.Customers.Include(c => c.cardInfo).FirstOrDefault(x => x.Email == email);            
            var accountId = user1.AccountId;

            if(user1.cardInfo == null)
            {
                card.AccountId = user1.AccountId;
                _context.CreditCards.Add(card);
                user1.cardInfo = card;
            }
            else{
                user1.cardInfo.CardNumber = card.CardNumber;
                user1.cardInfo.Expiration = card.Expiration;
                user1.cardInfo.Type = card.Type;
                user1.cardInfo.CVV = card.CVV;
                _context.CreditCards.Update(user1.cardInfo);
            }
            
            _context.SaveChanges();

            return RedirectToAction("Account", "Home");
        }
        public IActionResult RemoveCard()
        {
            String email = HttpContext.User.Identity.Name;
            var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();
            var accountId = user1.AccountId;

            var Card = _context.CreditCards.FirstOrDefault(c => c.AccountId == accountId);

            _context.CreditCards.Remove(Card);
            _context.SaveChanges();

            return RedirectToAction("Account", "Home");
        }

        public IActionResult CancelSubscription()
        {
            String email = HttpContext.User.Identity.Name;
            var user1 = _context.Customers.Where(x => (x.Email == email).Equals(true)).FirstOrDefault();  
            var accountId = user1.AccountId;

            var subscriptionPlan = _context.CustomerSubscriptions.FirstOrDefault(cs => cs.AccountId == accountId);
            _context.CustomerSubscriptions.Remove(subscriptionPlan);
            _context.SaveChanges();

            return RedirectToAction("Account", "Home");
        }
    }
}