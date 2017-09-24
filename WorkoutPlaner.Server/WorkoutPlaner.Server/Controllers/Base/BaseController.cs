using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlaner.Server.Data;
using Microsoft.AspNetCore.Identity;
using WorkoutPlaner.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WorkoutPlaner.Server.Controllers.Base
{
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected ApplicationUser _user;
        protected SignInManager<ApplicationUser> _signInManager;
        protected readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseController(UserManager<ApplicationUser> userManager,
            IHttpContextAccessor accessor,
            ApplicationDbContext cotnext,
            SignInManager<ApplicationUser> singinManager)
        {
            _userManager = userManager;
            _httpContextAccessor = accessor;
            _context = cotnext;
            _signInManager = singinManager;
            _httpContextAccessor.HttpContext.User.Identities.Any(x => x.IsAuthenticated);
            if (_signInManager.IsSignedIn(_httpContextAccessor.HttpContext.User))
                _user = _context.Users
                    .Include(u => u.DoneWorkouts)
                    .Include(u => u.Workouts)
                    .SingleOrDefault(u => u.UserName
                    .Equals(_httpContextAccessor.HttpContext.User.Identity.Name));
        }
    }
}