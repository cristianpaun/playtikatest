using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.Orders;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Unit>> AddOrder([FromBody] AddOrderDTO order) => 
            await _mediator.Send(new AddNewOrder.Command() { CartId = order.CartId, ProductsList = order.Products });
        
        // GET api/values/5
        [HttpGet("{cartId}")]
        public async Task<ActionResult<OrderDTO>> Get(string cartId) =>  
            await _mediator.Send(new Details.Query() { CartId = cartId });

    }
}