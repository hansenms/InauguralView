﻿using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InauguralView.Models;
using Microsoft.Azure.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;

namespace InauguralView.Controllers
{
    public class SpielController : Controller
    {
        public SpielController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            SpielEntry s = await DocumentDBRepository<SpielEntry>.GetItemAsync(id);
            return View(s);
        }

        public async Task<IActionResult> Query(string search = "*", int top = 10, int skip = 0)
        {
            string searchServiceName = Configuration["SEARCHSERVICE_NAME"];
            string searchIndex = Configuration["SEARCHSERVICE_INDEX"];
            string searchApiKey = Configuration["SEARCHSERVICE_AUTHKEY"];

            SearchIndexClient indexClient = new SearchIndexClient(searchServiceName, searchIndex, new SearchCredentials(searchApiKey));

            SearchParameters parameters;
            DocumentSearchResult<SpielSearchDoc> results;

            parameters =
                new SearchParameters()
                {
                    Select = new[] { "id", "speaker","spieldate" },
                    Top = top,
                    Skip = skip,
                    IncludeTotalResultCount = true
                };

            results = await indexClient.Documents.SearchAsync<SpielSearchDoc>(search, parameters);

            ViewData["SearchResultsCount"] = results.Count;
            ViewData["SearchTop"] = top;
            ViewData["SearchSkip"] = skip;
            ViewData["SearchString"] = search;

            List<SpielSearchDoc> res = new List<SpielSearchDoc>();

            foreach (SearchResult<SpielSearchDoc> r in results.Results)
            {
                res.Add(r.Document);
            }


            return View(res);
        }
    
       public async Task<IActionResult> Index()
        {
            var spiels = await DocumentDBRepository<SpielEntry>.GetItemsAsync(d => true);
            return View(spiels);
        }
    }
}