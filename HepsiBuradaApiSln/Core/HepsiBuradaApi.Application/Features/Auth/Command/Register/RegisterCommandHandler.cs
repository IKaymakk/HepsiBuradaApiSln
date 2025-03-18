using HepsiBuradaApi.Application.Bases;
using HepsiBuradaApi.Application.Features.Auth.Rules;
using HepsiBuradaApi.Application.Interfaces.AutoMapper;
using HepsiBuradaApi.Application.Interfaces.UnitOfWorks;
using HepsiBuradaApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Application.Features.Auth.Command.Register;

public class RegisterCommandHandler : BaseHandler, IRequestHandler<RegisterCommandRequest, Unit>
{
    private readonly AuthRules authRules;
    private readonly RoleManager<Role> roleManager;
    private readonly UserManager<User> userManager;

    public RegisterCommandHandler(AuthRules authRules, RoleManager<Role> roleManager, UserManager<User> userManager, IMapper _mapper, IUnitOfWork _unitofwork, IHttpContextAccessor httpContextAccessor) : base(_mapper, _unitofwork, httpContextAccessor)
    {
        authRules = authRules;
        this.roleManager = roleManager;
        this.userManager = userManager;
    }
    public async Task<Unit> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
    {
        //await authRules.UserShouldNotBeExist(await userManager.FindByEmailAsync(request.Email));
        //User user = _mapper.Map<User, RegisterCommandRequest>(request);
        User user = new();
        user.FullName = request.FullName;
        user.UserName = request.Email;
        user.Email = request.Email;
        user.PasswordHash = request.Password;
        user.SecurityStamp = Guid.NewGuid().ToString();

        IdentityResult result = await userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            if (!await roleManager.RoleExistsAsync("user"))
                await roleManager.CreateAsync(new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "user",
                    NormalizedName = "USER",
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });
            await userManager.AddToRoleAsync(user, "user");
        }
        return Unit.Value;
    }
}
