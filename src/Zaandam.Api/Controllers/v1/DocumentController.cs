using Microsoft.AspNetCore.Mvc;
using Zaandam.Domain.Contracts.Services;

namespace Zaandam.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    //TODO:JWT Autorize
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        //[HttpPost, Route("{id:objectid}/remount", Name = nameof(PostCreateNewTruckWithOpenOrders))]
        //[ProducesResponseType(typeof(TruckIdResponse), StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(CustomErrorResponse), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> PostCreateNewTruckWithOpenOrders([FromRoute] string id)
        //{
        //    var newTruckId = await _truckService.RemountTruckWithOpenOrders(ObjectId.Parse(id));

        //    return string.IsNullOrEmpty(newTruckId)
        //        ? NoContent()
        //        : CreatedAtAction(nameof(GetById), new { id = newTruckId }, new TruckIdResponse(newTruckId));
        //}
    }
}