namespace CustomWidgetEditor.ViewModels
{
  using System.ComponentModel.DataAnnotations;

  public class WidgetVm
  {
    public int PlanLibCode { get; set; }

    [Required]
    public string ItemTitle { get; set; }

    [Required]
    public string ItemDescription { get; set; }

    [Range( 0, 100 )]
    [Required]
    public int DefaultThreshold { get; set; }

    public string FormId { get; set; }

    [Required]
    public string State { get; set; }
  }
}