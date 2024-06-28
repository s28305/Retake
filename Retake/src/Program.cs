using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MyServer")!;
builder.Services.AddSingleton<IDepartmentRepository>(departmentRepository => new DepartmentRepository(connectionString));
builder.Services.AddSingleton<IEmployeeRepository>(employeeRepository => new EmployeeRepository(connectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

