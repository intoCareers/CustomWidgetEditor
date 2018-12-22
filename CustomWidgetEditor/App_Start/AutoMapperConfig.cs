using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CustomWidgetEditor.Models;
using CustomWidgetEditor.ViewModels;

namespace CustomWidgetEditor
{
  public class AutoMapperConfig : AutoMapper.Profile
  {
    public AutoMapperConfig()
    {
      CreateMap<PlanLibraryItem, WidgetVm>();
      CreateMap<WidgetVm, PlanLibraryItem>();
    }
  }
}