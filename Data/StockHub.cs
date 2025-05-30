using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using YahooFinanceApi;

namespace BlazorApp1.Data
{
    public class StockHub : Hub
    {
        //private static readonly ConcurrentDictionary<string, Timer> _timers = new();
        private readonly StockBackgroundService _stockService;

        public StockHub(StockBackgroundService stockService)
        {
            _stockService = stockService;
        }

        public async Task StartStockUpdates(string symbol)
        {
            _stockService.AddStock(Context.ConnectionId, symbol);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            _stockService.RemoveStock(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public void PrintItems(params List<int>[] items)
        {
            foreach (var item in items)
                Console.WriteLine(item);
        }
    }


}
