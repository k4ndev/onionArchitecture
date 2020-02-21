using AutoMapper;
using Core.Models;
using Core.Services.Data;
using ManageAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ManageAPI.Controllers
{
    [Authorize]
    [Route("api/login")]
    [ApiController]
   
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

       
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
           
            return Ok("ok");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Login([FromBody]UserDto userDto)
        {
            var user = await _userService.IsExistUser(userDto.Email, userDto.Password);

            if (user == null)
                return Unauthorized();

            string token = _userService.Login(user);

            return Ok(token);
        }

        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> Create([FromBody]UserCreateDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserCreateDto, User>(userDto);
                await _userService.Create(user);
                return StatusCode(201);
            }
            return BadRequest(ModelState);
        }
    }
}