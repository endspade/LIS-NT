//using Microsoft.AspNetCore.Authentication.Negotiate;

using Newtonsoft.Json.Linq;
using NGCP.BaseClass;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<clsSessionUser>();
//builder.Services.AddScoped<NonceService>();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
//   .AddNegotiate();

//builder.Services.AddAuthorization(options =>
//{
//    // By default, all incoming requests will be authorized according to the default policy.
//    options.FallbackPolicy = options.DefaultPolicy;
//});
//builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache(); // Required for session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP only
    options.Cookie.IsEssential = true; // Make the session cookie essential
});


builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    // Call the nonce generation method and print it to the console
    var nonce = GenerateNonce(16); // 16 bytes = 32 hex characters

    context.Response.Headers.Remove("Server");
    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Remove("www-authenticate");
    context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
    context.Response.Headers.Append("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Permitted-Cross-Domain-Policies", "none");
    context.Response.Headers.Append("Expect-CT", "max-age=0, enforce");
    context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000, includeSubDomains");
    context.Response.Headers.Append("Permissions-Policy", "accelerometer=(), camera=(), geolocation=(), gyroscope=(), magnetometer=(), microphone=(), payment=(), usb=()");
    context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Append("Access-Control-Allow-Methods", "GET,PUT,POST,DELETE");
    context.Response.Headers.Append("Access-Control-Allow-Headers", "Accept");
    context.Response.Headers.Append("Access-Control-Expose-Headers", "*");
    context.Response.Headers.Append("Clear-Site-Data", "\"\"");
    context.Response.Headers.Append("Cross-Origin-Embedder-Policy", "cross-origin");
    context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin");
    context.Response.Headers.Append("Cross-Origin-Resource-Policy", "same-origin");
    context.Response.Headers.Append("Access-Control-Allow-Credentials", "true");
    context.Response.Headers.Append("Access-Control-Max-Age", "600");
    context.Response.Headers.Append("Cache-Control", "Max-Age = 600");
    context.Response.Headers.Append("Content-Security-Policy", $"script-src 'self' 'unsafe-eval' 'nonce-{nonce}';");

    // Store the nonce in the HttpContext for later use in the view
    context.Items["CspNonce"] = nonce;

    await next();
    List<int> redirectList = new List<int> { 404, 500, 502, 401 };
    if (redirectList.IndexOf(context.Response.StatusCode) != -1)
    {
        List<string> acceptableContentType = new List<string> { "multipart/form-data", "application/x-www-form-urlencoded" };

        if (context.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
        {
            context.Request.Path = "/Helper/ErrorPage/" + context.Response.StatusCode.ToString();
        }
        else
        {
            context.Request.Path = "/Helper/JSONError/" + context.Response.StatusCode.ToString();
        }
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Home}/{action=Index}/{id?}");
    pattern: "{controller=Home}/{action=Login}/{id?}");


app.Run();


string GenerateNonce(int byteSize)
{
    // Create a byte array to hold the random bytes
    byte[] randomBytes = new byte[byteSize];

    // Fill the array with cryptographically strong random bytes
    using (var rng = RandomNumberGenerator.Create())
    {
        rng.GetBytes(randomBytes);
    }

    // Convert the byte array to a hexadecimal string
    return BitConverter.ToString(randomBytes).Replace("-", "").ToLowerInvariant();
}