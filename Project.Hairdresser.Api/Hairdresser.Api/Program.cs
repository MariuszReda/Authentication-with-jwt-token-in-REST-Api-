using Common.Options;
using Hairdresser.Api.Extensions;
using Hairdresser.Api.Installers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.InstallServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Hairdresser API", Version = "v1" });

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });

});

builder.Services.AddCors(options => options.AddPolicy("AllowOrigin", policy => policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()));


var app = builder.Build();

app.UseAuthorization();

var swaggerOptions = new SwaggerOptions();
app.Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);



app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowOrigin");


app.MapControllers();
app.Run();

//builder.Services.AddSwaggerGen();
//if (app.Environment.IsDevelopment())
//{
//}

//app.UseSwagger(options =>
//{
//    options.RouteTemplate = swaggerOptions.JsonRotue;
//});

//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
//});


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

