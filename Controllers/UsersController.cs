using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Helpers;
using Commander.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace   Commander.Controllers
{




    [ApiController]
    public class UsersController : Controller
    {




        private readonly CommanderContext _context;
        private readonly IMapper _mapper;
        IHttpContextAccessor _httpContextAccessor;


        public UsersController(IMapper mapper, CommanderContext context, IHttpContextAccessor httpContextAccessor

             )
        {
            this._context = context;
            this._mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }









        [Authorize(Roles = "user,driver")]
        [HttpPost("user/update")]
        public async Task<ActionResult> UpdateUserAsync([FromForm] UserForUpdate userForUpdate)
        {
            User user = await Functions.getCurrentUser(_httpContextAccessor, _context);
            if (userForUpdate.FullName != null)
            {
                user.FullName = userForUpdate.FullName;
                await _context.SaveChangesAsync();
            }
            if (userForUpdate.Email != null)
            {
                user.Email = userForUpdate.Email;
                await _context.SaveChangesAsync();
            }
            if (userForUpdate.Phone != null)
            {
                user.PhoneNumber = userForUpdate.Phone;
                await _context.SaveChangesAsync();
            }

            if (userForUpdate.ImageUrl != null)
            {
                // var orders = await _context.Orders.Where(x => x.UserImage == user.ImageUrl).OrderByDescending(x => x.Id).ToListAsync();
                // orders.ForEach((order) =>
                // {
                //     order.UserImage = userForUpdate.ImageUrl;
                // });

                user.ImageUrl = userForUpdate.ImageUrl;
                await _context.SaveChangesAsync();
            }
            if (userForUpdate.DeviceToken != null)
            {
                user.DeviceToken = userForUpdate.DeviceToken;
                await _context.SaveChangesAsync();
            }
            return Ok(userForUpdate);
        }



        [Authorize(Roles = "user,driver")]
        [HttpPost("user/detail")]
        public async Task<ActionResult> Details()
        {

            User user = await Functions.getCurrentUser(_httpContextAccessor, _context);
            // int Comments = await _context.Comments.Where(x => x.Text == user.Id && x.Text != null).CountAsync();

            // int Orders = await _context.Orders.Where(x => x.userId == user.Id).CountAsync();

            // int fav = await _context.Favorites.Where(x => x.UserId == user.Id).CountAsync();
            // int cart = await _context.Carts.Where(x => x.UserId == user.Id).CountAsync();

            // var driver = await _context.Drivers.Where(x => x.UserId == user.Id).AsNoTracking().FirstOrDefaultAsync();
            return Ok("djd");

        }
    }
}