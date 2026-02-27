using AutoMapper;
using CollegeApi.Application.Common;
using CollegeApi.Application.DTOs;
using CollegeApi.Application.Interfaces;
using CollegeApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CollegeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _Logger;

        private readonly IMapper _mapper;

        private APIResponse _apiResponse;

        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IMapper mapper, IUserService userService)
        {

            _Logger = logger;
            _mapper = mapper;
            _apiResponse = new();
            _userService = userService;

        }

        [HttpPost]
        [Route("Create")]
        //api/user/create
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateUserAsync(UserDTO dto)
        {
            try
            {
                var userCreated = await _userService.CreateUserAsync(dto);

                _apiResponse.Data = userCreated;
                _apiResponse.Status = true;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                //OK - 200 - Success
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException?.Message;
                _apiResponse.Error = new List<string> { ex.Message, inner };
                _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                _apiResponse.Status = false;
                return StatusCode(500, _apiResponse);
            }
        }

        [HttpGet]
        [Route("All", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[EnableCors(PolicyName = "AllowOnlyMicrosoft")]
        //[AllowAnonymous]
        public async Task<ActionResult<APIResponse>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();

            _apiResponse.Data = users;
            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            //OK - 200 - Success
            return Ok(_apiResponse);

        }

        [HttpGet]
        [Route("{id:int}", Name = "GetUserById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[DisableCors]
        public async Task<ActionResult<APIResponse>> GetUserByIdAsync(int id)
        {


            var user = await _userService.GetUserByIdAsync(id);


            _apiResponse.Data = user;
            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            //OK - 200 - Success
            return Ok(_apiResponse);


        }

        [HttpGet("{username}", Name = "GetUserByUsername")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetUserByUsernameAsync(string username)
        {


            var user = await _userService.GetUserByUsernameAsync(username);


            _apiResponse.Data = user;
            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            //OK - 200 - Success
            return Ok(_apiResponse);


        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> UpdateUserAsync(UserDTO dto)
        {

            var result = await _userService.UpdateUserAsync(dto);

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.Data = result;

            return Ok(_apiResponse);


        }

        [HttpDelete]
        [Route("Delete/{id:int}", Name = "DeleteUserById")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteUserAsync(int id)
        {


            var user = await _userService.DeleteUserAsync(id);

            _apiResponse.Status = true;
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.Data = user;

            return Ok(_apiResponse);


        }


    }
}
