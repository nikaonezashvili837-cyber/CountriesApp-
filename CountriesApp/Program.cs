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
app.MapGet("/countries/{id:int:range(0,4)}", async (HttpContext context ,int id) =>
{
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
  var idValue = context.Request.Path.Value?.Split('/').LastOrDefault();
  int id;
  Int32.TryParse(idValue, out id);
  if (id > 100)
  {
    context.Response.StatusCode = 400;
    await context.Response.WriteAsync($@"
    <html>
     <body>
        <p>
          <h1>The CountryID should be between 1 and 100</h1>
        </p>
     </body>
    </html>
    ");
    return;
  }
  context.Response.StatusCode = 40;
  await context.Response.WriteAsync($@"
    <html>
     <body>
        <p>
          <h1>No countries</h1>
        </p>
     </body>
    </html>
    ");
});
app.MapGet("/", async (HttpContext context) =>
{
  await context.Response.WriteAsync($@"
    <html>
     <body>
        <p>
          <h1>Hello world</h1>
        </p>
     </body>
    </html>
    ");
});
app.Run();
