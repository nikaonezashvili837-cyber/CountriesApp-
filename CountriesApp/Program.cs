var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Country[] countries =
[
    new Country(0, "United States"),
    new Country(1, "Canada"),
    new Country(2, "United Kingdom"),
    new Country(3, "India"),
    new Country(4, "Japan")
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
