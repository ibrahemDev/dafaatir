
//using Dafaatir.Modules.Users.App.Features.User.Models;
using MediatR;


using Dafaatir.Modules.Users.Contracts.Queries;
using Dafaatir.Modules.Users.Contracts.Models;


namespace Dafaatir.Modules.Users.App.Features.User.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserModel>>
{




    public GetAllUsersQueryHandler()
    {


    }

    public async Task<IEnumerable<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {

        await Task.Delay(500);
        var a = new UserModel
        {
            Name = "QQQQQQQQQQQQQQQQQQQQQQQQQQQQQ"
        };
        return [a];

    }
}
