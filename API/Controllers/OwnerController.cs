using API.models;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnerController : ControllerBase
{

    private readonly PetContext _context;

    public OwnerController(PetContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("get-owners")]
    public async Task<ActionResult<List<Owner>>> getOwners(){
        var owners = await _context.OWNER
        .ToListAsync();

        return Ok(owners);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<Response<Owner?>>> login([FromBody] CreateOwnerReq ownerReq) {
        //check owner doesn't already exist
        var IsOwner = await _context.OWNER
        .Where(o => o.Phone == ownerReq.Phone)
        .FirstOrDefaultAsync();

        Response<Owner?> response;

        if(IsOwner != null){
            response = new Response<Owner?>(IsOwner, true, "Owner Retrieved");

            return Ok(response);
        }

        var newOwner = new Owner();

        newOwner.Surname = ownerReq.Surname;
        newOwner.FirstName = ownerReq.FirstName;
        newOwner.Phone = ownerReq.Phone;

        await _context.OWNER.AddAsync(newOwner);

        await _context.SaveChangesAsync();

        var owner = await _context.OWNER
        .Where(o => o.Phone == newOwner.Phone)
        .FirstOrDefaultAsync();

        response = new Response<Owner?>(newOwner, true, "Owner successfully created");

        return Ok(response);

    }

}
