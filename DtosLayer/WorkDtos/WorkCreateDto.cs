using DtosLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtosLayer.WorkDtos
{
    public class WorkCreateDto:IDto
    {
        //eklerken ıd hariç diğerleri
        
        public string Definition { get; set; }
        public bool IsCompleted { get; set; }
    }
}
