using System.ComponentModel.DataAnnotations;


namespace TechTickles.Models; 

public class Feature{

  [Key]
  public int FeatureId{get; set;}

  public string Name{get; set;}
  public string Descripton{get; set;}

  public List<SubscriptionPlan> SubscriptionPlan { get; set; } = new();
}