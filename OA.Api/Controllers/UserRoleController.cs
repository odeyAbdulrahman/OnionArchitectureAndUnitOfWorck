using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OA.Api;
using OA.Api.Controllers;
using OA.Api.UnitOfWork;
using OA.Base.Helpers;
using OA.Data.Models;
using OA.Dtos.ServiceViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRoleController : BaseController
    {
        public UserRoleController(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings) : base(unitOfWork, appSettings)
        {
        }
       
        [HttpGet("{UserId}")]
        public async Task<ActionResult> GetAsync(string userId)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(userId);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                return Ok(await UnitOfWork.UserRoleService.GetAsync(User));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserMangeRolesViewModel model)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(model.UserId);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                FeedBack Feed = await UnitOfWork.UserRoleService.PostAsync(User, model.Roles.Select(x => x).ToList());
                return Ok(Response(Feed));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        public async Task<ActionResult> DelAsync([FromBody] UserMangeRolesViewModel model)
        {
            try
            {
                AspNetUser User = await UnitOfWork.UserAuthService.FindAsync(model.UserId);
                if (User is null)
                    return Ok(Response(FeedBack.NotFound));
                FeedBack Feed = await UnitOfWork.UserRoleService.DelAsync(User, model.Roles.Select(x => x.ToString()).ToList());
                return Ok(Response(Feed));

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
