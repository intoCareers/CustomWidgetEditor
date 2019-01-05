using System.ComponentModel;
using CustomWidgetEditor.Models;

namespace CustomWidgetEditor.ViewModels
{
  using System.ComponentModel.DataAnnotations;

  public class WidgetVm
  {
    public int PlanLibCode { get; set; }

    [Required]
    [DisplayName("Title")]
    public string ItemTitle { get; set; }

    [Required]
    [DisplayName("Item Description")]
    public string ItemDescription { get; set; }

    [Range( 0, 100 )]
    [Required]
    [DisplayName("Default Metric")]
    public int DefaultThreshold { get; set; }
    
    [Required]
    [DisplayName("Form Id")]
    public string FormId { get; set; }

    [Required]
    [DisplayName("State")]
    public string State { get; set; }

    [MaxLength(2)]
    [MinLength(2)]
    [DisplayName("State abbreviation")]
    public string StateAbbr { get; set; }
  }
}