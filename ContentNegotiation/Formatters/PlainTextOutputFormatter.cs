using System.Text;
using ContentNegotiation.Helpers;
using ContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace ContentNegotiation.Formatters
{
     public class PlainTextOutputFormatter : TextOutputFormatter
    {
        public PlainTextOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type? type)
        {
            if (typeof(Student).IsAssignableFrom(type) ||
           typeof(IEnumerable<Student>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext
        context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var data = context.Object as IEnumerable<Student>;

            if (data == null)
                return;

            using (var buffer = new MemoryStream())
            using (var writer = new StreamWriter(buffer, selectedEncoding))
            {
                var plainTextTable = data.ToPlainTextTable();

                writer.WriteLine(plainTextTable);

                writer.Flush();

                buffer.Position = 0;
                await buffer.CopyToAsync(response.Body);
            }

        }
    }
}
