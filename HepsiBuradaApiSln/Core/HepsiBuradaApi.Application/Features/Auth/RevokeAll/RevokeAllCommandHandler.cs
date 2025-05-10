using HepsiBuradaApi.Application.Bases;
using HepsiBuradaApi.Application.Features.Auth.Rules;
using HepsiBuradaApi.Application.Interfaces.AutoMapper;
using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using HepsiBuradaApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.RevokeAll;

public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommandRequest,Unit>
{
    private readonly UserManager<User> _userManager;
    private readonly AuthRules _authRules;
    public RevokeAllCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor, UserManager<User> userManager, AuthRules authRules) : base(mapper, unitOfWork, contextAccessor)
    {
        _userManager = userManager;
        _authRules = authRules;
    }

    public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
    {
        List<User> users = await _userManager.Users.ToListAsync(cancellationToken);
        
        foreach(User user in users)
        {
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        return Unit.Value;
    }
}
