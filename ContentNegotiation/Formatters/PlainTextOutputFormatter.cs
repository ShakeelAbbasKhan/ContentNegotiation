using System.Text;
using ContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace ContentNegotiationDemo.Formatters
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
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<Student>)
            {
                foreach (var company in (IEnumerable<Student>)context.Object)
                {
                    FormatCsv(buffer, company);
                }
            }
            else
            {
                FormatCsv(buffer, (Student)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, Student student)
        {
            buffer.AppendLine($"Id: {student.Id}");
            buffer.AppendLine($"FirstName: {student.FirstName}");
            buffer.AppendLine($"LastName: {student.LastName}");
            buffer.AppendLine();
        }
    }
}
