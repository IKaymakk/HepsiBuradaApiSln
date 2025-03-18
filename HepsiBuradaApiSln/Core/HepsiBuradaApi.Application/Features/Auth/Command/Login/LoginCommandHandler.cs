using HepsiBuradaApi.Application.Bases;
using HepsiBuradaApi.Application.Features.Auth.Rules;
using HepsiBuradaApi.Application.Interfaces.AutoMapper;
using HepsiBuradaApi.Application.Interfaces.Tokens;
using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using HepsiBuradaApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Command.Login;

public class LoginCommandHandler : BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly AuthRules _authRules;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    public LoginCommandHandler(IMapper _mapper, IUnitOfWork _unitofwork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, AuthRules authRules, RoleManager<Role> roleManager, ITokenService tokenService, IConfiguration configuration) : base(_mapper, _unitofwork, httpContextAccessor)
    {
        _userManager = userManager;
        _authRules = authRules;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }
    public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByEmailAsync(request.Email);
        bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        await _authRules.EmailOrPasswordShouldNotBeInvalid(user, checkPassword);

        IList<string> roles = await _userManager.GetRolesAsync(user);

        JwtSecurityToken token = await _tokenService.CreateToken(user, roles);
        string refreshToken = _tokenService.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

        await _userManager.UpdateAsync(user);
        await _userManager.UpdateSecurityStampAsync(user);

        var _token = new JwtSecurityTokenHandler().WriteToken(token);

        await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

        return new LoginCommandResponse()
        {
            Token = _token,
            ResfreshToken = refreshToken,
            ExpireTime = token.ValidTo
        };
    }
}
