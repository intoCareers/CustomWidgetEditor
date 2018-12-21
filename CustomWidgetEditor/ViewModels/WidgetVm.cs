using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomWidgetEditor.ViewModels
{
  public class WidgetVm
  {
    public int Id { get; set; }

    [Required]
    public string ItemTitle { get; set; }

    [Required]
    public string ItemDescription { get; set; }

    [Range( 0, 100 )]
    [Required]
    public int DefaultThreshold { get; set; }

    public string FormId { get; set; }
    
  }
}