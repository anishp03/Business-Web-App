using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 


namespace TechTickles.Models;

public class Cart {

    [Key]
    [ForeignKey("Customer")]
    public int AccountId { get; set; }
    
    [ForeignKey("SubscriptionPlan")]
    public int PlanId { get; set; }

    public  Customer Customer { get; set; }
    public SubscriptionPlan SubscriptionPlan { get; set; }

}