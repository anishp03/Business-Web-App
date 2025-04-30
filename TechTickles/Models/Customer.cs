using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechTickles.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(UserName), IsUnique = true)]
public class Customer {

  [Key]
  public int AccountId{get; set;}

   [Required(ErrorMessage = "First Name Required")]
    public String FirstName { get; set; }

    [Required(ErrorMessage = "Last Name Required")]
    public String LastName { get; set; }

    [Required(ErrorMessage = "Email Required")]
    [StringLength(255)]
    [Column(TypeName = "varchar(255)")]
    public String Email { get; set; }

    [Required(ErrorMessage = "Username Required")]
    public String UserName { get; set; }

    [Required(ErrorMessage = "Password Required")]
    public String Password { get; set; }

    public CreditCard cardInfo {get; set;}
    public List<Review> reviews { get; set; }
    public CustomerSubscription Subscription { get; set; }
    public Cart Cart { get; set; }
}
