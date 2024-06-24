using Dafaatir.Contracts.Entity;
using Dafaatir.Contracts.Repository;


namespace Dafaatir.Modules.Users.Contracts.Repositories;
public interface IUserRepository : IGenericRepository<UserEntity, UserId>
{



}
