﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Domain.Socioboard.Interfaces.Services;
using Microsoft.AspNetCore.Hosting;
using Api.Socioboard.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Socioboard.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class YoutubeGroupController : Controller
    {
        public YoutubeGroupController(ILogger<UserController> logger, IEmailSender emailSender, IHostingEnvironment appEnv, Microsoft.Extensions.Options.IOptions<Helper.AppSettings> settings)
        {
            _logger = logger;
            _emailSender = emailSender;
            _appEnv = appEnv;
            _appSettings = settings.Value;
            _redisCache = new Helper.Cache(_appSettings.RedisConfiguration);

        }
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _appEnv;
        private Helper.AppSettings _appSettings;
        private Helper.Cache _redisCache;


        [HttpPost("InviteGroupMember")]
        public IActionResult InviteGroupMember(Int64 userId, string emailId)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger, _appEnv);
            Repositories.YoutubeGroupRepository.InviteGroupMember(userId, emailId, _appSettings, _logger, dbr);
            return Ok("");
        }

        [HttpGet("GetGroupMember")]
        public ActionResult GetGroupMember(long userId)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger, _appEnv);
            //IList<Domain.Socioboard.Models.YoutubeGroupInvite> grppMembers = Repositories.YoutubeGroupRepository.GetGroupMembers(channelId, _appSettings, _logger, dbr);
            IList<Domain.Socioboard.Models.YoutubeGroupInvite> grppMembers = Repositories.YoutubeGroupRepository.GetGroupMembers(userId, _appSettings, _logger, dbr);
            return Ok(grppMembers);
        }

        [HttpGet("GetYtGroupChannel")]
        public ActionResult GetYtGroupChannel(Int64 userId)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger, _appEnv);
            IList<Domain.Socioboard.Models.Groupprofiles> grppMembers = Repositories.YoutubeGroupRepository.GetYtGroupChannel(userId, _appSettings, _logger, dbr);
            return Ok(grppMembers);
        }

        [HttpGet("GetYtYourGroups")]
        public ActionResult GetYtYourGroups(Int64 userId)
        {
            DatabaseRepository dbr = new DatabaseRepository(_logger, _appEnv);
            IList<Domain.Socioboard.Models.YoutubeGroupInvite> grppMembers = Repositories.YoutubeGroupRepository.GetYtYourGroups(userId, _appSettings, _logger, dbr);
            return Ok(grppMembers);
        }


    }
}
