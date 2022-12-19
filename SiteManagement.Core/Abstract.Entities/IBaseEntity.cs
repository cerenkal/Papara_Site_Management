
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Core.Abstract.Entities
{
    public interface IBaseEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        DateTime DeletedDate { get; set; }
      
    }
}
