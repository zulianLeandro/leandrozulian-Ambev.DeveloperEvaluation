using Ambev.DeveloperEvaluation.Application.Sale.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sale.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.Common.Utilities;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;

        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateSaleResult))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse))]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PaginatedList<GetAllSalesResult>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetAllSales([FromQuery] GetAllSalesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(GetSaleByIdResult))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> GetSaleById(Guid id)
        {
            var query = new GetSaleByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> UpdateSale(Guid id, [FromBody] UpdateSaleCommand command)
        {
            command.SaleId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse))]
        public async Task<IActionResult> CancelSale(Guid id)
        {
            var command = new CancelSaleCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
