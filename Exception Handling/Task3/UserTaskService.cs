using System;
using System.Linq;
using Task3.DoNotChange;

namespace Task3
{
    public class UserTaskService
    {
        private readonly IUserDao _userDao;

        public UserTaskService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public void AddTaskForUser(int userId, UserTask task)
        {
            const string InvalidUserIdMessage = "Invalid userId";
            const string UserNotFoundMessage = "User not found";
            const string TaskExistsMessage = "The task already exists";

            if (userId < default(int))
            {
                throw new ArgumentException(InvalidUserIdMessage);
            }

            var user = _userDao.GetUser(userId) ?? throw new InvalidOperationException(UserNotFoundMessage);
            var tasks = user.Tasks;

            Func<UserTask, bool> hasDuplicateDescription = existingTask =>
                string.Equals(task.Description, existingTask.Description, StringComparison.OrdinalIgnoreCase);

            if (tasks.Any(hasDuplicateDescription))
            {
                throw new ArgumentException(TaskExistsMessage);
            }

            tasks.Add(task);
        }
    }
}