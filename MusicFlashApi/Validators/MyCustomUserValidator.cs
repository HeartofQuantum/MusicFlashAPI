using Microsoft.AspNet.Identity;
using MusicFlashApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MusicFlashApi.Validators
{
    public class MyCustomUserValidator : UserValidator<ApplicationUser>
    {

        // only allows certain domain names
        List<string> _allowedEmailDomains = new List<string> { "hotmail.com", "outlook.com", "gmail.com", "yahoo.com" };

        public MyCustomUserValidator(ApplicationUserManager appUserManager) : base(appUserManager)
        {

        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);

            var emailDomain = user.Email.Split('@')[1];

            if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))
            {
                var errors = result.Errors.ToList();

                errors.Add(String.Format("Email domain '{0}' is not allowed", emailDomain));

                result = new IdentityResult(errors);
            }

            return result;
        }

    }
}