using System;
using System.Web.Mvc;
using Worker.Web.ViewModel;

namespace Worker.Web.Helpers
{
    /// <summary>
    /// The paging helpers.
    /// </summary>
    public static class PagingHelpers
    {
        /// <summary>
        /// The page links.
        /// </summary>
        /// <param name="html">The html helper.</param>
        /// <param name="pageInfo">The page info.</param>
        /// <returns>The <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfo pageInfo)
        {
            var totalPages = (int)Math.Ceiling((decimal)pageInfo.TotalItems.Value / pageInfo.PageSize.Value);
            
            var left = GetLeft(pageInfo, totalPages);
            var right = GetRight(pageInfo, totalPages);

            if (totalPages == 1)
                return null;

            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination page");

            if (pageInfo.CurrentPage > 1)
            {
                var leftPoint = GetTagLi(1, "<<");
                ul.InnerHtml += leftPoint;
            }

            for (int i = left; i <= right; i++)
            {
                var li = GetTagLi(i, i.ToString());

                if (i == pageInfo.CurrentPage)
                    li.AddCssClass("active");

                ul.InnerHtml += li;
            }

            if (pageInfo.CurrentPage < totalPages)
            {
                var rightPoint = GetTagLi(totalPages, ">>");
                ul.InnerHtml += rightPoint;
            }

            return new MvcHtmlString(ul.ToString());
        }

        /// <summary>
        /// The page sizes.
        /// </summary>
        /// <param name="html">The html helper.</param>
        /// <param name="currentPageSize">The current page size.</param>
        /// <returns>The <see cref="MvcHtmlString"/></returns>
        public static MvcHtmlString PageSizes(this HtmlHelper html, int currentPageSize)
        {
            var ul = new TagBuilder("ul");
            ul.AddCssClass("pagination pagination-sm page-size");

            foreach (var pageSize in WebsiteConfig.PageSizes)
            {
                var li = GetTagLi(pageSize, pageSize.ToString());

                if (pageSize == currentPageSize)
                    li.AddCssClass("active");

                ul.InnerHtml += li;
            }

            return new MvcHtmlString(ul.ToString());
        }

        /// <summary>
        /// Create tag li for paging.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="text">The text.</param>
        /// <returns>The <see cref="MvcHtmlString"/></returns>
        private static TagBuilder GetTagLi(int value, string text)
        {
            var li = new TagBuilder("li");
            var a = new TagBuilder("a");

            a.MergeAttribute("data-value", value.ToString());
            a.InnerHtml = text;

            li.InnerHtml += a;

            return li;
        }

        /// <summary>
        /// Get right element in paging.
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="totalPages"></param>
        /// <returns></returns>
        private static int GetRight(PageInfo pageInfo, int totalPages)
        {
            var pageNumber = WebsiteConfig.PageNumber;
            var shift = (int)Math.Floor((double)pageNumber / 2);

            if (totalPages < pageNumber)
                return totalPages;

            var result = pageInfo.CurrentPage + shift < totalPages ? pageInfo.CurrentPage + shift : totalPages;

            if (pageInfo.CurrentPage - shift < 1)
                result = result + shift - (pageInfo.CurrentPage.Value - 1);

            return result.Value;
        }

        /// <summary>
        /// Get left element in paging.
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <param name="totalPages"></param>
        /// <returns></returns>
        private static int GetLeft(PageInfo pageInfo, int totalPages)
        {
            var pageNumber = WebsiteConfig.PageNumber;
            var shift = (int)Math.Floor((double)pageNumber / 2);

            if (totalPages < pageNumber)
                return 1;

            var result = pageInfo.CurrentPage - shift > 1 ? pageInfo.CurrentPage - shift : 1;

            if (pageInfo.CurrentPage + shift > totalPages)
                result = result - shift + (totalPages - pageInfo.CurrentPage.Value);

            return result.Value;
        }
    }
}