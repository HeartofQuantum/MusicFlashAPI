using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MusicFlashApi.Validators
{
    public class MyCustomPasswordValidator : PasswordValidator
    {
        public override async Task<IdentityResult> ValidateAsync(string password)
        {
            IdentityResult result = await base.ValidateAsync(password);

            if (password.Contains("abcdefgh") || password.Contains("12345678"))
            {
                var errors = result.Errors.ToList();

                errors.Add("Passwords cannot contain a sequence of characters");
                result = new IdentityResult();
            }
            return result;
        }

    }
}