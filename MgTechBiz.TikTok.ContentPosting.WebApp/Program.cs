using System.Text.Json;
using System.Text.Json.Nodes;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;
using MgTechBiz.TikTok.ContentPosting.Helpers.Services;
using MgTechBiz.TikTok.ContentPosting.Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<IContentPosting, ContentPosting>();

//builder.Services.AddAuthentication(options =>
//	{
//		options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//		options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//	})
//	.AddCookie()
//	.AddOpenIdConnect(options =>
//	{
//		options.SignInScheme = "Cookies";
//		options.Authority = builder.Configuration["Oidc:Authority"];
//		options.RequireHttpsMetadata = true;
//		options.ClientId = builder.Configuration["Oidc:ClientId"];
//		options.ClientSecret = builder.Configuration["Oidc:ClientSecret"];
//		options.ResponseType = "code";
//		options.UsePkce = true;
//		options.Scope.Add("profile");
//		options.SaveTokens = true;

//		options.Events = new OpenIdConnectEvents
//		{
//			OnTokenResponseReceived = async context =>
//			{
//				var loggerFactory = new LoggerFactory();
//				var logger = loggerFactory.CreateLogger<Type>();
//				//save the tokens
//				var jObject = (await JsonSerializer.DeserializeAsync<JsonObject>(context.Response.Body))?.ToJsonString();
//				logger.LogInformation("token response received....");
//				logger.LogInformation(jObject);
//				var tokenResponse = await JsonSerializer.DeserializeAsync<TokenResponseDto>(context.Response.Body);

//				if (tokenResponse != null)
//				{
//					TokenService.SetToken(tokenResponse);
//				}
//			}
//		};
//	});

builder.Services.Configure<IISServerOptions>(options =>
{
	options.MaxRequestBodySize = int.MaxValue;
});


builder.Services.Configure<FormOptions>(options =>
{
	options.ValueLengthLimit = int.MaxValue;
	options.MultipartBodyLengthLimit = long.MaxValue; // <-- !!! long.MaxValue
	options.MultipartBoundaryLengthLimit = int.MaxValue;
	options.MultipartHeadersCountLimit = int.MaxValue;
	options.MultipartHeadersLengthLimit = int.MaxValue;
});


var app = builder.Build();

app.Use(async (context, next) =>
{
	context.Features.Get<IHttpMaxRequestBodySizeFeature>()!.MaxRequestBodySize = null; // unlimited I guess
	await next.Invoke();
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
//app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
	//.RequireAuthorization();

app.Run();
