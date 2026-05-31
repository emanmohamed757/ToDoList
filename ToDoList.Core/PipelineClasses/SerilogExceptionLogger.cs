using System.Web.Http.ExceptionHandling;

namespace ToDoList.Core.PipelineClasses
{
    public class SerilogExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            Serilog.Log.Logger.Error(context.Exception, "Unhandled exception");
        }
    }
}