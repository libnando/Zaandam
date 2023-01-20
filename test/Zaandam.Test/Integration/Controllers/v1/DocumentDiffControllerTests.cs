using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Zaandam.Domain.DTOs.Requests;
using Zaandam.Test.Integration.Common;

namespace Zaandam.Test.Integration.Controllers.v1;

public class DocumentDiffControllerTests : IClassFixture<ZWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ZWebApplicationFactory<Program>
        _factory;

    public DocumentDiffControllerTests(ZWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact(DisplayName = "Should return the status 'bad request' when create document with empty data")]
    public async Task PostDocument_EmptyData_ShouldReturnStatusBadRequest()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var command = new DocumentRequest()
        {
            Data = String.Empty,
        };

        //Act
        var response = await _client.PostAsJsonAsync($"/v1/diff/{id}/left", command);

        //Assert
        response.IsSuccessStatusCode.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact(DisplayName = "Should return the status 'created' when create document with valid data")]
    public async Task PostDocument_ValidData_ShouldReturnStatusCreated()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var command = new DocumentRequest()
        {
            Data = "cGF5YnlyZA==",
        };

        //Act
        var response = await _client.PostAsJsonAsync($"/v1/diff/{id}/left", command);

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact(DisplayName = "Should return the status 'ok' when get diff from valid documents")]
    public async Task GetDiff_ValidDocuments_ShouldReturnStatusOk()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var command = new DocumentRequest()
        {
            Data = "cGF5YnlyZA==",
        };

        await _client.PostAsJsonAsync($"/v1/diff/{id}/left", command);
        await _client.PostAsJsonAsync($"/v1/diff/{id}/right", command);

        //Act
        var response = await _client.GetAsync($"/v1/diff/{id}");

        //Assert
        response.IsSuccessStatusCode.Should().BeTrue();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

}