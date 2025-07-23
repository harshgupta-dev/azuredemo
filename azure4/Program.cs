using Microsoft.FeatureManagement;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Azure App Configuration
builder.Configuration.AddAzureAppConfiguration(
    options =>
    {
        options.Connect("Endpoint=https://democonfigharsh.azconfig.io;Id=36/y;Secret=9fgOe8R782nw12VuRTznx3fRnIWEtD2ozcsA7C5S6eVcz1bZ4igHJQQJ99BGACBsN54axwQzAAABAZAC4YJQ");
        options.UseFeatureFlags();
    }
    );

builder.Services.AddFeatureManagement();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
