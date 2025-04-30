using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 


namespace TechTickles.Models;

public class Review {

    [Key]
    public int ReviewId { get; set; }
    
    [ForeignKey("Customer")]
    public int AccountId { get; set; }

    [Required(ErrorMessage = "Stars Required")]
    [Range(1, 5, ErrorMessage = "Stars must be between 1-5")]
    [Display(Name = "Stars(1 - 5)")]
    public int Stars {  get; set; }

    [Required(ErrorMessage = "Title Required")]
    public string Title{ get; set; }

    [Required(ErrorMessage = "Description Required")]
    public string Description{get; set;}
    public DateTime Date { get; set; } = DateTime.Now;
    public Customer Customer { get; set; }
}