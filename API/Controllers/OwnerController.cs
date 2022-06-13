using API.models;
using Auth0.ManagementApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace API.Controllers;

[ApiController]
[Route("api/owner")]
public class OwnerController : ControllerBase
{
    private readonly PetContext _context;

    public OwnerController(PetContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<ActionResult<Response<Owner?>>> CreateOwner([FromBody] OwnerSignUpReq ownerSignUpReq)
    {
        try
        {
            ownerSignUpReq.email = ownerSignUpReq.email != null ? ownerSignUpReq.email.Trim() : ownerSignUpReq.email;

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
            var client = new RestClient("https://dev-tt6-hw09.us.auth0.com");
            var request = new RestRequest("/oauth/token", Method.Post);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\"client_id\":\"L6nnmAddJrNPKicBm97WFD8I6flvCgiy\",\"client_secret\":\"2xA-2uwAJ7Iye8yV1OZIg_jBTK0MKJtLyo-BNV6FPI_KD3QaemxHGYJViRnKCVvD\",\"audience\":\"https://dev-tt6-hw09.us.auth0.com/api/v2/\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
            RestResponse tokenResponse = client.Execute(request);

            //create a user


            return Ok(tokenResponse.Content);            
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    [HttpGet]
    // [Authorize("read:user")]
    [Authorize]
    [Route("{ownerID:int}/view-pets")]
    public async Task<ActionResult<List<Pet>>> ViewPets(){

        var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

        var pets =
        await _context.Pet
        .Where(pet => pet.OwnerId == ownerID)
        .ToListAsync();

        return Ok(pets);

    }

    [HttpGet]
    [Route("{ownerID:int}/view-treatments")]
    public async Task<ActionResult<List<Treatment>>> ViewTreatments()
    {

        var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

        var treatments = await _context.Treatment
        .Where(treatment => treatment.OwnerId == ownerID)
        .ToListAsync();

        return Ok(treatments);

    }


    [HttpGet]
    [Route("{ownerID:int}/view-procedures")]
    public async Task<ActionResult<List<ProcedureView>>> ViewProcedures()
    {
        var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

        var procedures =
        await _context.view_procedure
        .Where(procedure => procedure.OwnerId == ownerID)
        .ToListAsync();

        return Ok(procedures);
    }

    [HttpPut]
    [Route("{ownerID:int}/update-details")]
    public async Task<ActionResult<Response<Owner?>>> UpdateOwnerDetails([FromBody] OwnerRequest ownerRequest){
        var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

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

        resposne = new Response<Owner?>(owner, true, "Owner Updated successfully.");

        return Ok(resposne);

    }
}
