using eFilingExcel.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================================
// Controllers
// =====================================================
builder.Services.AddControllers();

// =====================================================
// Swagger / OpenAPI
// =====================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================================
// Services - Dependency Injection
// =====================================================
builder.Services.AddScoped<PembayarCukaiService>();

var app = builder.Build();

// =====================================================
// Swagger UI (Development only)
// =====================================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// =====================================================
// Middleware
// =====================================================
app.UseHttpsRedirection();

app.UseAuthorization();

// =====================================================
// Map Controllers
// =====================================================
app.MapControllers();

app.Run();