using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_S10203108.Models;

namespace Web_S10203108.BookController
{
    [Authorize]
    public class BookController : Controller
    {
        // GET: Book
        public async Task<ActionResult> Index()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://mstudentproject.azurewebsites.net");
            HttpResponseMessage response = await client.GetAsync("/api/books");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<Book> bookList = JsonConvert.DeserializeObject<List<Book>>(data);
                return View(bookList);
            }
            else
            {
                return View(new List<Book>());
            }
        }
    }
}