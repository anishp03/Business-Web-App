using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace TechTickles.Models;

public class CreditCard{

  [Key]
  [ForeignKey("CardOwner")]
  public int AccountId{get; set;}

  public long CardNumber{get; set;}

  public string Expiration {get; set;}

  public string Type{get; set;}

  public int CVV{get; set;}

  public Customer CardOwner { get; set; }
  
}