using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;

namespace GarMonitor.Models.ViewModels
{
    public class GarFileListViewModel
    {
        public IEnumerable<GarFile> GarFiles { get; set;} = Enumerable.Empty<GarFile>();
    }
}