using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Repository
{
    public class UserRepository: IUserInterface
    {
        UserDBContext dbContext;
        public UserRepository(UserDBContext context)
        {
            dbContext = context;
        }
        public async Task<bool> CreateAsync(User user)
        {
            dbContext!.Users.Add(user);
            var result=await dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user1 = await dbContext.Users.FirstOrDefaultAsync(x => x.id ==id);
            if (user1 != null)
            {
                dbContext.Users.Remove(user1);
                var result = await dbContext.SaveChangesAsync();

                return result > 0;

            }
            return false;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }
        

        public async Task<User> GetByIdAsync(int id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(e => e.id == id);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var user1 = await dbContext.Users.FirstOrDefaultAsync(x=>x.id == user.id);
            if (user1 != null)
            {
                user1.name = user.name;
                user1.email = user.email;
                var result= await dbContext.SaveChangesAsync();

                return result>0;

            }
            return false;
        }
    }
}
