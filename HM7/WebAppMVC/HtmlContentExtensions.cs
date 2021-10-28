using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppMVC.Models;

namespace WebAppMVC
{
    public static class HtmlContentExtensions
    {
        public static IHtmlContent MyEditorForModel(this IModelForEditorForm model) =>
            new FormContent(model);

        private class FormContent : IHtmlContent
        {
            private string _resultContent;
            public FormContent(IModelForEditorForm model)
            {
                var rb = new StringBuilder();
                rb.Append("<form method=\"POST\">");
                
                
                
                rb.Append("</form>");
            }

            public void WriteTo(TextWriter writer, HtmlEncoder encoder) =>
                writer.WriteLine(_resultContent);
        }
    }
}
