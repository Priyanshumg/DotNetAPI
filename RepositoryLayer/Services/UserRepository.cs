using RepositoryLayer.Context;
using RepositoryLayer.Enitity;
using CommonLayer.RequestModel;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserInterface
    {
        private readonly UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public UserEntity UserRegistration(RegisterModel model)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = model.FirstName;
            userEntity.LastName = model.LastName;
            userEntity.UserName = model.UserName;
            userEntity.UserEmail = model.UserEmail;
            userEntity.UserPassword = model.UserPassword;
            context.UserTable.Add(userEntity);
            context.SaveChanges();
            return userEntity;
        }
    }
}
