using API.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


    //todo login to get token 
    //or rethink how this works
    [HttpGet]
    [Route("get-token")]
    public async Task<ActionResult<Response<string?>>>GetToken(){
        
        var client = new RestClient("https://dev-tt6-hw09.us.auth0.com");
        var request = new RestRequest("/oauth/token",Method.Post);
        request.AddHeader("content-type", "application/json");
        request.AddParameter("application/json", "{\"client_id\":\"taBuUExBpBMEMagUBgo0omd9W8kli7eC\",\"client_secret\":\"dk3zpnzWxeDfdVlmUOs4Wgl3dcw4fCjG6EiO0tmCcjsoWBhRs5nDPkHponp_A-s7\",\"audience\":\"https://diploma-challenge-sem-1.com.au\",\"grant_type\":\"client_credentials\"}", ParameterType.RequestBody);
        RestResponse response = client.Execute(request);

        return Ok(response.Content);
    }

    [HttpPost]
    [Route("create-accounts")]
    public async Task<ActionResult<Response<Owner?>>> CreateOwners([FromBody] List<OwnerRequest> ownerRequests)
    {

        var trimmedOwnerReq = new List<OwnerRequest>();

        //remove trailing white space
        ownerRequests.ForEach((owner) =>
        {
        trimmedOwnerReq.Add(
            new OwnerRequest(
                owner.Firstname?.Trim(),
                owner.Surname?.Trim(),
                owner.Phone?.Trim(),
                owner.Email?.Trim()
                )
            );

        });

        var ownersPhoneNumbers = new List<string>();



        trimmedOwnerReq.ForEach(owner =>
        {
            if (owner.Phone != null)
            {
                ownersPhoneNumbers.Add(owner.Phone);
            }

        });


        var addedOwnersSuccessfully = new List<Owner>();

        var response = new Response<List<Owner>>();

        var isOwner = await _context.Owner
            .Where(o => ownersPhoneNumbers.Contains(o.Phone))
            .ToListAsync(); 

        if (isOwner.Count() != 0){
        
            response = new Response<List<Owner>>(isOwner, false, "Some Owners already exist");

            return StatusCode(409, response);
        }

            var newID = await _context.Owner
            .OrderByDescending(o => o.OwnerId)
            .Select(o => o.OwnerId)
            .FirstOrDefaultAsync();

        newID = newID == 0 ? 1 : newID;

        foreach (var owner in trimmedOwnerReq)
        {
            var newOwner = new Owner(newID + 1, owner.Firstname, owner.Surname, owner.Phone, owner.Email);

            await _context.Owner.AddAsync(newOwner);

            addedOwnersSuccessfully.Add(newOwner);

            newID += 1;
        }

        _context.SaveChanges();

        response = new Response<List<Owner>>(addedOwnersSuccessfully, true, "Owners successfully Added");

        return Ok(trimmedOwnerReq);

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