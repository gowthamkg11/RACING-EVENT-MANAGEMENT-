﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace NUS.TheAmagingRace.DAL
{



    public class TARUser : IdentityUser, IAuditedEntity
    {

        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string DisplayName { get; set; }
        public string ImagePath { get; set; }
        public bool isAssigned { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<TARUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            userIdentity.AddClaim(new Claim("DisplayName", DisplayName));
            userIdentity.AddClaim(new Claim("ImagePath", ImagePath));
            userIdentity.AddClaim(new Claim("Assigned", isAssigned.ToString()));


    //        userIdentity.AddClaim(
    //new Claim(TARUser.isAssigned,
    //isAssigned.GetValueOrDefault(false).ToString()));
           return userIdentity;
        }
    }
        

    }

    

