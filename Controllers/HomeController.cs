using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Graph;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _2_1_Call_MSGraph.Models;
using System.IO;
using System.Collections.Generic;

namespace _2_1_Call_MSGraph.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly GraphServiceClient _graphServiceClient;

        public HomeController(ILogger<HomeController> logger,
                          GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            _graphServiceClient = graphServiceClient;
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> Index()
        {
            var user = await _graphServiceClient.Me.Request().GetAsync();
            ViewData["ApiResult"] = user.DisplayName;

            var groups = await _graphServiceClient.Me.MemberOf.Request().GetAsync();
            ViewData["groups"] = groups;

            var applications = await _graphServiceClient.Me.AppRoleAssignments.Request().GetAsync();
            ViewData["applications"] = applications;

            return View();
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> Profile()
        {
            var me = await _graphServiceClient.Me.Request().GetAsync();
            ViewData["Me"] = me;

            try
            {
                // Get user photo
                using (var photoStream = await _graphServiceClient.Me.Photo.Content.Request().GetAsync())
                {
                    byte[] photoByte = ((MemoryStream)photoStream).ToArray();
                    ViewData["Photo"] = Convert.ToBase64String(photoByte);
                }
            }
            catch (System.Exception)
            {
                ViewData["Photo"] = null;
            }

            return View();
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> MyGroups()
        {
            var mygroups = await _graphServiceClient.Me.MemberOf.Request().GetAsync();

            ViewData["mygroups"] = mygroups;

            return View();
        }
        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> MyUsers()
        {
            var myusers = await _graphServiceClient.Groups[groupid].Members.Request().GetAsync();

            ViewData["myusers"] = myusers;

            return View();
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> MyApps()
        {
            var myapps = await _graphServiceClient.Me.AppRoleAssignments.Request().GetAsync();
            ViewData["myapps"] = myapps;

            return View();
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> ManageGroups()
        {
            if (await IsAuthorized("ManageGroups"))
            {
                var transitivegroups = await _graphServiceClient.Me.TransitiveMemberOf.Request().GetAsync();

                HomeHelper helper = new HomeHelper();
                SortedDictionary<string, string> groups = new SortedDictionary<string, string>();
                ViewData["transitivegroups"] = helper.TransitiveGroupsToDictionary(groups, transitivegroups);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [AuthorizeForScopes(ScopeKeySection = "DownstreamApi:Scopes")]
        public async Task<IActionResult> ManageUsers()
        {
            if (await IsAuthorized("ManageUsers"))
            {
                var transitivegroups = await _graphServiceClient.Me.TransitiveMemberOf.Request().GetAsync();

                HomeHelper helper = new HomeHelper();
                SortedDictionary<string, string> groups = new SortedDictionary<string, string>();
                groups = helper.TransitiveGroupsToDictionary(groups, transitivegroups);

                SortedDictionary<string, string> members = await GetGroupMembers(groups);
                ViewData["members"] = members;
            }
            else {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        private async Task<SortedDictionary<string, string>> GetGroupMembers(SortedDictionary<string, string> groups)
        {
            //https://docs.microsoft.com/en-us/graph/api/group-list-members
            SortedDictionary<string, string> users = new SortedDictionary<string, string>();
            HomeHelper helper = new HomeHelper();
            foreach (string s in groups.Values)
            {
                var members = await _graphServiceClient.Groups[s].Members
                    .Request()
                    .GetAsync();

                users = helper.UsersToDictionary(users, members);
            }

            return users;
        }

        private async Task<bool> IsAuthorized(string action)
        {
            bool result = false;
            var mygroups = await _graphServiceClient.Me.MemberOf.Request().GetAsync();

            HomeHelper helper = new HomeHelper();
            result = helper.IsAdmin(mygroups);

            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
