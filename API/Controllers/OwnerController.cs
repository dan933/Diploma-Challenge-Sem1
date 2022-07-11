using API.Models;
using API.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class OwnerController : ControllerBase
{

    private readonly diplomachallengeContext _context;

    public OwnerController(diplomachallengeContext context)
    {
        _context = context;
    }

    [HttpDelete]
    [Route("delete-order/{Id:int}")]
    public async Task<ActionResult<OrderReq>> deleteOrder()
    {
        var OrderId = Convert.ToInt32(RouteData.Values["Id"]);

        var order = await _context.Orders
        .Where(o => o.Id == OrderId)
        .FirstOrDefaultAsync();

        _context.Orders.Remove(order!);

        _context.SaveChanges();

        return Ok(order);

    }


    [HttpPut]
    [Route("update-order/{Id:int}")]
    public async Task<ActionResult<OrderReq>> updateOrder([FromBody] OrderReq orderReq)
    {
        var OrderId = Convert.ToInt32(RouteData.Values["Id"]);

        var order = await _context.Orders
        .Where(o => o.Id == OrderId)
        .FirstOrDefaultAsync();

        if (order != null)
        {
            order.Quantity = orderReq.Quantity;
            order.OrderDate = orderReq.OrderDate;
            order.ShipDate = orderReq.ShipDate;
            order.ProductId = orderReq.ProductId;
            order.ShipMode = orderReq.ShipMode;

            await _context.SaveChangesAsync();

            return Ok(order);
        }

        return Ok(null);


    }

    [HttpGet]
    [Route("get-order/{Id:int}")]
    public async Task<ActionResult<Order>> getOrder()
    {

        var OrderId = Convert.ToInt32(RouteData.Values["Id"]);

        var order = await _context.Orders
        .Where(o => o.Id == OrderId)
        .FirstOrDefaultAsync();

        return Ok(order);
    }

    [HttpGet]
    [Route("get-products")]
    public async Task<ActionResult<Customer>> getProducts()
    {
        var products = await _context.Products.ToListAsync();

        return Ok(products);
    }

    [HttpGet]
    [Route("get-shipping")]
    public async Task<ActionResult<Customer>> getShipping()
    {
        var shippings = await _context.Shippings.ToListAsync();

        return Ok(shippings);
    }

    [HttpGet]
    [Route("get-customers")]
    public async Task<ActionResult<Customer>> getCustomers()
    {
        var customers = await _context.Customers.ToListAsync();

        return Ok(customers);
    }

    [HttpGet]
    [Route("get-orders/{Id:int}")]
    public async Task<ActionResult<Order>> getOrders()
    {

        var userId = Convert.ToInt32(RouteData.Values["Id"]);

        var orders = await _context.view_orders
        .Where(o => o.CustId == userId)
        .ToListAsync();

        return Ok(orders);
    }

    [HttpPost]
    [Route("{Id:int}/create-order")]
    public async Task<ActionResult<Order>> createOrders([FromBody] OrderReq orderReq)
    {
        var userId = Convert.ToInt32(RouteData.Values["Id"]);

        var order = new Order();

        order.CustomerId = userId;
        order.ProductId = orderReq.ProductId;
        order.OrderDate = orderReq.OrderDate;
        order.Quantity = orderReq.Quantity;
        order.ShipDate = orderReq.ShipDate;
        order.ShipMode = orderReq.ShipMode;

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        return Ok(order);
    }
}