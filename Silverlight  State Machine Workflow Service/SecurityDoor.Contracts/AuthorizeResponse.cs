using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityDoor.Contracts
{
    public class AuthorizeResponse
    {
        public bool Authorized { get; set; }

        public bool Alert { get; set; }
    }
}
