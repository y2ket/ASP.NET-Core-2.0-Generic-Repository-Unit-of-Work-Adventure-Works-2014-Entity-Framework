using EMS2.Models;
using EMS2.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace EMS2.TagHelpers
{
    public class PagingJobTitleTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;
        protected PagingJobTitleTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public JobTitlesViewModel JobTitleModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            TagBuilder currentItem = CreateTag(JobTitleModel.PageNumber, urlHelper);

            if (JobTitleModel.HasPreviousPage)
            {
                int page = 1;
                while (page <= JobTitleModel.PageNumber - 1)
                {
                    TagBuilder prevItem = CreateTag(page, urlHelper);
                    tag.InnerHtml.AppendHtml(prevItem);
                    page++;
                }
            }

            tag.InnerHtml.AppendHtml(currentItem);

            if (JobTitleModel.HasNextPage)
            {
                int page = JobTitleModel.PageNumber + 1;
                while (page <= JobTitleModel.TotalPages)
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
            if (pageNumber == JobTitleModel.PageNumber)
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
