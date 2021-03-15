using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WowAhMonitor.Models;
using WowAhMonitor.Services;

namespace WowAhMonitor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IBlizzardApiService _apiService;
        public List<string> Realms { get; private set; }
        public string Message { get; private set; }

        public IndexModel(IBlizzardApiService apiService)
        {
            _apiService = apiService;
        }

        public void OnGet()
        {
            Message = "Hello";
            Realms = _apiService.GetRealmsLinksAsync().Result.ConnectedRealms
                .Select(selfUri => selfUri.Href.ToString()).ToList();
        }
    }
}