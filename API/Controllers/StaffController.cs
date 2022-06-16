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
[Route("api/staff")]
public class StaffController : ControllerBase
{
    private readonly PetContext _context;

    public StaffController(PetContext context)
    {
        _context = context;
    }

    //[Authorize("write:admin")]
    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<Response<Owner?>>> CreateOwner([FromBody] OwnerSignUpReq ownerSignUpReq)
    {
        try
        {
            ownerSignUpReq.email = ownerSignUpReq.email != null ? ownerSignUpReq.email.Trim() : ownerSignUpReq.email;
            ownerSignUpReq.family_name = ownerSignUpReq.family_name != null ? ownerSignUpReq.family_name.Trim() : ownerSignUpReq.family_name;
            ownerSignUpReq.given_name = ownerSignUpReq.given_name != null ? ownerSignUpReq.given_name.Trim() : ownerSignUpReq.given_name;
            ownerSignUpReq.phone = ownerSignUpReq.phone != null ? ownerSignUpReq.phone.Trim() : ownerSignUpReq.phone;

            Response<Owner?> response;

            //check owner email is not already in the database
            var isOwner = await _context.Owner
            .Where(o => o.Email == ownerSignUpReq.email)
            .FirstOrDefaultAsync();

            
            if ( isOwner != null ){
                response = new Response<Owner?>(isOwner, false, "Owner already Exists!");
                return StatusCode(409, response);
            }

            //get token for management API
            //todo environment variables
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
            newUser.Password = ownerSignUpReq.password;
            newUser.FirstName = ownerSignUpReq.given_name;
            newUser.LastName = ownerSignUpReq.family_name;

            var resp = await clientManagement
            .Users
            .CreateAsync(newUser);

            var userID = resp.UserId;

            var newOwner =
            new Owner(
            userID,
            ownerSignUpReq.family_name,
            ownerSignUpReq.given_name,
            ownerSignUpReq.phone,
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

    //todo auth header
    //todo roles for admin
    [HttpPost]
    [Route("create-treatment")]
    public async Task<ActionResult<Response<Treatment?>>> CreateTreatment([FromBody] Treatment treatmentReq)
    {
        var isPet = await _context.Pet
        .Where(p => p.OwnerId == treatmentReq.OwnerId)
        .Where(p => p.PetName == treatmentReq.PetName)
        .FirstOrDefaultAsync();

        Response<Treatment?> response;

        if (isPet == null){
            response = new Response<Treatment?>(null, false, "Pet and/or owner do not exist");
            return StatusCode(409, response);
        }

        var isProcedure = await _context.view_procedure
        .Where(p => p.ProcedureID == treatmentReq.ProcedureID)
        .FirstOrDefaultAsync();

        if(isProcedure == null){
            response = new Response<Treatment?>(null, false, "Procedure does not exist");
            return StatusCode(409, response);
        }

        await _context.AddAsync(treatmentReq);
        await _context.SaveChangesAsync();

        response = new Response<Treatment?>(treatmentReq, true, "Treatment has been added");

        return Ok(response);
    }

    //todo auth header
    //todo role admin scope
    [HttpPut]
    [Route("{treatmentID:int}/treatment-paid")]
    public async Task<ActionResult<Response<Treatment?>>> TreatmentPaid(){

        Response<Treatment?> response;

        var treatmentID = Convert.ToInt32(RouteData.Values["treatmentID"]);

        var treatment = await _context.Treatment
        .Where(t => t.ID == treatmentID)
        .FirstOrDefaultAsync();


        if (treatment == null){

            response = new Response<Treatment?>(null, false, "Treatment not found");
            return response;
        }

        var procedure = await _context.view_procedure
        .Where(p => p.ProcedureID == treatment!.ProcedureID)
        .FirstOrDefaultAsync();
        

        if(treatment!.Payment == procedure!.Price){
            response = new Response<Treatment?>(treatment, false, "Treatment has already been paid for.");
            return response;

        }else{
            var difference = procedure.Price - treatment.Payment;

            treatment.Payment = treatment.Payment + difference;

            await _context.SaveChangesAsync();
        }

        response = new Response<Treatment?>(treatment, true, "Treatment has been marked as paid");
        return response;
    }



    //todo create new procedures
    //todo  auth header
    //todo role admin scope

    //todo CICD pipeline with github and azure
}