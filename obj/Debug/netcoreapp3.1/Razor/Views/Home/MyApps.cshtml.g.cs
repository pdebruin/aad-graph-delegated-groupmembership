#pragma checksum "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "30c236b480f8deb0a3f9a3e7cea0b11194baf31b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_MyApps), @"mvc.1.0.view", @"/Views/Home/MyApps.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\_projects\aad-graph-delegated-groupmembership\Views\_ViewImports.cshtml"
using _2_1_Call_MSGraph;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\_projects\aad-graph-delegated-groupmembership\Views\_ViewImports.cshtml"
using _2_1_Call_MSGraph.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
using Newtonsoft.Json.Linq;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"30c236b480f8deb0a3f9a3e7cea0b11194baf31b", @"/Views/Home/MyApps.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c8f405b3760fadc064a50fd999fd9e2ef9558e1", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_MyApps : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
  
    ViewData["Title"] = "My apps";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2>");
#nullable restore
#line 5 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n<h3>");
#nullable restore
#line 6 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n\r\n<table class=\"table table-striped table-condensed\" style=\"font-family: monospace\">\r\n    <tr>\r\n        <th>Property</th>\r\n        <th>Value</th>\r\n    </tr>\r\n");
#nullable restore
#line 13 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
             
//        var apps = (IEnumerable<Microsoft.Graph.UserAppRoleAssignmentsCollectionPage>)ViewData["applications"];
        var myapps = (Microsoft.Graph.UserAppRoleAssignmentsCollectionPage)ViewData["myapps"];
        foreach (var app in myapps)
        {
            var properties = app.GetType().GetProperties();
            foreach (var child in properties)
            {
                object value = child.GetValue(app);
                string stringRepresentation;
                if (!(value is string) && value is IEnumerable<string>)
                {
                    stringRepresentation = "["
                        + string.Join(", ", (value as IEnumerable<string>).OfType<object>().Select(c => c.ToString()))
                        + "]";
                }
                else
                {
                    stringRepresentation = value?.ToString();
                }


#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td> ");
#nullable restore
#line 35 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
                Write(child.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n                <td> ");
#nullable restore
#line 36 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
                Write(stringRepresentation);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </td>\r\n            </tr>\r\n");
#nullable restore
#line 38 "C:\_projects\aad-graph-delegated-groupmembership\Views\Home\MyApps.cshtml"
                }
            }
        

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591