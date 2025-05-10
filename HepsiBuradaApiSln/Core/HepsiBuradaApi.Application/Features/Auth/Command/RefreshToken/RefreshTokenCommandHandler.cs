using HepsiBuradaApi.Application.Bases;
using HepsiBuradaApi.Application.Features.Auth.Exceptions;
using HepsiBuradaApi.Application.Features.Auth.Rules;
using HepsiBuradaApi.Application.Interfaces.AutoMapper;
using HepsiBuradaApi.Application.Interfaces.Tokens;
using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using HepsiBuradaApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Command.RefreshToken;

public class RefreshTokenCommandHandler : BaseHandler, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly AuthRules _authRules;
    private readonly ITokenService _tokenService;
    public RefreshTokenCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, UserManager<User> userManager, ITokenService tokenService, AuthRules authRules) : base(mapper, unitOfWork, contextAccessor)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _authRules = authRules;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        string email = principal.FindFirstValue(ClaimTypes.Email);

        User user = await _userManager.FindByEmailAsync(email);
        IList<string> roles = await _userManager.GetRolesAsync(user);

        await _authRules.RefreshTokenSohuldNotBeExpired(user.RefreshTokenExpireTime);

        JwtSecurityToken newAccessToken = await _tokenService.CreateToken(user, roles);
        string refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        await _userManager.UpdateAsync(user);

        return new RefreshTokenCommandResponse()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = refreshToken
        };
    }
}
