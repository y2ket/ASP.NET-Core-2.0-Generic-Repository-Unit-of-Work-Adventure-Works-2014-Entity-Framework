using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using EMS2.ViewModel;

namespace Angular2Core.Helpers
{
    public class PagingPersonEmployeeTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        public PagingPersonEmployeeTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PersonEmployeeViewModel PersonEmployeeModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            if (PersonEmployeeModel.HasPreviousPage)
            {
                int page = 1;
                while (page <= PersonEmployeeModel.PageNumber - 1)
                {
                    TagBuilder prevItem = CreateTag(page, urlHelper);
                    tag.InnerHtml.AppendHtml(prevItem);
                    page++;
                }
            }

            TagBuilder currentItem = CreateTag(PersonEmployeeModel.PageNumber, urlHelper);
            tag.InnerHtml.AppendHtml(currentItem);

            if (PersonEmployeeModel.HasNextPage)
            {
                int page = PersonEmployeeModel.PageNumber + 1;
                while (page <= PersonEmployeeModel.TotalPages)
                {
                    TagBuilder nextItem = CreateTag(page, urlHelper);
                    tag.InnerHtml.AppendHtml(nextItem);
                    page++;
                }
            }
            output.Content.AppendHtml(tag);
        }

        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            TagBuilder item = new TagBuilder("li");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == PersonEmployeeModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, new { page = pageNumber });
            }
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
