using API.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/Owner")]
public class OwnerController : ControllerBase
{
    private readonly PetContext _context;

    public OwnerController(PetContext context){
        _context = context;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<Response<Owner?>>> CreateOwner([FromBody] OwnerRequest ownerRequest)
    {
        try
        {
            //gets the largest ID in the DB
            var lastID = await _context.Owner
            .OrderByDescending(owner => owner.OwnerId)
            .Select(owner => owner.OwnerId)
            .FirstAsync();

            int ownerID = 0;

            if(lastID <= 0){
                ownerID = 1;
            }else{
                ownerID += lastID + 1;
            }

            var newOwner = new Owner(
                ownerID, ownerRequest.Surname,
                ownerRequest.Firstname, ownerRequest.Phone);

            

            await _context.Owner.AddAsync(newOwner);
            await _context.SaveChangesAsync();

            var response = new Response<Owner>(newOwner, true, "Owner successfully created");
            return Ok();
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
