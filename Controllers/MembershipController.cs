// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.
using _2_1_Call_MSGraph.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace _2_1_Call_MSGraph.Controllers
{
    [AuthorizeForScopes(Scopes = new[] { "GroupMember.ReadWrite.All" })]
    public class MembershipController : BaseController
    {

        private readonly string[] groupScopes =
            new[] { "GroupMember.ReadWrite.All" };

        public MembershipController(
            ITokenAcquisition tokenAcquisition,
            ILogger<HomeController> logger) : base(tokenAcquisition, logger)
        {
        }

        ////https://docs.microsoft.com/en-us/graph/api/group-post-members
        public async Task<IActionResult> Add()
        {
            var graphClient = GetGraphClientForScopes(groupScopes);

            string john = "user guid";
            string group1 = "groupid";

            var dirobj = new DirectoryObject
            {
                Id = john
            };

            // elevated permissions needed https://docs.microsoft.com/en-us/graph/auth-v2-service
            await graphClient.Groups[group1].Members.References.Request().AddAsync(dirobj);

            return RedirectToAction("ManageGroups", "Home");

        }

        ////https://docs.microsoft.com/en-us/graph/api/group-delete-members
        public async Task<IActionResult> Delete()
        {
            var graphClient = GetGraphClientForScopes(groupScopes);

            string john = "user guid";
            string group1 = "groupid";

            // elevated permissions needed https://docs.microsoft.com/en-us/graph/auth-v2-service
            await graphClient.Groups[group1].Members[john].Reference.Request().DeleteAsync();

            return RedirectToAction("ManageGroups", "Home");

        }
    }
}
