using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using Serilog;

namespace ToDoList.Core.PipelineClasses
{
    public class SerilogExceptionLogger : ExceptionLogger
    {
        public async override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            Serilog.Log.Logger.Error(context.Exception, "Unhandled exception");
        }
    }
}