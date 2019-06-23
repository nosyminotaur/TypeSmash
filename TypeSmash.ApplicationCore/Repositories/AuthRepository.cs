using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TypeSmash.ApplicationCore.Entities;
using TypeSmash.ApplicationCore.Interfaces;
using TypeSmash.Infrastructure;

namespace TypeSmash.ApplicationCore.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _manager;
        public AuthRepository(UserManager<AppUser> manager)
        {
            _manager = manager;
        }

        /* if no such email exists, return error email not found.
        if email exists and password is correct, return user.
        if email exists and password is incorrect, return error password incorrect.
        */
        public async Task<UserOut> EmailLogin(string email, string password)
        {
            var result = await _manager.FindByEmailAsync(email);
            if (result == null)
            {
                return new UserOut
                {
                    success = false,
                    errors = new List<string>() {"Email not found"}
                };
            }

            bool isPasswordCorrect = await _manager.CheckPasswordAsync(result, password);
            if (isPasswordCorrect)
            {
                return new UserOut(result.UserName, result.Email, true);
            }
            return new UserOut
            {
                success = false,
                errors = new List<string>() {"Incorrect password"}
            };
        }

        /* similar to emaillogin, except first call of getting user from username instead of email
         */
        public async Task<UserOut> UsernameLogin(string username, string password)
        {
            var result = await _manager.FindByNameAsync(username);

            if (result == null)
            {
                return new UserOut() 
                {
                    success = false,
                    errors = new List<string>() {"Email not found"}
                };
            }

            bool isPasswordCorrect = await _manager.CheckPasswordAsync(result, password);
            if (isPasswordCorrect)
            {
                return new UserOut(result.UserName, result.Email, true);
            }
            return new UserOut() 
            {
                success = false,
                errors = new List<string>() {"Incorrect password"}
            };
        }

        public async Task<UserOut> Register(string email, string username, string password)
        {
            var result = await _manager.FindByNameAsync(username);
            
            //If username exists already, return error
            if (result != null)
            {
                return new UserOut { success = false, errors = new List<string> { "Username exists" } };
            }

            //If email exists already, return error
            result = await _manager.FindByEmailAsync(email);
            if (result != null)
            {
                return new UserOut { success = false, errors = new List<string> { "Email exists" } };
            }

            AppUser user = new AppUser
            {
                UserName = username,
                Email = email,
                Id = System.Guid.NewGuid().ToString()
            };

            var createResult = await _manager.CreateAsync(user, password);
            
            if (createResult != IdentityResult.Success)
            {
                var errors = createResult.Errors.Select(ier => ier.Description);
                return new UserOut { success = false, errors = errors.ToList() };
            }

            return new UserOut(username, email, true);
        }
    }
}