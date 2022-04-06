using Intex.Models;
using Intex.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-blah")]
    public class PaginationTagHelper : TagHelper
    {
        //Dynamically create the page links

        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        //Different than View Context
        public PageInfo PageBlah { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        //public override void Process(TagHelperContext thc, TagHelperOutput tho)
        //{
        //    IUrlHelper uh = uhf.GetUrlHelper(vc);

        //    TagBuilder final = new TagBuilder("div");

        //    for (int i = 1; i <= PageBlah.TotalPages; i++)
        //    {
        //        TagBuilder tb = new TagBuilder("a");

        //        tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
        //        if (PageClassesEnabled)
        //        {
        //            tb.AddCssClass(PageClass);
        //            tb.AddCssClass(i == PageBlah.CurrentPage
        //                ? PageClassSelected : PageClassNormal);
        //        }
        //        tb.InnerHtml.Append(i.ToString());
        //        final.InnerHtml.AppendHtml(tb);
        //    }

        //    tho.Content.AppendHtml(final.InnerHtml);
        //}

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {

            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            

            if (PageBlah.TotalPages > 10)
            {
                if (PageBlah.CurrentPage < PageBlah.TotalPages - 6) // Pagination if the page nubmer is not at the end
                {
                   for (int i = PageBlah.CurrentPage - 3; i <= PageBlah.CurrentPage + 3; i++)
                    {
                        if (i > 0)
                        {
                            TagBuilder tb = new TagBuilder("a");

                            tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i, city = PageBlah.City, county = PageBlah.County, severity = PageBlah.Severity, date = PageBlah.Date });
                            if (PageClassesEnabled)
                            {
                                tb.AddCssClass(PageClass);
                                tb.AddCssClass(i == PageBlah.CurrentPage
                                    ? PageClassSelected : PageClassNormal);
                            }
                            tb.InnerHtml.Append(i.ToString());
                            final.InnerHtml.AppendHtml(tb);
                        }
                    }

                    TagBuilder middle = new TagBuilder("a");
                    middle.InnerHtml.Append("________________");
                    final.InnerHtml.AppendHtml(middle);

                    for (int i = PageBlah.TotalPages - 3; i <= PageBlah.TotalPages; i++)
                    {
                        TagBuilder tb = new TagBuilder("a");

                        tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i, city = PageBlah.City, county = PageBlah.County, severity = PageBlah.Severity, date = PageBlah.Date });
                        if (PageClassesEnabled)
                        {
                            tb.AddCssClass(PageClass);
                            tb.AddCssClass(i == PageBlah.CurrentPage
                                ? PageClassSelected : PageClassNormal);
                        }
                        tb.InnerHtml.Append(i.ToString());
                        final.InnerHtml.AppendHtml(tb);
                    }
                }
                else //Pagination if current page is towards end of total page range
                {
                    for (int i = 1; i < 4; i++)
                    {
                        TagBuilder tb = new TagBuilder("a");

                        tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i, city = PageBlah.City, county = PageBlah.County, severity = PageBlah.Severity, date = PageBlah.Date });
                        if (PageClassesEnabled)
                        {
                            tb.AddCssClass(PageClass);
                            tb.AddCssClass(i == PageBlah.CurrentPage
                                ? PageClassSelected : PageClassNormal);
                        }
                        tb.InnerHtml.Append(i.ToString());
                        final.InnerHtml.AppendHtml(tb);
                    }

                    TagBuilder middle = new TagBuilder("a");
                    middle.InnerHtml.Append("________________");
                    final.InnerHtml.AppendHtml(middle);

                    for (int i = PageBlah.CurrentPage - 3; i <= PageBlah.TotalPages; i++)
                    {
                        TagBuilder tb = new TagBuilder("a");

                        tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i, city = PageBlah.City, county = PageBlah.County, severity = PageBlah.Severity, date = PageBlah.Date });
                        if (PageClassesEnabled)
                        {
                            tb.AddCssClass(PageClass);
                            tb.AddCssClass(i == PageBlah.CurrentPage
                                ? PageClassSelected : PageClassNormal);
                        }
                        tb.InnerHtml.Append(i.ToString());
                        final.InnerHtml.AppendHtml(tb);
                    }
                }
            }
            else // If less than 10 total pages, display all page buttons
            {
                for (int i = 1; i <= PageBlah.TotalPages; i++)
                {
                    TagBuilder tb = new TagBuilder("a");

                    tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i , city = PageBlah.City, county = PageBlah.County, severity = PageBlah.Severity, date = PageBlah.Date });
                    if (PageClassesEnabled)
                    {
                        tb.AddCssClass(PageClass);
                        tb.AddCssClass(i == PageBlah.CurrentPage
                            ? PageClassSelected : PageClassNormal);
                    }
                    tb.InnerHtml.Append(i.ToString());
                    final.InnerHtml.AppendHtml(tb);
                }

                tho.Content.AppendHtml(final.InnerHtml);
            }




            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}
