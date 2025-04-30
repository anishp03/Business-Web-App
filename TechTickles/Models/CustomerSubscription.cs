using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace TechTickles.Models;

public class CustomerSubscription {
  
  [Key]
  [ForeignKey("Customer")]
  public int AccountId { get; set; }

  [ForeignKey("Plan")]
  public int PlanId { get; set; }

  public DateTime StartDate { get; set; }
  public DateTime? EndDate{ get; set;}
  public bool IsActive { get; set; } = false;
  public Customer Customer { get; set; }
  public SubscriptionPlan Plan { get; set; }

}
