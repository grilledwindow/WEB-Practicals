using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web_S10203108.Models;

namespace Web_S10203108.VoteController
{
    public class VoteController : Controller
    {
        public async Task<ActionResult> Details(int id)
        {
            // Make Web API call to get a list of votes related to a BookId
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://mstudentproject.azurewebsites.net");
            // Note: the "id" here is the BookId
            HttpResponseMessage response = await
            client.GetAsync("/api/votes/" + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                List<VoteDetails> voteList =
                 JsonConvert.DeserializeObject<List<VoteDetails>>(data);
                return View(voteList);
            }
            else
            {
                return View(new List<VoteDetails>());
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            // Read BookId and Justification from HTML Form
            int bookid = Convert.ToInt32(collection["item.Id"]);
            string justification = collection["item.Justification"];

            // Transfer data read to a vote object
            Vote vote = new Vote();
            vote.BookId = bookid;
            vote.Justification = justification;

            // Make Web API call to post the vote object
            HttpClient client = new HttpClient();
            client.BaseAddress = new
             Uri("https://mstudentproject.azurewebsites.net");
            //Convert the vote to JSON string
            string json = JsonConvert.SerializeObject(vote);
            StringContent votecontent = new StringContent(json, UnicodeEncoding.UTF8,
           "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/votes",
            votecontent);
            if (response.StatusCode == HttpStatusCode.Created)
            {
                // Successful – code 201 returned
                return RedirectToAction("Details", new { id = bookid });
            }
            else
            {
                // Unsuccessful – other returned code
                TempData["BookId"] = bookid;
                TempData["Justification"] = justification;
                TempData["Message"] = "Fail to add vote record!";
                return RedirectToAction("Index", "Book");
            }
        }
    }
}