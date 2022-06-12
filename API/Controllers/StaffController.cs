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
        // try
        // {
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
}

// foreach (var owner in trimmedOwnerReq)
// {


//     if (isOwner.Count() == 0)
//     {
//         var newID = await _context.Owner
//         .OrderByDescending(o => o.OwnerId)
//         .Select(o => o.OwnerId)
//         .FirstOrDefaultAsync();

//         var newOwner = new Owner(newID + 1, owner.Firstname, owner.Surname, owner.Phone);

//         await _context.Owner.AddAsync(newOwner);
//         addedOwnersSuccessfully.Add(newOwner);
//     }else{
//         response = new Response<List<Owner>>(isOwner, false, "Some Owners already exist");

//         return StatusCode(409, response);
//     }
//}

// _context.SaveChanges();

// response = new Response<List<Owner>>(addedOwnersSuccessfully, true, "Owners successfully Added");

// return Ok(trimmedOwnerReq);



// //check to see if owner already exists with the same phone number
// var isOwner = await _context.Owner
// .Where(owner => owner.Phone == ownerRequest.Phone)
// .FirstOrDefaultAsync();

// Response<Owner?> response;

// // Owner already exists
// if (isOwner?.OwnerId != null)
// {
//     response = new Response<Owner?>(isOwner, false, "Owner with this phone number already exists");
//     return StatusCode(409, response);
// }


// //gets the largest ID in the DB
// var lastID = await _context.Owner
// .OrderByDescending(owner => owner.OwnerId)
// .Select(owner => owner.OwnerId)
// .FirstOrDefaultAsync();

// int ownerID = 0;

// if (lastID <= 0)
// {
//     ownerID = 1;
// }
// else
// {
//     //Increases ID by one                
//     ownerID += lastID + 1;
// }

// var newOwner =
// new Owner
// (
//     ownerID, ownerRequest.Surname,
//     ownerRequest.Firstname, ownerRequest.Phone
// );



// await _context.Owner.AddAsync(newOwner);
// await _context.SaveChangesAsync();

// response = new Response<Owner?>(newOwner, true, "Owner successfully created");
// return Ok(response);

// catch (System.Exception)
// {

//     throw;
// }


// [HttpGet]
// [Route("{ownerID:int}/view-pets")]
// public async Task<ActionResult<List<Pet>>> ViewPets(){

//     var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

//     var pets =
//     await _context.Pet
//     .Where(pet => pet.OwnerId == ownerID)
//     .ToListAsync();

//     return Ok(pets);

// }

// [HttpGet]
// [Route("{ownerID:int}/view-treatments")]
// public async Task<ActionResult<List<Treatment>>> ViewTreatments()
// {

//     var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

//     var treatments = await _context.Treatment
//     .Where(treatment => treatment.OwnerId == ownerID)
//     .ToListAsync();

//     return Ok(treatments);

// }


// [HttpGet]
// [Route("{ownerID:int}/view-procedures")]
// public async Task<ActionResult<List<ProcedureView>>> ViewProcedures()
// {
//     var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

//     var procedures =
//     await _context.view_procedure
//     .Where(procedure => procedure.OwnerId == ownerID)
//     .ToListAsync();

//     return Ok(procedures);
// }

// [HttpPut]
// [Route("{ownerID:int}/update-details")]
// public async Task<ActionResult<Response<Owner?>>> UpdateOwnerDetails([FromBody] OwnerRequest ownerRequest){
//     var ownerID = Convert.ToInt32(RouteData.Values["ownerID"]);

//     var owner =
//     await _context.Owner
//     .FindAsync(ownerID);

//     Response<Owner?> resposne;

//     if(owner == null){
//         resposne = new Response<Owner?>(null, false, $"OwnerID: {ownerID} does not exist.");
//         return StatusCode(409, resposne);
//     }

//     owner.Firstname = ownerRequest.Firstname;
//     owner.Surname = ownerRequest.Surname;
//     owner.Phone = ownerRequest.Phone;

//     await _context.SaveChangesAsync();

//     resposne = new Response<Owner?>(owner, true, "Owner Updated successfully.");

//     return Ok(resposne);

// }
