using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Collections.Generic;
using YahooFinanceApi;

namespace BlazorApp1.Data
{

    //1. create hub : Hub like stockhub

    //Singleton -IServiceScopeFactory, ILogger<T>, HttpClientFactory
    //Scoped - DbContext, IHubContext<StockHub>
    //Transient -Repositories, EmailService

    //BackgroundService it's singletone services
    public class StockBackgroundService : BackgroundService
    {
        //IServiceScopeFactory is a factory that creates dependency injection (DI) scopes.
        //Allows singleton services to access scoped services properly.
        //IServiceScopeFactory is used to create temporary DI scopes inside a singleton service.
        //We must not inject scoped services directly into singletons (like IHubContext).
        //instead, we create a new scope each time using _scopeFactory.CreateScope().

        // It creates IServiceScope (scoped lifetime) → Allows access to scoped services like IHubContext<StockHub>.
        // Prevents memory leaks and DI lifetime mismatches when used in singleton services.

        private readonly IServiceScopeFactory _scopeFactory;        
        private readonly Dictionary<string, string> _trackedStocks = new();
        //private readonly ConcurrentDictionary<string, string> _trackedStocks = new();
        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(3); // Fetch every 5 sec

        public StockBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public void AddStock(string connectionId, string symbol)
        {
            _trackedStocks[connectionId] = symbol; // Associate stock with connection
        }

        public void RemoveStock(string connectionId)
        {
            _trackedStocks.Remove(connectionId); // Remove on disconnect
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var hubContext = _scopeFactory.CreateScope()
                                  .ServiceProvider
                                  .GetRequiredService<IHubContext<StockHub>>();
            //CancellationToken used for stop this task
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_updateInterval, stoppingToken);//Simulate doing work every 5 seconds

                // Create a temporary  DI scope
                //using var scope = _scopeFactory.CreateScope();
                //var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<StockHub>>();

                foreach (var (connectionId, symbol) in _trackedStocks)
                {
                    var stockData = await FetchStockData(symbol);
                    await hubContext.Clients.Client(connectionId).SendAsync("ReceiveStockUpdate", stockData);
                }
            }
        }

        private async Task<string> FetchStockData(string symbol)
        {
            try
            {
                var securities = await Yahoo.Symbols(symbol + ".NS")
                    .Fields(Field.RegularMarketPrice, Field.RegularMarketTime)
                    .QueryAsync();

                var stock = securities[symbol + ".NS"];
                return $"{{ \"Symbol\": \"{symbol}\", \"Price\": {stock[Field.RegularMarketPrice]}, \"Time\": \"{stock[Field.RegularMarketTime]}\" }}";
            }
            catch
            {
                return "{ \"error\": \"Unable to fetch stock data\" }";
            }
        }
    }
}
