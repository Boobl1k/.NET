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

            private static string CreateContent(IEnumerable<PropertyInfo> propertyInfos)
            {
                var rb = new StringBuilder();

                foreach (var propertyInfo in propertyInfos)
                {
                    rb.Append("<div class=\"editor-field\">");

                    rb.Append(CreateHeaderForProperty(propertyInfo));
                    rb.Append(CreateBodyForProperty(propertyInfo));

                    rb.Append("</div>");
                }

                return rb.ToString();
            }

            private static string CreateHeaderForProperty(PropertyInfo prop)
            {
                var res = string.Empty;

                res += "<div class=\"editor-label\">";
                res += $"<label for=\"{prop.Name}\">{prop.Name}</label>";
                res += "</div>";
                
                return res;
            }
            private static string CreateBodyForProperty(PropertyInfo prop)
            {
                var result = string.Empty;
                
                if (prop.PropertyType.IsAssignableTo(typeof(Enum)))
                {
                    result += "<select class=\"form-group\">";
                    result += $"<option selected>{prop.Name}</option>";
                    result += prop.PropertyType
                        .GetFields()
                        .Where(m => m.Name != "value__")
                        .Select(field => $"<option value=\"{field.Name}\">{field.Name}</option>")
                        .Aggregate(string.Concat);
                    result += "</select>";
                }
                else if (IsNumber(prop.PropertyType))
                    result += $"<input class=\"text-box single-line\" type=\"number\" name=\"{prop.Name}\">";
                else
                    result += $"<input class=\"text-box single-line\" type=\"text\" name=\"{prop.Name}\">";

                return result;
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
