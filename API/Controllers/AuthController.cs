using System.Text.RegularExpressions;
using API.models;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly PetContext _context;

    public AuthController(PetContext context)
    {
        _context = context;
    }


    [HttpGet]
    [Route("login")]
    public async Task<ActionResult<Response<string?>>> Login()
    {
        // var client = new RestClient("https://dev-tt6-hw09.us.auth0.com");
        // var request = new RestRequest("/oauth/token",Method.Post);

        // //request.AddHeader("content-type", "application/json");

        // request.AddHeader("content-type", "application/x-www-form-urlencoded");
        // request.AddParameter("application/x-www-form-urlencoded", "grant_type=password&username=dnadistrictservices@gmail.com&password=Daniel@daniel&audience=https://diploma-challenge-sem-1.com.au&client_id=taBuUExBpBMEMagUBgo0omd9W8kli7eC&client_secret=dk3zpnzWxeDfdVlmUOs4Wgl3dcw4fCjG6EiO0tmCcjsoWBhRs5nDPkHponp_A-s7", ParameterType.RequestBody);

        //request.AddParameter("application/json", "{\"grant_type\":\"password\",\"username\":\"dnadistrictservices@gmail.com\",\"password\":\"Daniel_180\",\"audience\":\"https://diploma-challenge-sem-1.com.au\"},\"client_id\":\"taBuUExBpBMEMagUBgo0omd9W8kli7eC\",\"client_secret\":\"dk3zpnzWxeDfdVlmUOs4Wgl3dcw4fCjG6EiO0tmCcjsoWBhRs5nDPkHponp_A-s7\"", ParameterType.RequestBody);
        //request.AddParameter("application/x-www-form-urlencoded", "response_type=code&client_id=L6nnmAddJrNPKicBm97WFD8I6flvCgiy&redirect_uri=undefined&audience=https://dev-tt6-hw09.us.auth0.com/api/v2/", ParameterType.RequestBody);




        //request.AddParameter("application/json", "{\"grant_type\":\"password\",\"username\":\"dnadistrictservices@gmail.com\",\"password\":\"Daniel_180\",\"audience\":\"https://dev-tt6-hw09.us.auth0.com/api/v2/\"},\"client_id\":\"L6nnmAddJrNPKicBm97WFD8I6flvCgiy\",\"client_secret\":\"2xA-2uwAJ7Iye8yV1OZIg_jBTK0MKJtLyo-BNV6FPI_KD3QaemxHGYJViRnKCVvD\"", ParameterType.RequestBody);
        // request.AddQueryParameter("grant_type","password");
        // request.AddQueryParameter("username","dnadistrictservices@gmail.com");
        // request.AddQueryParameter("password","Daniel_180");
        // request.AddQueryParameter("audience","https://dev-tt6-hw09.us.auth0.com/api/v2/");
        // request.AddQueryParameter("client_id","L6nnmAddJrNPKicBm97WFD8I6flvCgiy");
        // request.AddQueryParameter("client_secret","2xA-2uwAJ7Iye8yV1OZIg_jBTK0MKJtLyo-BNV6FPI_KD3QaemxHGYJViRnKCVvD");
        // request.AddQueryParameter("scope","read:user write:user write:admin");
        //RestResponse response = client.Execute(request);

        var client = new HttpClient();
        var requestContent = new FormUrlEncodedContent(new[] {
            new KeyValuePair<string,string>("grant_type","password"),
            new KeyValuePair<string,string>("username","dnadistrictservices"),
            new KeyValuePair<string,string>("password","Daniel@daniel"),
            new KeyValuePair<string,string>("audience","https://diploma-challenge-sem-1.com.au&client_id=taBuUExBpBMEMagUBgo0omd9W8kli7eC"),
            new KeyValuePair<string,string>("scope","read:user"),
            new KeyValuePair<string,string>("client_id","taBuUExBpBMEMagUBgo0omd9W8kli7eC"),
            new KeyValuePair<string,string>("client_secret","dk3zpnzWxeDfdVlmUOs4Wgl3dcw4fCjG6EiO0tmCcjsoWBhRs5nDPkHponp_A-s7"),
        });

        HttpResponseMessage response = await client.PostAsync(
            "https://dev-tt6-hw09.us.auth0.com/oauth/token", requestContent
        );

        // Get the response content.
        HttpContent responseContent = response.Content;

        return Ok(response);
    }

}