#pragma checksum "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Customer\CustomersAddPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b76d5d5972d008ecca2086640221b6f9085132b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Customer_CustomersAddPartial), @"mvc.1.0.view", @"/Views/Customer/CustomersAddPartial.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b76d5d5972d008ecca2086640221b6f9085132b", @"/Views/Customer/CustomersAddPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c24294de6a7094e278bf7275c9e4098c19c5a3d8", @"/Views/_ViewImports.cshtml")]
    public class Views_Customer_CustomersAddPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""modal fade"" id=""confirm-add"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myModalLabel"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h5 class=""modal-title"">Edit</h5>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close"">
                    <span aria-hidden=""true"">&times;</span>
                </button>
            </div>
");
#nullable restore
#line 10 "C:\Users\Mikhail\source\repos\restaurant-system\restaurant-system\Views\Customer\CustomersAddPartial.cshtml"
              await Html.RenderPartialAsync("CustomersFieldsPartical", new { Prefix = "add" });

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-default"" data-dismiss=""modal"">Close</button>
                <input type=""submit"" onclick=""add()"" id=""modal-delete-button"" value=""Create"" class=""btn btn-primary"" />
            </div>
        </div>
    </div>
</div>

<script>

    function add() {
        $.ajax({
            type: ""POST"",
            url: ""AddCustomer"",
            data: {
                id: EditId,
                firstname: $(""#add-input-firstname"").val(),
                lastname: $(""#add-input-lastname"").val(),
                phone: $(""#add-input-phone"").val()
            },
            success: function (data) {
                document.location.reload();
            }
        })
    }

</script>
");
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
