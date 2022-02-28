using FVCPD.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FVCPDbContext>(options => {
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseDeveloperExceptionPage();
	//app.UseSwagger();
	//app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
