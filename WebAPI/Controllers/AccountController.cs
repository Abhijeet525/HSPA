using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repo.Interface;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await unitOfWork.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);

            if(user == null)
            {
                return Unauthorized();
            }

            var loginRes = new LoginResDto();
            loginRes.UserName = user.UserName;
            loginRes.Token = "Token to be granted";

            return Ok(loginRes);
        }
    }
}
