var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSpaStaticFiles(config =>
        config.RootPath = "Client/dist"
    );

var app = builder.Build();

app.UseSpaStaticFiles();
app.UseSpa(spaBuilder => 
{
    spaBuilder.Options.SourcePath = "Client";
    
    Console.WriteLine(builder.Environment.IsDevelopment());
    Console.WriteLine(app.Environment.IsDevelopment());

    if(builder.Environment.IsDevelopment())
    {
        spaBuilder.UseProxyToSpaDevelopmentServer("http://localhost:3000");
    }
});

app.UseRouting();

app.Run();
