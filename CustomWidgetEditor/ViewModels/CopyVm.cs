using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomWidgetEditor.ViewModels
{
  public class CopyVm
  {
    [Required]
    public int PlanLibCode { get; set; }

    [Required]
    public string StateAbbr { get; set; }

    [DisplayName("Current State")]
    public string CurrentState { get; set; }

    public string OldStateAbbr { get; set; }
  }
}