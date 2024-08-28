using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace IJPMvcApp.Helper
{
    public static class ImageHelper
    {
        public static HtmlString MyImage(this IHtmlHelper hh, string id, string src, string altText,
           object attributes = null)
        {
            TagBuilder tag = new TagBuilder("IMG");
            tag.TagRenderMode = TagRenderMode.SelfClosing;
            tag.GenerateId("Id", id);
            tag.MergeAttribute("SRC", src);
            tag.MergeAttribute("ALT", altText);
            tag.MergeAttributes(new RouteValueDictionary(attributes));
            StringWriter writer = new StringWriter();
            tag.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());

        }
    }
}
