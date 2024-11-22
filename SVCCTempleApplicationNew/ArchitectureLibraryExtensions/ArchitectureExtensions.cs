using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ArchitectureLibraryExtensions
{
    public static class ArchitectureExtensions
    {
        public static string ViewToHtmlString(this Controller controller, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            }
            controller.ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
        public static bool DateDifference(this DateTime dateTime, DateTime compareDateTime, int value)
        {
            return true;
        }
        public static byte[] ToByteArray(this MailMessage message)
        {
            var assembly = typeof(SmtpClient).Assembly;
            var mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

            using (var memoryStream = new MemoryStream())
            {
                // Get reflection info for MailWriter contructor
                var mailWriterContructor = mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);

                // Construct MailWriter object with our FileStream
                var mailWriter = mailWriterContructor.Invoke(new object[] { memoryStream });

                // Get reflection info for Send() method on MailMessage
                var sendMethod = typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call method passing in MailWriter
                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { mailWriter, true, true }, null);

                // Finally get reflection info for Close() method on our MailWriter
                var closeMethod = mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call close method
                closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);

                return memoryStream.ToArray();
            }
        }
        //The below method needs System.Web.Mvc - I believe so - I forgot how got it earlier
        //public static string ViewToHTMLString(string _viewName, object _modelInstance, ControllerContext _controllerContextInstance, ViewDataDictionary _viewDataDictionaryInstance, TempDataDictionary _tempDataDictionaryInstance)
        //{
        //    _viewDataDictionaryInstance.Model = _modelInstance;
        //    using (StringWriter writer = new StringWriter())
        //    {
        //        ViewEngineResult result = ViewEngines.Engines.FindPartialView(_controllerContextInstance, _viewName);
        //        ViewContext viewContext = new ViewContext(_controllerContextInstance, result.View, _viewDataDictionaryInstance, _tempDataDictionaryInstance, writer);
        //        result.View.Render(viewContext, writer);
        //        result.ViewEngine.ReleaseView(_controllerContextInstance, result.View);
        //        return writer.GetStringBuilder().ToString();
        //    }
        //}
        public static string EnumDescription(this Enum genericEnum)
        {
            Type genericEnumType = genericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            if ((memberInfo != null && memberInfo.Length > 0))
            {
                var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if ((_Attribs != null && _Attribs.Count() > 0))
                {
                    return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }
            return genericEnum.ToString();
        }
        /*
        https://www.google.com/search?ei=uZjlXIj-M4rP0PEP3v-VsA4&q=%3Cbr+%2F%3E+in+LabelFor+is+displaying+as+is+in+Razor&oq=%3Cbr+%2F%3E+in+LabelFor+is+displaying+as+is+in+Razor&gs_l=psy-ab.3...2943.14480..14680...5.0..1.232.5574.24j27j2......0....1..gws-wiz.....0..0i71j0i131j0j0i67j0i131i67j0i10i67j0i10j0i22i30j0i22i10i30j0i13i30j33i299j33i160.g3I5depWi8M
        https://stackoverflow.com/questions/9030763/how-to-make-html-displayfor-display-line-breaks
        public static MvcHtmlString DisplayWithBreaksFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model).Replace("\r\n", "<br />\r\n");

            if (String.IsNullOrEmpty(model))
                return MvcHtmlString.Empty;

            return MvcHtmlString.Create(model);
        }
        @Html.DisplayWithBreaksFor(m => m.MultiLineField)
        public static class HtmlHelpers
        {
            public static IHtmlString ReplaceBreaks(this HtmlHelper helper, string str)
            {
                return MvcHtmlString.Create(str.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).Aggregate((a, b) => a + "<br />" + b));
            }
        }
        @using Shaul.Web.Helpers
        @Html.ReplaceBreaks(Model.MultiLineText)
        */
    }
}
