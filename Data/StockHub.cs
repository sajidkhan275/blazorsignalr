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

            //var list1 = new List<int> { 1, 2, 3 };
            //var list2 = new List<int> { 4, 5 };
            //PrintItems(list1, list2);
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


        // public override async Task OnConnectedAsync()
        // {
        //     await base.OnConnectedAsync();
        // }

        //public override async Task OnDisconnectedAsync(Exception? exception)
        //{
        //    // Stop and remove the timer for the disconnected client
        //    if (_timers.TryRemove(Context.ConnectionId, out var timer))
        //    {
        //        timer.Dispose();
        //    }

        //    await base.OnDisconnectedAsync(exception);
        //}
        //public async Task StartStockUpdates(string symbol)
        //{
        //    if (!_timers.ContainsKey(symbol))
        //    {
        //        _timers[Context.ConnectionId] = new Timer(async _ => await SendStockUpdate(symbol), null, 0, 20000);
        //        //_timers[symbol] = new Timer(async _ => await SendStockUpdate(symbol), null, 0, 15000);
        //    }
        //}
        //public async Task SendStockUpdate(string stockSymbol)
        //{
        //    var stockData = await GetStock(stockSymbol);
        //    await Clients.Caller.SendAsync("ReceiveStockUpdate", stockData);
        //    //await Clients.All.SendAsync("ReceiveStockUpdate", stockSymbol);
        //}

        //public async Task<decimal> GetStock(string stockSymbo)
        //{
        //    try
        //    {
        //        var securities = await Yahoo.Symbols(stockSymbo + ".NS").Fields(Field.RegularMarketPrice, Field.RegularMarketTime).QueryAsync();
        //        var stock = securities[stockSymbo + ".NS"];
        //        var p = stock[Field.RegularMarketPrice];
        //        return (Convert.ToDecimal(p));

        //    }
        //    catch (Exception ex)
        //    {
        //        return ( 0);
        //    }

        //}

    }


}
