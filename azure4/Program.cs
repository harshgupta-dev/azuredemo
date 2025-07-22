var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Configuration.AddAzureAppConfiguration("Endpoint=https://applicationconfigharsh.azconfig.io;Id=xuIP;Secret=2IYlwK45MbS1rmTR4xLB6J8f0B5d3xNYp8Xwu6CEBWGWe2HG38toJQQJ99BGACBsN54axwQzAAABAZAC2uZ2");

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
