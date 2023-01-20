using Microsoft.AspNetCore.Mvc;
using Zaandam.Domain.Contracts.Services;
using Zaandam.Domain.DTOs.Requests;
using Zaandam.Domain.DTOs.Responses;
using Zaandam.Domain.Enums;

namespace Zaandam.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/diff")] //TODO:JWT Autorize    
    public class DocumentDiffController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentDiffController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost("{id}/left")]
        [ProducesResponseType(typeof(ZResponse<DocumentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostDocumentToLeft([FromRoute] string id, [FromBody] DocumentRequest request) => await PostDocument(id, DocPositionEnum.Left, request.Data);

        [HttpPost("{id}/right")]
        [ProducesResponseType(typeof(ZResponse<DocumentResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostDocumentToRight([FromRoute] string id, [FromBody] DocumentRequest request) => await PostDocument(id, DocPositionEnum.Right, request.Data);

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DocumentDiffResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetDiff(string id)
        {
            var response = await _documentService.GetDiff(id);

            if (response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }

        private async Task<IActionResult> PostDocument(string id, DocPositionEnum docPosition, string data)
        {
            var response = await _documentService.Create(id, docPosition, data);

            if (response.Errors.Any())
                return BadRequest(response);

            return Created($"/{response.Data.First().Id}", response);
        }
    }
}