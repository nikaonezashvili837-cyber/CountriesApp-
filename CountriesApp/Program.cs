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
    var countiesList = countries.Select(element => $"<li>{element.CountryName}</li>");
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
app.MapGet("/countries/{id:int:range(0,5)}", async (HttpContext context) =>
{
    int id;
    Int32.TryParse(context.Request.RouteValues["id"]?.ToString(), out id);
    var country = countries.Where(element => element.Id == id).Select(element => element.CountryName);
    string html = String.Join("", country);
    await context.Response.WriteAsync($@"
    <html>
     <body>
        <p>
          {html}
        </p>
     </body>
    </html>
    ");
});
app.MapFallback(async (HttpContext context) =>
{
  context.Response.StatusCode = 400;
  string html = $"<h1>Nothing found at {context.Request.Path}</h1>";
  await context.Response.WriteAsync($@"
    <html>
     <body>
        <p>
          {html}
        </p>
     </body>
    </html>
    ");
});
app.MapGet("/", () => "Hello World!");
app.Run();
