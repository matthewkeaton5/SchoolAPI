using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class UserParameters : RequestParameters
    {
        public UserParameters()
        {
            Order = "Name";
        }
        public string SearchTerm { get; set; }

    }
}
