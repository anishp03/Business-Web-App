using System.ComponentModel.DataAnnotations;

namespace TechTickles.Models; 

public class SubscriptionPlan {

  [Key]
  public int PlanId{get; set;}

  public string Name{get; set;}
  public double Price{get; set;}
  public string Description{get; set;}

  public List<Feature> Feature { get; set; } = new();
  public List<CustomerSubscription> CustomerSubscriptions { get; set; } = new();

  public List<Cart> Carts { get; set; } = new();
}
