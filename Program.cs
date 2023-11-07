using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication3.DataContext;
using WebApplication3.Model;

var builder = WebApplication.CreateBuilder(args);


// Add dbContext, here you can we are using In-memory databse.
builder.Services.AddDbContext<MyDataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/SaveStudent", async (Student student, MyDataContext db) =>
{
    db.Students.Add(student);
    await db.SaveChangesAsync();

    return Results.Created($"/save/{student.Id}", student);
});

app.MapGet("/GetAllStudent", async (MyDataContext db) =>
   await db.Students.ToListAsync());

app.MapPut("/UpdateStudents/{id}", async (int id, Student studentinput, MyDataContext db) =>
{
    var student = await db.Students.FindAsync(id);

    if (student is null) return Results.NotFound();

    student.Name = studentinput.Name;
    student.Phone = studentinput.Phone;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/ DeleteStudent /{id}", async (int id, MyDataContext db) =>
{
    if (await db.Students.FindAsync(id) is Student student)
    {
        db.Students.Remove(student);
        await db.SaveChangesAsync();
        return Results.Ok(student);
    }
    return Results.NotFound();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
