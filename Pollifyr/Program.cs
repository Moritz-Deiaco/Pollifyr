
/*
using Logging.Net;
using MoonCore.Abstractions;
using MoonCoreUI.Services;
using Pollifyr.App.Services.Partials;
using Pollifyr.App.Database;
using Pollifyr.App.Helpers.Startup;
using Pollifyr.App.Repository;
using Pollifyr.App.Services;
using Pollifyr.App.Services.Auth;
using Pollifyr.App.Services.Surveys;
using CookieService = Pollifyr.App.Services.Auth.CookieService;

Console.WriteLine();
Console.WriteLine("Pollifyr");
Console.WriteLine($"Copyright © 2023-{DateTime.UtcNow.Year} mxritz.xyz");
Console.WriteLine();

var builder = WebApplication.CreateBuilder(args);

Logger.UseMBLogger();

ConfigHelper configHelper = new();
await configHelper.Perform();
ConfigService configService = new();
builder.Services.AddSingleton<ConfigService>();

// Services / Database
DbHelper databaseCheckup = new (configService);
await databaseCheckup.Perform();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddScoped(typeof(Repository<>), typeof(GenericRepository<>));

// Services / Imprint
ImprintHelper imprintHelper = new();
await imprintHelper.Perform();
builder.Services.AddSingleton<ImprintService>();

// Services /  Identity
builder.Services.AddSingleton<DateTimeService>();
builder.Services.AddScoped<CookieService>();
builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpContextAccessor();

// Services / Alerts
builder.Services.AddScoped<AlertService>();

// Services / Surveys
builder.Services.AddScoped<SurveyService>();
builder.Services.AddScoped<QuestionService>();  
builder.Services.AddScoped<AnswerService>();

// Services / MoonCore
ToastService.Prefix = "pollifyr.toasts";
ModalService.Prefix = "pollifyr.modals";
AlertService.Prefix = "pollifyr.alerts";
ClipboardService.Prefix = "pollifyr.clipboard";
FileDownloadService.Prefix = "pollifyr.utils";

builder.Services.AddScoped<ClipboardService>();
builder.Services.AddScoped<ModalService>();
builder.Services.AddScoped<ToastService>();

// Configure the Webserver to run on the port specified in the config file
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(configService.Get().Properties.Port);
});

Logger.Info("Running on http://localhost:"+configService.Get().Properties.Port);
    
// Required Services
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (configService.Get().Properties.UseHsts)
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
*/


namespace Pollifyr
{
    public class Program
    {
        private static readonly Startup Startup = new();

        public static async Task Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Pollifyr");
            Console.WriteLine($"Copyright © 2023-{DateTime.UtcNow.Year} mxritz.xyz");
            Console.WriteLine();

            await Startup.Init(args);
            await Startup.Start();
        }
    }
}




