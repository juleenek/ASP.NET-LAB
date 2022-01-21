using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_2.Filter
{
    public class DisableBasicAuthentication : Attribute, IFilterMetadata
    {
    }
}
