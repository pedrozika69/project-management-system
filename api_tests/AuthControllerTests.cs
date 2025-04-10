using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

public class AuthControllerTests
{
    
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
    private readonly Mock<SignInManager<IdentityUser>> _mockSignInManager;
    private readonly Mock<IConfiguration> _mockConfiguration;
    
    private readonly AuthController _controller;

        public AuthControllerTests()
        {
            // Mock UserManager
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Mock SignInManager
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            _mockSignInManager = new Mock<SignInManager<IdentityUser>>(
                _mockUserManager.Object,
                contextAccessorMock.Object,
                userPrincipalFactoryMock.Object,
                null, null, null, null);

            // Mock Configuration
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c["Jwt:Key"]).Returns("very_secret_key_that_is_long_enough");
            _mockConfiguration.Setup(c => c["Jwt:Issuer"]).Returns("test_issuer");
            _mockConfiguration.Setup(c => c["Jwt:Audience"]).Returns("test_audience");
            _mockConfiguration.Setup(c => c["Jwt:ExpireMinutes"]).Returns("30");

            // Create controller instance
            _controller = new AuthController(
                _mockUserManager.Object,
                _mockSignInManager.Object,
                _mockConfiguration.Object);
        }

        [Fact]
        public async Task Register_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var model = new RegisterModel { Email = "test@example.com", Password = "Password123!" };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Register_WithInvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var model = new RegisterModel { Email = "test@example.com", Password = "short" };
            var errors = new[] { new IdentityError { Description = "Password too short" } };
            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(errors));

            // Act
            var result = await _controller.Register(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(errors, badRequestResult.Value);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsToken()
        {
            // Arrange
            var model = new LoginModel { Email = "test@example.com", Password = "Password123!" };
            var user = new IdentityUser { Email = model.Email, UserName = model.Email };
            
            _mockSignInManager.Setup(x => x.PasswordSignInAsync(model.Email, model.Password, false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            _mockUserManager.Setup(x => x.FindByEmailAsync(model.Email))
                .ReturnsAsync(user);
            _mockUserManager.Setup(x => x.GetRolesAsync(user))
                .ReturnsAsync(new List<string> { "User" });

            // Act
            var result = await _controller.Login(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.True(okResult.Value.GetType().GetProperty("token") != null);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var model = new LoginModel { Email = "test@example.com", Password = "wrongpassword" };
            _mockSignInManager.Setup(x => x.PasswordSignInAsync(model.Email, model.Password, false, false))
                .ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);

            // Act
            var result = await _controller.Login(model);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        // [Fact]
        // public void GenerateJwtToken_ReturnsValidToken()
        // {
        //     // Arrange
        //     var user = new IdentityUser { Email = "test@example.com", Id = "123" };
        //     var roles = new List<string> { "User", "Admin" };
            
        //     // Act
        //     var token = _controller.GenerateJwtToken(user, roles);

        //     // Assert
        //     Assert.NotEmpty(token);
            
        //     // Additional validation of token structure
        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     var jwtToken = tokenHandler.ReadJwtToken(token);
            
        //     Assert.Equal(user.Email, jwtToken.Subject);
        //     Assert.Equal(_mockConfiguration.Object["Jwt:Issuer"], jwtToken.Issuer);
        //     Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == "User");
        //     Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        // }
}
