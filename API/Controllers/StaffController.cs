using API.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    owner.Phone?.Trim()
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
            var newOwner = new Owner(newID + 1, owner.Firstname, owner.Surname, owner.Phone);

            await _context.Owner.AddAsync(newOwner);

            addedOwnersSuccessfully.Add(newOwner);

            newID += 1;
        }

        _context.SaveChanges();

        response = new Response<List<Owner>>(addedOwnersSuccessfully, true, "Owners successfully Added");

        return Ok(trimmedOwnerReq);

    }

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
}