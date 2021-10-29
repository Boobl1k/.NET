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
            private readonly Task<string> _resultContentTask;

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
                        rb.Append(type
                            .GetFields()
                            .Where(m => m.Name != "value__")
                            .Select(field => $"<option value=\"{field.Name}\">{field.Name}</option>")
                            .Aggregate(string.Concat));
                        rb.Append("</select>");
                    }
                    else if (IsNumber(type))
                        rb.Append("<input class=\"text-box single-line\" type=\"number\">");
                    else
                        rb.Append("<input class=\"text-box single-line\" type=\"text\">");

                    rb.Append("</div>");
                }

                return rb.ToString();
            }

            private static readonly Type[] numberTypesArray =
            {
                typeof(int), typeof(int?),
                typeof(uint), typeof(uint?),
                typeof(short), typeof(short?),
                typeof(ushort), typeof(ushort?),
                typeof(long), typeof(long?),
                typeof(ulong), typeof(ulong?),
                typeof(nint), typeof(nint?),
                typeof(byte), typeof(byte?),
                typeof(float), typeof(float?),
                typeof(double), typeof(double?),
                typeof(decimal), typeof(decimal?)
            };

            private static bool IsNumber(Type type) => numberTypesArray.Any(type.IsAssignableTo);

            public async void WriteTo(TextWriter writer, HtmlEncoder encoder) =>
                await writer.WriteLineAsync(await _resultContentTask);
        }
    }
}
