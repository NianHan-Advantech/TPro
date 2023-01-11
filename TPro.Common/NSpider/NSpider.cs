using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;

namespace TPro.Common.NSpider
{
    public class NSpider : INSpider
    {
        private readonly NSpiderOption _option;
        private readonly HttpClient _client;
        private readonly Timer _spiderpool;
        private string _currentUrl { get; set; }
        private List<string> _urlpool { get; set; } = new List<string>();

        public NSpider(NSpiderOption option)
        {
            _option = option;
            _currentUrl = option.StartUrl;
            _client = new HttpClient();
            _spiderpool = new Timer(_option.TimeInterval);
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.0.0 Safari/537.36 Edg/108.0.1462.76");
            _spiderpool.Elapsed += async (s, e) => await HandleUrl();
        }

        public void Start()
        {
            _spiderpool.Start();
        }

        private async Task HandleUrl()
        {
            try
            {
                var response = await _client.GetAsync(_currentUrl);
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    var html = new HtmlDocument();
                    html.Load(stream);
                    HandleHtml(html);
                    foreach (var item in _option.NextUrlsXpaths)
                    {
                        var urls = html.DocumentNode.SelectNodes(item).Select(e => e.InnerText);
                        _urlpool.AddRange(urls);
                    }
                    if (_urlpool.Any())
                    {
                        _currentUrl = _urlpool.First();
                    }
                    else
                    {
                        _spiderpool.Close();
                        _client.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                _spiderpool.Stop();
                throw;
            }
        }

        public void Stop()
        {
            _spiderpool.Stop();
        }

        public void Close()
        {
            _spiderpool.Close();
        }

        public void Dispose()
        {
            _spiderpool.Stop();
            _spiderpool.Close();
            _spiderpool.Dispose();
            _client.Dispose();
        }

        public Func<HtmlDocument, int> HandleHtml = html => 1;

        //public Func<HtmlDocument, List<string>> CountNextUrls = (html) =>
        //{
        //    foreach (var item in _option.NextUrlsXpaths)
        //    {
        //        var urls = html.DocumentNode.SelectNodes(item).Select(e => e.InnerText);
        //        _urlpool.AddRange(urls);
        //    }
        //};
    }

    public interface INSpider : IDisposable
    {
        void Start();

        //string ClimbAsText();

        //Stream ClimbAsStream();

        //HtmlDocument ClimbAsHtml();

        void Stop();

        void Close();
    }

    public class NSpiderOption
    {
        private int timeInterval = 1000;

        public int TimeInterval
        {
            get { return timeInterval; }
            set { if (value < 1000) throw new Exception("The minimum value of TimeInterval is 1000"); timeInterval = value; }
        }

        public string StartUrl { get; set; }

        public List<string> NextUrlsXpaths { get; set; }

    }
}