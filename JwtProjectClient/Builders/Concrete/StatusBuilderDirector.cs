﻿using JwtProjectClient.Builders.Abstract;
using JwtProjectClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtProjectClient.Builders.Concrete
{
    public class StatusBuilderDirector
    {
        private StatusBuilder builder;
        public StatusBuilderDirector(StatusBuilder builder)
        {
            this.builder = builder;
        }
        public Status GenerateStatus(AppUser activeUser,string roles)
        {
            return builder.GenerateStatus(activeUser, roles);

        }
    }
}
