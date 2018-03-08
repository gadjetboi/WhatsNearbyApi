using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Yelp.Api;

namespace WhatsNearbyApi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/yelp")]
    [Authorize]
    public class YelpController : Controller
    {
        public YelpController() {
            //controller statement
        }

        [HttpGet]
        [Route("GetBusiness")]
        public async Task<IActionResult> GetBusiness(double lat, double lng, string categories = null) {
            try {
                var request = new Yelp.Api.Models.SearchRequest();
                request.Latitude = lat;
                request.Longitude = lng;
                if (categories != null) request.Categories = categories;
                request.MaxResults = 40;
                //request.Radius = 20; //maximum 25miles
                request.OpenNow = true; //get only open stores

                var client = new Client("aFKewegoRzd6G0g_S3-UQkII28xShW1JGZqS8otwLCsmeEYrnaOUGR6XS2ub-IXo0Wx8uWgckNrBbI568fkpN9K3rYgNpMNCr7RuqjlfqY825esrWcfn7_FCOOWYWnYx");
                var results = await client.SearchBusinessesAllAsync(request);

                return Ok(results);
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
            
        }

    }
}
