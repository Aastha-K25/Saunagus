// WebApplication bruges til at konfigurere og starte ASP.NET Core applikationen

// builder bruges til at konfigurere services og middleware i projektet
var builder = WebApplication.CreateBuilder(args);

// Program.cs fungerer som startpunktet for ASP.NET Core API’et

// Her konfigureres:
// - services
// - middleware
// - routes
// - sikkerhed
// - API funktionalitet

// --------------------------------------------------

// Tilføjer controller support til projektet. AddControllers gør det muligt at bruge REST API controllers
// Controllerne håndterer HTTP requests som GET, POST og DELETE (CRUD)
builder.Services.AddControllers();

// --------------------------------------------------
// Tilføjer support til Swagger
// Swagger bruges til:
// - dokumentation
// - test af endpoints
// - visualisering af API’et

// AddEndpointsApiExplorer bruges til at finde API endpoints automatisk
builder.Services.AddEndpointsApiExplorer();

// AddSwaggerGen genererer Swagger dokumentation
builder.Services.AddSwaggerGen();

// --------------------------------------------------

// Tilføjer CORS policy. CORS står for: Cross-Origin Resource Sharing. CORS er en sikkerhedsmekanisme i browseren
// Den bestemmer hvilke klienter som må kommunikere med API’et
// Frontend og API kører ofte på forskellige URLs. Derfor skal CORS aktiveres så browseren tillader kommunikationen

builder.Services.AddCors(options =>
{
    // Opretter policy med navnet "AllowAll"
    options.AddPolicy("AllowAll",

        policy =>
        {
            // Tillader requests fra alle domæner
            policy.AllowAnyOrigin()

                // Tillader alle headers
                .AllowAnyHeader()

                // Tillader alle HTTP metoder
                // fx GET, POST og DELETE
                .AllowAnyMethod();
        });
});

// --------------------------------------------------

// Postman og Swagger kan bruges til at teste REST API endpoints
// Her kan man teste:
// - statuskoder
// - JSON data
// - endpoints
// - requests og responses
// JSON bruges til at sende data mellem frontend og API

// --------------------------------------------------
// Build opretter den færdige app
var app = builder.Build();

// --------------------------------------------------

// Swagger bruges kun i development miljø. Environment bruges til at kontrollere hvilket miljø applikationen kører i

if (app.Environment.IsDevelopment())
{
    // Aktiverer Swagger middleware
    app.UseSwagger();

    // Aktiverer Swagger UI så endpoints kan testes i browseren
    app.UseSwaggerUI();
}

// --------------------------------------------------
// Aktiverer CORS middleware. UseCors fortæller API’et, at CORS policy skal bruges
// "AllowAll" refererer til policy navnet
app.UseCors("AllowAll");

// --------------------------------------------------

// MapControllers forbinder controller routes med API’et. Det betyder at routes som: /api/saunagus nu virker
app.MapControllers();

// --------------------------------------------------
// Run starter web-applikationen. API’et begynder nu at lytte efter requests
app.Run();