using JwtProjectClient.Builders.Concrete;
using JwtProjectClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjectClient.Builders.Abstract
{
    public abstract class StatusBuilder
    {
        public abstract Status GenerateStatus(AppUser activeUser,string roles);
    }
}
