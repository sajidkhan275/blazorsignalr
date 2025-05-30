using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BlazorApp1.Data;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

var builder = WebApplication.CreateBuilder(args);

//configure replace with builder

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddSingleton<StockBackgroundService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<StockBackgroundService>());

//builder.Services.AddHostedService<StockService>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


string blobConnString = builder.Configuration.GetConnectionString("AzureBlobStorage");
string containerName = "demoblobcontainer";

string keyVaultUri = "https://blobstorageconn.vault.azure.net/";
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());
string blobConnStringKeyVault = builder.Configuration["BlobStorageConnectionString1"];

//var client = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
//KeyVaultSecret secret = await client.GetSecretAsync("BlobStorageConnectionString1");

builder.Services.AddSingleton(sp =>new BlobStorageService(blobConnString, containerName));

//builder.Services.AddTransient<RequestHandler>();

//public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<StockHub>("/stockHub");
app.MapFallbackToPage("/_Host");

//var sp = app.Services.GetRequiredService<StockService>();
//sp.ExecuteAsync();
app.Run();
