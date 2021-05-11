using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class OrganizationParameters : RequestParameters
    {
        public OrganizationParameters()
        {
            Order = "Name";
        }
        public string SearchTerm { get; set; }
        public string CityFilter { get; set; }
    }
}
