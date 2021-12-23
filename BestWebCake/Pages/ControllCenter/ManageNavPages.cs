using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BestWebCake.Pages.ControllCenter
{
    public static class ManageNavPages
    {
        public static string Index => "Index";

        public static string Cake => "Cake";

        public static string CreateCake => "CreateCake";

        public static string EditCake => "EditCake";

        public static string Crude => "Crude";

        public static string CreateCrude => "CreateCrude";

        public static string EditCrude => "EditCrude";

        public static string CakeCrude => "CakeCrude";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);
        public static string CakeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Cake);
        public static string CreateCakeNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateCake);
        public static string EditCakeNavClass(ViewContext viewContext) => PageNavClass(viewContext, EditCake);
        public static string CrudeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Crude);
        public static string CreateCrudeNavClass(ViewContext viewContext) => PageNavClass(viewContext, CreateCrude);
        public static string EditCrudeNavClass(ViewContext viewContext) => PageNavClass(viewContext, EditCrude);
        public static string CakeCrudeNavClass(ViewContext viewContext) => PageNavClass(viewContext, CakeCrude);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
