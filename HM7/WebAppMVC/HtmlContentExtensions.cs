using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using WebAppMVC.Models;

namespace WebAppMVC
{
    public static class HtmlContentExtensions
    {
        public static IHtmlContent MyEditorForModel(this IModelForEditorForm model) =>
            new FormContent(model);

        private class FormContent : IHtmlContent
        {
            private Task<string> _resultContentTask;

            public FormContent(IModelForEditorForm model) =>
                _resultContentTask = Task.Run(() => CreateContent(model.GetType().GetProperties()));

            private static string CreateContent(IEnumerable<PropertyInfo> fieldInfos)
            {
                var rb = new StringBuilder();
                
                foreach (var fieldInfo in fieldInfos)
                {
                    rb.Append("<div class=\"editor-field\">");

                    var type = fieldInfo.PropertyType;
                    var name = fieldInfo.Name;

                    if (type.IsAssignableTo(typeof(Enum)))
                    {
                        rb.Append("<select class=\"form-group\">");
                        rb.Append($"<option selected>{name}</option>");
                        rb.Append(type.GetFields()
                            .Where(m => m.Name != "__value")
                            .Select(field => $"<option value=\"{field.Name}\">{field.Name}</option>"));
                        rb.Append("</select>");
                    }
                    else if (IsNumber(type))
                        rb.Append("<input class=\"text-box single-line\" type=\"number\">");
                    else
                        rb.Append("<input class=\"text-box single-line\" type=\"text\">");

                    rb.Append("</div>");
                }

                return rb.ToString();

                //TODO добавить другие числа
                bool IsNumber(Type type) => 
                    type.IsAssignableTo(typeof(int)) || type.IsAssignableTo(typeof(int?));
            }

            public async void WriteTo(TextWriter writer, HtmlEncoder encoder) =>
                await writer.WriteLineAsync(await _resultContentTask);
        }
    }
}
