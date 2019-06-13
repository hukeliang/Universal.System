using Universal.System.Entity.Model;

namespace Universal.System.DataAccess.Interface
{
    public interface IUserDataAccess
    {
        UserModel QueryUser(string userName, string password);
        UserModel QueryUser();
        dynamic QueryUser(string f);
    }
}
