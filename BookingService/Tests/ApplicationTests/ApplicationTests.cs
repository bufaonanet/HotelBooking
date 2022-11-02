using Application.Guest;
using Application.Guest.DTOs;
using Application.Guest.Ports;
using Application.Guest.Requests;
using Domain.Ports;
using Moq;

namespace Application;

public class ApplicationTests
{
    private GuestManager _sut;

    [Fact]
    public async Task HappyPath()
    {
        //Arrange
        var guestDto = new GuestDTO
        {
            Name = "Fulano",
            Surname = "Ciclano",
            Email = "abc@gmail.com",
            IdNumber = "abca",
            IdTypeCode = 1
        };

        int expectedId = 222;

        var request = new CreateGuestRequest()
        {
            Data = guestDto,
        };

        var fakeRepo = new Mock<IGuestRepository>();
        fakeRepo.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Guest>()))
            .Returns(Task.FromResult(expectedId));

        _sut = new GuestManager(fakeRepo.Object);

        //Act
        var res = await _sut.CreateGuest(request);

        //Assert
        Assert.NotNull(res);
        Assert.True(res.Success);
        Assert.Equal(expectedId, res.Data.Id);
        Assert.Equal(guestDto.Name, res.Data.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("ab")]
    [InlineData("abc")]
    public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
    {
        //Arrange
        var guestDto = new GuestDTO
        {
            Name = "Fulano",
            Surname = "Ciclano",
            Email = "abc@gmail.com",
            IdNumber = docNumber,
            IdTypeCode = 1
        };

        var request = new CreateGuestRequest()
        {
            Data = guestDto,
        };

        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo.Setup(x => x.CreateAsync(
            It.IsAny<Domain.Entities.Guest>())).Returns(Task.FromResult(222));

        _sut = new GuestManager(fakeRepo.Object);

        //Act
        var res = await _sut.CreateGuest(request);

        //Assert
        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.Equal(ErrorCodes.INVALID_PERSON_ID, res.ErrorCode);
        Assert.Equal("The ID passed is not valid", res.Message);
    }

    [Theory]
    [InlineData("", "surnametest", "asdf@gmail.com")]
    [InlineData(null, "surnametest", "asdf@gmail.com")]
    [InlineData("Fulano", "", "asdf@gmail.com")]
    [InlineData("Fulano", null, "asdf@gmail.com")]
    [InlineData("Fulano", "surnametest", "")]
    [InlineData("Fulano", "surnametest", null)]
    public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(string name, string surname, string email)
    {
        var guestDto = new GuestDTO
        {
            Name = name,
            Surname = surname,
            Email = email,
            IdNumber = "abcd",
            IdTypeCode = 1
        };

        var request = new CreateGuestRequest()
        {
            Data = guestDto,
        };

        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo
            .Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Guest>()))
            .Returns(Task.FromResult(222));

        _sut = new GuestManager(fakeRepo.Object);

        var res = await _sut.CreateGuest(request);

        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.Equal(ErrorCodes.MISSING_REQUIRED_INFORMATION, res.ErrorCode);
        Assert.Equal("Missing required information passed", res.Message);
    }

    [Fact]
    public async Task Should_Return_GuestNotFound_When_GuestDoesntExist()
    {
        var fakeRepo = new Mock<IGuestRepository>();

        fakeRepo
            .Setup(x => x.GetAsync(333))
            .Returns(Task.FromResult<Domain.Entities.Guest>(null));

        _sut = new GuestManager(fakeRepo.Object);

        var res = await _sut.GetGuest(333);

        Assert.NotNull(res);
        Assert.False(res.Success);
        Assert.Equal(ErrorCodes.GUEST_NOT_FOUND, res.ErrorCode);
        Assert.Equal("No Guest record was found with the given Id", res.Message);
    }

    [Fact]
    public async Task Should_Return_Guest_Success()
    {
        var fakeRepo = new Mock<IGuestRepository>();

        var fakeGuest = new Domain.Entities.Guest
        {
            Id = 333,
            Name = "Test",
            DocumentId = new Domain.ValueObjects.PersonId
            {
                DocumentType = Domain.Enums.DocumentType.DriveLicence,
                IdNumber = "123"
            }
        };

        fakeRepo
            .Setup(x => x.GetAsync(333))
            .Returns(Task.FromResult(fakeGuest));

        _sut = new GuestManager(fakeRepo.Object);

        var res = await _sut.GetGuest(333);

        Assert.NotNull(res);
        Assert.True(res.Success);
        Assert.Equal(res.Data.Id, fakeGuest.Id);
        Assert.Equal(res.Data.Name, fakeGuest.Name);
    }

}