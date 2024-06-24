using Dafaatir.Modules.Users.Contracts.Models;
using MediatR;


namespace Dafaatir.Modules.Users.Contracts.Queries;



public record GetAllUsersQuery() : IRequest<IEnumerable<UserModel>>;
