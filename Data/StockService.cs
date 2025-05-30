
using Microsoft.AspNetCore.SignalR;
using YahooFinanceApi;

namespace BlazorApp1.Data
{
    public class StockService : BackgroundService
    {
        private readonly IHubContext<StockHub> _hubContext;

        public StockService(IHubContext<StockHub> hubContext)
        {
            _hubContext = hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string symbol = "TCS.NS";
            var (stockSymbol, price) = await GetStock(symbol);
            await _hubContext.Clients.All.SendAsync("ReceiveStockUpdate", stockSymbol, price);
        }

        public async Task<(string symbol, decimal price)> GetStock(string stockSymbo)
        {
            try
            {
                var securities = await Yahoo.Symbols(stockSymbo).Fields(Field.RegularMarketPrice, Field.RegularMarketTime).QueryAsync();
                var stock = securities[stockSymbo];
                var p = stock[Field.RegularMarketPrice];
                return (stockSymbo, Convert.ToDecimal(p));
            }
            catch (Exception ex)
            {
                return (stockSymbo,0);
            }
        }
    }
}
