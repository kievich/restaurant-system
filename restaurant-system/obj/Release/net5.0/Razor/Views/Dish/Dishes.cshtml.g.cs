#pragma checksum "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19a713fe0443bccb90018db248c43804402a5982"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dish_Dishes), @"mvc.1.0.view", @"/Views/Dish/Dishes.cshtml")]
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
#line 1 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\_ViewImports.cshtml"
using restaurant_system;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\_ViewImports.cshtml"
using restaurant_system.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19a713fe0443bccb90018db248c43804402a5982", @"/Views/Dish/Dishes.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c24294de6a7094e278bf7275c9e4098c19c5a3d8", @"/Views/_ViewImports.cshtml")]
    public class Views_Dish_Dishes : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<restaurant_system.Models.Dish>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
  
    ViewData["Title"] = "Home Page1";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>\r\n    Dishes\r\n</h1>\r\n<br>\r\n\r\n<button type=\"button\" class=\"btn btn-primary\" data-toggle=\"modal\" data-target=\"#confirm-add\">Create</button>\r\n<br>\r\n<br>\r\n");
#nullable restore
#line 13 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
  await Html.RenderPartialAsync("SearchPartial", new { SearchString = ViewBag.searchString });

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<br>
<table class=""table table-hover"">

    <thead>
        <tr>
            <th scope=""col"">№</th>
            <th scope=""col"">Name</th>
            <th scope=""col"">Description</th>
            <th scope=""col"">Price</th>
            <th scope=""col"">Archived</th>
            <th scope=""col""></th>
        </tr>
    </thead>
");
#nullable restore
#line 27 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tbody>\r\n            <tr");
            BeginWriteAttribute("id", " id=\"", 760, "\"", 777, 2);
            WriteAttributeValue("", 765, "row-", 765, 4, true);
#nullable restore
#line 30 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
WriteAttributeValue("", 769, item.Id, 769, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                <td>");
#nullable restore
#line 31 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
               Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 32 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
               Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 33 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
               Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 34 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
               Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 35 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
               Write(item.Archived);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1025, "\"", 1054, 3);
            WriteAttributeValue("", 1035, "SetEditId(", 1035, 10, true);
#nullable restore
#line 37 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
WriteAttributeValue("", 1045, item.Id, 1045, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1053, ")", 1053, 1, true);
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" class=\"btn btn-outline-primary\" data-toggle=\"modal\" data-target=\"#confirm-edit\">Edit</button>\r\n                    <button");
            BeginWriteAttribute("onclick", " onclick=\"", 1192, "\"", 1223, 3);
            WriteAttributeValue("", 1202, "SetDeleteId(", 1202, 12, true);
#nullable restore
#line 38 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
WriteAttributeValue("", 1214, item.Id, 1214, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1222, ")", 1222, 1, true);
            EndWriteAttribute();
            WriteLiteral(" type=\"button\" class=\"btn btn-outline-primary\" data-toggle=\"modal\" data-target=\"#confirm-delete\">Delete</button>\r\n                </td>\r\n\r\n            </tr>\r\n        </tbody>\r\n");
#nullable restore
#line 43 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>\r\n\r\n\r\n\r\n");
#nullable restore
#line 48 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
  await Html.RenderPartialAsync("DishesAddPartial");

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
  await Html.RenderPartialAsync("DeleteConfirmPartial", new { Action = "DeleteDish" }); 

#line default
#line hidden
#nullable disable
#nullable restore
#line 50 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
  await Html.RenderPartialAsync("DishesEditPartial");

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Dish\Dishes.cshtml"
   await Html.RenderPartialAsync("PaginationPartial", new { PageCount = ViewBag.PageCount, CurrentPage = ViewBag.CurrentPage, SearchString = ViewBag.searchString }); 

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<restaurant_system.Models.Dish>> Html { get; private set; }
    }
}
#pragma warning restore 1591