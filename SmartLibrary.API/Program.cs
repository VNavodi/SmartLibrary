using Microsoft.EntityFrameworkCore;
using SmartLibrary.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with In-Memory Database
builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseInMemoryDatabase("SmartLibraryDB"));

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    await db.Database.EnsureCreatedAsync();
}

// Configure HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// ===== MINIMAL API ENDPOINTS =====

// GET: /authors
app.MapGet("/authors", async (LibraryContext db) =>
{
    var authors = await db.Authors.ToArrayAsync();
    return Results.Ok(authors);
});

// GET: /authors/1
app.MapGet("/authors/{id}", async (int id, LibraryContext db) =>
{
    var author = await db.Authors.FindAsync(id);
    if (author == null)
        return Results.NotFound();
    return Results.Ok(author);
})
.WithName("GetAuthorMinimal");

// POST: /authors
app.MapPost("/authors", async (Author author, LibraryContext db) =>
{
    db.Authors.Add(author);
    await db.SaveChangesAsync();
    return Results.CreatedAtRoute("GetAuthorMinimal",
        new { id = author.Id }, author);
});

// PUT: /authors/1
app.MapPut("/authors/{id}", async (int id, Author author, LibraryContext db) =>
{
    if (id != author.Id)
        return Results.BadRequest();

    db.Entry(author).State = EntityState.Modified;

    try
    {
        await db.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!db.Authors.Any(a => a.Id == id))
            return Results.NotFound();
        else
            throw;
    }

    return Results.NoContent();
});

// DELETE: /authors/1
app.MapDelete("/authors/{id}", async (int id, LibraryContext db) =>
{
    var author = await db.Authors.FindAsync(id);
    if (author == null)
        return Results.NotFound();

    db.Authors.Remove(author);
    await db.SaveChangesAsync();

    return Results.Ok(author);
});


// GET: /categories
app.MapGet("/categories", async (LibraryContext db) =>
    Results.Ok(await db.Categories.ToArrayAsync()));

// GET: /categories/1
app.MapGet("/categories/{id}", async (int id, LibraryContext db) =>
{
    var category = await db.Categories.FindAsync(id);
    if (category == null)
        return Results.NotFound();
    return Results.Ok(category);
})
.WithName("GetCategoryMinimal");

// POST: /categories
app.MapPost("/categories", async (Category category, LibraryContext db) =>
{
    db.Categories.Add(category);
    await db.SaveChangesAsync();
    return Results.CreatedAtRoute("GetCategoryMinimal",
        new { id = category.Id }, category);
});

// DELETE: /categories/1
app.MapDelete("/categories/{id}", async (int id, LibraryContext db) =>
{
    var category = await db.Categories.FindAsync(id);
    if (category == null)
        return Results.NotFound();

    db.Categories.Remove(category);
    await db.SaveChangesAsync();

    return Results.Ok(category);
});

app.Run();