var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Remove or comment out this line if it exists
// app.UseHttpsRedirection();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();