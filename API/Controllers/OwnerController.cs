using System.Text.RegularExpressions;
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

    public OwnerController(PetContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    [Authorize("read:message")]    
    [Route("view-pets")]
    public async Task<ActionResult<List<Pet>>> ViewPets(){
        var sub = HttpContext?.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        var pets = await _context.Pet
        .ToListAsync();

        return Ok(pets);

    }

    //todo login endpoint that gives owner a token

    //todo authorisation header
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


    //todo authorisation header
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

    //todo authorisation header
    [HttpPut]
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
