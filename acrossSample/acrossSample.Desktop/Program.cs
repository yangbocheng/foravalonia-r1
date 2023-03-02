using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Serilog;
using ReactiveUI;

namespace acrossSample.Desktop
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            var logpath = "logs/myapp.txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logpath,rollingInterval:RollingInterval.Day)
                .CreateLogger();

            Log.Information("app is running");

            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            RxApp.DefaultExceptionHandler = new MyCoolObservableExceptionHandler();
            try
            {
                BuildAvaloniaApp()
                    .StartWithClassicDesktopLifetime(args);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
            }
            finally
            {
                Log.CloseAndFlush();
            }

            
        }

        private static void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Log.Error(e.Exception, "TaskScheduler_UnobservedTaskException");
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
    }

    public class MyCoolObservableExceptionHandler : IObserver<Exception>
    {
        public void OnNext(Exception value)
        {
            if (Debugger.IsAttached) Debugger.Break();

            Log.Fatal(value, "Rx.OnNext.");

            RxApp.MainThreadScheduler.Schedule(() => { throw value; });
        }

        public void OnError(Exception error)
        {
            if (Debugger.IsAttached) Debugger.Break();

            Log.Fatal(error, "Rx.OnError.");

            RxApp.MainThreadScheduler.Schedule(() => { throw error; });
        }

        public void OnCompleted()
        {
            if (Debugger.IsAttached) Debugger.Break();
            RxApp.MainThreadScheduler.Schedule(() => { throw new NotImplementedException(); });
        }
    }
}