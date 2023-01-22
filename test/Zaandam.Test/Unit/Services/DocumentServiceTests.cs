using FluentAssertions;
using Moq;
using Zaandam.Domain.Contracts.Repositories;
using Zaandam.Domain.Contracts.Services;
using Zaandam.Domain.Contracts.UnitsOfWork;
using Zaandam.Domain.Enums;
using Zaandam.Domain.Models;
using Zaandam.Domain.Services;

namespace Zaandam.Test.Unit.Services;

public class DocumentServiceTests
{
    [Theory(DisplayName = "Should return error response when create a document with invalid base64 data")]
    [InlineData("invalid-base64-data")]
    [InlineData("")]
    [InlineData(null)]
    public async Task CreateDocument_InvalidBase64Data_ShouldReturnError(string base64Data)
    {
        // arrange
        var (repositoryMock, service) = DocumentContextMock();
        var key = Guid.NewGuid().ToString();
        var filePosition = DocPositionEnum.Left;

        // act
        var response = await service.Create(key, filePosition, base64Data);

        // assert
        response.Errors.Should().HaveCount(1);
        response.Errors.Should().Contain(error => error.Message.Equals("Invalid base64 file data."));
    }

    [Fact(DisplayName = "Should return error response when create a document with key greather than limit (50 chars)")]
    public async Task CreateDocument_KeyGreatherThanLimit50Chars_ShouldReturnError()
    {
        // arrange
        var (repositoryMock, service) = DocumentContextMock();
        var base64Data = "cGF5YnlyZCA9KQ==";
        var key = "invalid-keylabcdefghijklmnopqrstuvxyz123456--------";
        var filePosition = DocPositionEnum.Right;

        // act
        var response = await service.Create(key, filePosition, base64Data);

        // assert
        response.Errors.Should().HaveCount(1);
        response.Errors.Should().Contain(error => error.Message.Equals("Number of characters in field Key must be less than 50."));
    }

    [Fact(DisplayName = "Should return success response when create a valid document")]
    public async Task CreateDocument_ValidDocument_ShouldReturnSuccess()
    {
        // arrange
        var (repositoryMock, service) = DocumentContextMock();
        var base64Data = "cGF5YnlyZCA9KQ==";
        var key = Guid.NewGuid().ToString();
        var filePosition = DocPositionEnum.Left;

        // act
        var response = await service.Create(key, filePosition, base64Data);

        // assert
        response.Errors.Should().HaveCount(0);
        response.Data.Should().HaveCount(1);
        response.Data.Should().Contain(item => item.Key.Equals(key));
    }

    [Fact(DisplayName = "Should return offset diff response when get diff from documents with equals size")]
    public async Task GetDiff_DocumentsWithEqualSizeAndDiffs_ShouldReturnOffsetDiff()
    {
        // arrange
        var (repositoryMock, service) = DocumentContextMock();
        var key = Guid.NewGuid().ToString();
        var documents = new List<Document>()
        {
            new Document(key, "cGF5YnlyZCA9KQ==", DocPositionEnum.Left),
            new Document(key, "cGF5YmFyZCA9KQ==", DocPositionEnum.Right)
        };

        repositoryMock.Setup(r => r.AllByKeyAsync(key)).Returns(Task.FromResult<IEnumerable<Document>>(documents));
        
        // act
        var response = await service.GetDiff(key);

        // assert
        response.Errors.Should().HaveCount(0);
        response.Data.Should().HaveCount(1);
        response.Data.Should().Contain(item => item.EqualsSize.Equals(true));
        response.Data.Should().Contain(item => item.EqualsData.Equals(false));
        response.Data.Should().Contain(item => item.OffsetDiffs.Contains(5));
    }

    [Fact(DisplayName = "Should return size and data equals when get diff from documents with equals data")]
    public async Task GetDiff_DocumentsWithEqualData_ShouldReturnSizeAndDataEqualsTrue()
    {
        // arrange
        var (repositoryMock, service) = DocumentContextMock();
        var key = Guid.NewGuid().ToString();
        var documents = new List<Document>()
        {
            new Document(key, "cGF5YnlyZCA9KQ==", DocPositionEnum.Left),
            new Document(key, "cGF5YnlyZCA9KQ==", DocPositionEnum.Right)
        };

        repositoryMock.Setup(r => r.AllByKeyAsync(key)).Returns(Task.FromResult<IEnumerable<Document>>(documents));

        // act
        var response = await service.GetDiff(key);

        // assert
        response.Errors.Should().HaveCount(0);
        response.Data.Should().HaveCount(1);
        response.Data.Should().Contain(item => item.EqualsSize.Equals(true));
        response.Data.Should().Contain(item => item.EqualsData.Equals(true));
        response.Data.Should().Contain(item => !item.OffsetDiffs.Any());
    }

    [Fact(DisplayName = "Should return error response when get diff from invalid documents")]
    public async Task GetDiff_InvalidDocuments_ShouldReturnErrorResponse()
    {
        // arrange
        var (repositoryMock, service) = DocumentContextMock();
        var key = Guid.NewGuid().ToString();
        var documents = new List<Document>()
        {
            new Document(key, "cGF5YnlyZCA9KQ==", DocPositionEnum.Left)
        };

        repositoryMock.Setup(r => r.AllByKeyAsync(key)).Returns(Task.FromResult<IEnumerable<Document>>(documents));

        // act
        var response = await service.GetDiff(key);

        // assert
        response.Errors.Should().HaveCount(1);
        response.Errors.Should().Contain(error => error.Message.Equals("Fail to retrieve docs."));
    }

    private static (Mock<IDocumentRepository> RepositoryMock, IDocumentService Service) DocumentContextMock()
    {
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var repositoryMock = new Mock<IDocumentRepository>();

        unitOfWorkMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);

        var service = new DocumentService(unitOfWorkMock.Object, repositoryMock.Object);

        return (repositoryMock, service);
    }
}