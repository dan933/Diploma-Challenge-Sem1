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


    [HttpGet]
    [Route("get-pets/{userId:int}")]
    public async Task<ActionResult<Response<List<Pet>>>> getPets()
    {
        var userId = Convert.ToInt32(RouteData.Values["userId"]);

        var pets = await _context.PET
        .Where(p => p.OwnerID == userId)
        .ToListAsync();

        var response = new Response<List<Pet>>(pets, true, "Pets Retrieved");

        return Ok(response);
    }

    [HttpPost]
    [Route("{userId:int}/add-pet")]
    public async Task<ActionResult<Response<Pet?>>> addPet([FromBody] AddPetReq petReq){
         var userId = Convert.ToInt32(RouteData.Values["userId"]);
        var IsPet = await _context.PET
        .Where(p => p.OwnerID == userId)
        .Where(p => p.PetName == petReq.PetName)        
        .FirstOrDefaultAsync();

        Response<Pet?> response;

        if(IsPet != null){
            response = new Response<Pet?>(IsPet, false, "A Pet with this name already exists");
            return StatusCode(409, response);
        }

        var newPet = new Pet();

        newPet.PetName = petReq.PetName;
        newPet.OwnerID = petReq.OwnerID;
        newPet.Type = petReq.Type;

        await _context.PET.AddAsync(newPet);

        await _context.SaveChangesAsync();

        response = new Response<Pet?>(newPet, true, "Pet successfully created");

        return Ok(response);

    }
}
