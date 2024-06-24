/*

using Dafaatir.Contracts.Entity;
namespace Dafaatir.Modules.Users.Contracts.Entites;



public class UserEntity : NormalEntity<UserId>, INormalEntity<UserId>
{

    public string Name { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }

    protected UserEntity(UserId id, string name, string password, string email) : base(id)
    {
        Id = id;
        Name = name;
        Password = password;
        Email = email;
    }

    // Static factory method to create a UserEntity without an ID
    public static UserEntity Create(string name, string password, string email)
    {
        // You can add validation logic here if needed before creating the entity
        return new UserEntity(UserId.CreateEmpty(), name, password, email);
    }
}

*/