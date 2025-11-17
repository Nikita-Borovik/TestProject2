using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Xunit.Abstractions;

namespace TestProject2
{
    public class XUnitTestOutputSink : ILogEventSink
    {
        private static readonly ThreadLocal<ITestOutputHelper?> _testOutput = new ThreadLocal<ITestOutputHelper?>();
        private readonly ITextFormatter _formatter;

        public XUnitTestOutputSink(string outputTemplate)
        {
            _formatter = new MessageTemplateTextFormatter(outputTemplate, null);
        }

        public static void SetTestOutput(ITestOutputHelper? testOutput)
        {
            _testOutput.Value = testOutput;
        }

        public void Emit(LogEvent logEvent)
        {
            var output = _testOutput.Value;
            if (output != null)
            {
                using var writer = new StringWriter();
                _formatter.Format(logEvent, writer);
                output.WriteLine(writer.ToString().TrimEnd());
            }
        }
    }
}

