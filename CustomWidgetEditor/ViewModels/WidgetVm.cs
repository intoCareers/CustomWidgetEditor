using System.ComponentModel;

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
    [DisplayName("")]
    public int DefaultThreshold { get; set; }

    public string FormId { get; set; }

    [Required]
    public string State { get; set; }
  }
}