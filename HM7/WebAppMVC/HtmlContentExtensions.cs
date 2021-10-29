using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
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
                _resultContentTask = Task.Run(() =>CreateContent(model.GetType().GetProperties()));

            private static string CreateContent(IEnumerable<PropertyInfo> propertyInfos) =>
                propertyInfos
                    .Select(x =>
                        "<div class=\"editor-field\">" +
                        CreateHeaderForProperty(x) +
                        CreateBodyForProperty(x) +
                        "</div>")
                    .Aggregate(string.Concat);

            private static string CreateHeaderForProperty(PropertyInfo prop) =>
                $"<div class=\"editor-label\"><label for=\"{prop.Name}\">" +
                $"{((DisplayAttribute)prop.GetCustomAttribute(typeof(DisplayAttribute)))?.Name ?? FromCamelCase(prop.Name)}" +
                $"</label></div>";

            private static string FromCamelCase(string str) =>
                str.Skip(1).Aggregate(str[0].ToString(), 
                    (current, t) => current + (char.IsUpper(t) ? $" {char.ToLower(t)}" : t));

            private static string CreateBodyForProperty(PropertyInfo prop) =>
                prop.PropertyType.IsAssignableTo(typeof(Enum))
                    ? "<select class=\"form-group\">"
                      + $"<option selected>{prop.Name}</option>"
                      + prop.PropertyType
                          .GetFields()
                          .Where(m => m.Name != "value__")
                          .Select(field => $"<option value=\"{field.Name}\">{field.Name}</option>")
                          .Aggregate(string.Concat)
                      + "</select>"
                    : IsNumber(prop.PropertyType)
                        ? $"<input class=\"text-box single-line\" type=\"number\" name=\"{prop.Name}\">"
                        : $"<input class=\"text-box single-line\" type=\"text\" name=\"{prop.Name}\">";

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

            async void IHtmlContent.WriteTo(TextWriter writer, HtmlEncoder encoder) =>
                writer.WriteLine(await _resultContentTask);
        }
    }
}
