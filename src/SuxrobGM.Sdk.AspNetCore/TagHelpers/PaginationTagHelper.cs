﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SuxrobGM.Sdk.AspNetCore.TagHelpers
{
    /// <summary>
    /// Tag helper to render pagination UI.
    /// Requires client-side Bootstrap 4 files to display correctly.
    /// </summary>
    /// <remarks>
    /// <c>TotalPages</c> - number of pages, default value is 10
    /// <c>PageIndex</c> - number of the current page, default value is 1
    /// <c>PageUrl</c> - page URL, default value is <c>"./Index"</c>
    /// <c>PageHandler</c> - page handler name to pass in query of the URL, default value is <c>"pageIndex"</c>
    /// <c>PageFragment</c> - page fragment name to pass in query of the URL, default value is <c>null</c>
    /// </remarks>
    [HtmlTargetElement("pagination")]
    public class PaginationTagHelper : TagHelper
    {
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }
        public string PageUrl { get; set; }
        public string PageHandler { get; set; }
        public string PageFragment { get; set; }

        public PaginationTagHelper()
        {
            PageHandler = "pageIndex";
            PageUrl = "./Index";
            TotalPages = 10;
            PageIndex = 1;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var prevDisabled = PageIndex > 1 ? "" : "disabled";
            var nextDisabled = PageIndex < TotalPages ? "" : "disabled";

            output.TagName = "pagination";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent("<ul class='pagination pagination-sm text-body mb-0'>");
            output.Content.AppendHtml($"<li class='page-item {prevDisabled}'><a class='page-link' href='{PageUrl}?{PageHandler}={PageIndex - 1}#{PageFragment}'>Previous</a></li>");

            if (TotalPages <= 10)
            {
                for (var i = 1; i <= TotalPages; i++)
                {
                    var activeClassName = i == PageIndex ? "active" : "";
                    output.Content.AppendHtml($"<li class='page-item {activeClassName}'><a class='page-link' href='{PageUrl}?{PageHandler}={i}#{PageFragment}'>{i}</a></li>");
                }
            }
            else
            {
                var activeClassName = PageIndex == 1 ? "active" : "";

                if (PageIndex - 4 > 1)
                {
                    output.Content.AppendHtml($"<li class='page-item {activeClassName}'><a class='page-link' href='{PageUrl}?{PageHandler}=1#{PageFragment}'>1</a></li>");
                    output.Content.AppendHtml("<li class='page-item disabled'><a class='page-link'>...</a></li>");
                }               

                for (var i = PageIndex - 4; i <= PageIndex + 4; i++)
                {
                    if (i > TotalPages)
                        break;

                    if (i <= 0) 
                        continue;
                    activeClassName = i == PageIndex ? "active" : "";
                    output.Content.AppendHtml($"<li class='page-item {activeClassName}'><a class='page-link' href='{PageUrl}?{PageHandler}={i}#{PageFragment}'>{i}</a></li>");
                }

                if (TotalPages - PageIndex > 4)
                {
                    activeClassName = PageIndex == TotalPages ? "active" : "";
                    output.Content.AppendHtml("<li class='page-item disabled'><a class='page-link'>...</a></li>");
                    output.Content.AppendHtml($"<li class='page-item {activeClassName}'><a class='page-link' href='{PageUrl}?{PageHandler}={TotalPages}#{PageFragment}'>{TotalPages}</a></li>");
                }
            }
            
            output.Content.AppendHtml($"<li class='page-item {nextDisabled}'><a class='page-link' href='{PageUrl}?{PageHandler}={PageIndex + 1}#{PageFragment}'>Next</a></li>");
            output.Content.AppendHtml("</ul>");
        }
    }
}
