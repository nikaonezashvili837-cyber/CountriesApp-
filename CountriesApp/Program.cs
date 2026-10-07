var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
string[] countries =
[
    "United States",
    "Canada",
    "United Kingdom",
    "India",
    "Japan"
];
app.MapGet("/countries", async (HttpContext context) =>
{
    var countiesList = countries.Select(element => $"<li>{element}</li>");
    string html = String.Join("", countiesList);
    await context.Response.WriteAsync($@"
    <html>
     <body>
        <ul>
          {html}
        </ul>
     </body>
    </html>
    ");
});
app.MapGet("/", () => "Hello World!");
app.Run();
