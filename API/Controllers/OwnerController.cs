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

    [HttpGet]
    [Route("get-all")]
    public async Task<ActionResult<List<Owner>>> getOwners()
    {
        try
        {
            List<Owner> Owners = await _context.Owner.ToListAsync();

            return Ok(Owners);
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
