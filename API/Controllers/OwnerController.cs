using System.Text.RegularExpressions;
using API.Helpers;
using API.models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace API.Controllers;

[ApiController]
[Route("api/owner")]
public class OwnerController : ControllerBase
{
    private readonly PetContext _context;

    protected readonly IConfiguration Configuration;

    public OwnerController(PetContext context,IConfiguration configuration)
    {
        _context = context;
        Configuration = configuration;
    }

    [HttpGet]
    [Route("test")]
    public string GetDBToken(){
        ManagementHelper managementHelper = new ManagementHelper();

        var token = managementHelper.GetManagementToken(Configuration);
        return token;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<Response<Owner?>>> CreateOwner([FromBody] OwnerSignUpReq ownerSignUpReq)
    {
        try
        {
            Response<Owner?> response;

            //check owner email is not already in the database
            var isOwner = await _context.Owner
            .Where(o => o.Email == ownerSignUpReq.email)
            .FirstOrDefaultAsync();

            
            if ( isOwner != null ){
                response = new Response<Owner?>(isOwner, false, "Owner already Exists!");
                return StatusCode(409, response);
            }

            //todo environment variables
            //get token for management API
            var client = new RestClient("https://dev-tt6-hw09.us.auth0.com");
            var request = new RestRequest("/oauth/token", Method.Post);
            request.AddHeader("content-type", "application/json");

            //todo clean up parameters
            request.AddParameter("application/json", "{\"client_id\":\"kUAAhoahZIBdb6SMoQZbryn9fZ6WIbsy\",\"client_secret\":\"zTHQ3l3jxD_coV1WO5xJGe1GyXOdZfwapI54k-EPLc3l4NuuyHAvm9c1UpwlObhN\",\"audience\":\"https://dev-tt6-hw09.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            RestResponse tokenResponse = client.Execute(request);

            dynamic token = tokenResponse.Content != null ? JObject.Parse(tokenResponse.Content)["access_token"]!.ToString() : "";

            var clientManagement = new ManagementApiClient(token, new Uri("https://dev-tt6-hw09.us.auth0.com/api/v2"));

            var newUser = new UserCreateRequest();
            //Authentication database name
            newUser.Connection = "Username-Password-Authentication";
            newUser.Email = ownerSignUpReq.email;                        
            newUser.FirstName = ownerSignUpReq.firstName;
            newUser.LastName = ownerSignUpReq.lastName;

            var resp = await clientManagement
            .Users
            .CreateAsync(newUser);

            var userID = resp.UserId;

            var newOwner =
            new Owner(
            userID,
            ownerSignUpReq.lastName,
            ownerSignUpReq.firstName,
            ownerSignUpReq.phoneNumber,
            ownerSignUpReq.email);

            await _context.Owner.AddAsync(newOwner);
            await _context.SaveChangesAsync();



            response = new Response<Owner?>(newOwner, true, "Owner successfully created.");
            
            return Ok(response);
                     
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    
    [HttpGet]
    [Authorize]
    [Route("get-user")]
    public async Task<ActionResult<Response<Owner?>>> CreateOwner(){
        var sub = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var owner = await _context.Owner
        .Where(o => o.UserID == sub)
        .FirstOrDefaultAsync();

        var response = new Response<Owner?>(owner, true, "Owner Successfully returned.");

        return Ok(response);

    }

    [HttpPost]
    [Authorize]
    [Route("update-owner")]
    public async Task<ActionResult<Response<Owner?>>> UpdateOwner([FromBody] OwnerSignUpReq? updateUserReq){

        var sub = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var owner = await _context.Owner
        .Where(o => o.UserID == sub)
        .FirstOrDefaultAsync();

        if(owner == null){
            //create owner table record
            return StatusCode(409, "boo");
        }

        //get token for management API
        var client = new RestClient("https://dev-tt6-hw09.us.auth0.com");
        var request = new RestRequest("/oauth/token", Method.Post);
        request.AddHeader("content-type", "application/json");

        //todo clean up parameters
        request.AddParameter("application/json", "{\"client_id\":\"kUAAhoahZIBdb6SMoQZbryn9fZ6WIbsy\",\"client_secret\":\"zTHQ3l3jxD_coV1WO5xJGe1GyXOdZfwapI54k-EPLc3l4NuuyHAvm9c1UpwlObhN\",\"audience\":\"https://dev-tt6-hw09.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
        RestResponse tokenResponse = client.Execute(request);

        dynamic token = tokenResponse.Content != null ? JObject.Parse(tokenResponse.Content)["access_token"]!.ToString() : "";

        var clientManagement = new ManagementApiClient(token, new Uri("https://dev-tt6-hw09.us.auth0.com/api/v2"));

        var updateUser = new UserUpdateRequest();

        updateUser.Email = updateUserReq?.email != null ? updateUserReq.email : null;        
        updateUser.FirstName = updateUserReq?.firstName != null ? updateUserReq.firstName : null;
        updateUser.LastName = updateUserReq?.lastName != null ? updateUserReq.lastName : null;


        await clientManagement.Users.UpdateAsync(sub, updateUser);

        owner.Email = updateUserReq?.email != null ? updateUserReq.email : owner.Email;
        owner.Firstname = updateUserReq?.firstName != null ? updateUserReq.firstName : owner.Firstname;
        owner.Surname = updateUserReq?.lastName != null ? updateUserReq.lastName : owner.Surname;
        owner.Phone = updateUserReq?.phoneNumber != null ? updateUserReq.phoneNumber : owner.Phone;

        await _context.SaveChangesAsync();

        return Ok(owner);

    }

    [HttpGet]
    [Authorize] //("read:message")
    [Route("view-pets")]
    public async Task<ActionResult<List<Pet>>> ViewPets(){
        
        var sub = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        int ownerID = await _context.Owner
        .Where(o => o.UserID == sub)
        .Select(o => o.OwnerId)
        .FirstOrDefaultAsync();

        if(ownerID <= 0){
            //create owner table record
            return StatusCode(409, "boo");
        }


        var pets = await _context.Pet
        .Where(p => p.OwnerId == ownerID)
        .ToListAsync();

        return Ok(pets);

    }


    [HttpGet]
    [Authorize]
    [Route("view-treatments")]
    public async Task<ActionResult<List<Treatment>>> ViewTreatments()
    {

        var sub = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        int ownerID = await _context.Owner
        .Where(o => o.UserID == sub)
        .Select(o => o.OwnerId)
        .FirstOrDefaultAsync();

        if(ownerID <= 0){
            //create owner table record
            return StatusCode(409, "Boo");
        }

        var treatments = await _context.Treatment
        .Where(treatment => treatment.OwnerId == ownerID)
        .ToListAsync();

        return Ok(treatments);

    }


    [HttpGet]
    [Authorize]
    [Route("procedures")]
    public async Task<ActionResult<List<Procedure>>> ViewProcedures()
    {      
        var procedures =
        await _context.Procedure   
        .ToListAsync();

        return Ok(procedures);
    }

    [HttpPost]
    [Authorize]
    [Route("create-treatment")]
    public async Task<ActionResult<Treatment>> CreateTreatment([FromBody] TreatmentReq treatmentReq){
        var sub = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        int ownerID = await _context.Owner
        .Where(o => o.UserID == sub)
        .Select(o => o.OwnerId)
        .FirstOrDefaultAsync();

        if(ownerID <= 0){
            //create owner table record
            return StatusCode(409, "boo");
        }

        var treatment = new Treatment(
            ownerID,
             treatmentReq.PetName,
             treatmentReq.ProcedureID,
             treatmentReq.Date,
             treatmentReq.Notes,
             0
            );

        await _context.Treatment.AddAsync(treatment);
        var resp = await _context.SaveChangesAsync();

        return Ok(resp);
    }


    //todo authorisation header
    [HttpPut]
    [Authorize]
    [Route("{ownerID:int}/update-details")]
    public async Task<ActionResult<Response<Owner?>>> UpdateOwnerDetails([FromBody] OwnerRequest ownerRequest){
        var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

        //todo get user information from auth header

        var owner =
        await _context.Owner
        .FindAsync(ownerID);

        Response<Owner?> resposne;

        if(owner == null){
            resposne = new Response<Owner?>(null, false, $"OwnerID: {ownerID} does not exist.");
            return StatusCode(409, resposne);
        }

        owner.Firstname = ownerRequest.Firstname;
        owner.Surname = ownerRequest.Surname;
        owner.Phone = ownerRequest.Phone;
        owner.Email = ownerRequest.Email;

        await _context.SaveChangesAsync();

        //todo auth0 details also need to be updated

        resposne = new Response<Owner?>(owner, true, "Owner Updated successfully.");

        return Ok(resposne);

    }


}
