using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.Security;

namespace CoursePractice.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string username)
        {
            string[] roles = new string[] { };
            using (MonitoringSystemDBEntities db = new MonitoringSystemDBEntities())
            {
                // Получаем пользователя
                Учётные_записи user = db.Учётные_записи.FirstOrDefault(u => u.логин == username);
                if (user != null)
                {
                    Тип_учётной_записи userRole = db.Тип_учётной_записи.Find(user.тип_учётной_записи);
                    if (userRole != null)
                    {
                        roles = new string[] { userRole.тип_пользователя };
                    }
                }
                return roles;
            }
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;

            using (MonitoringSystemDBEntities db = new MonitoringSystemDBEntities())
            {
                // Получаем пользователя
                Учётные_записи user = db.Учётные_записи.FirstOrDefault(u => u.логин == username);

                if (user != null)
                {
                    Тип_учётной_записи userRole = db.Тип_учётной_записи.Find(user.тип_учётной_записи);
                    if (userRole != null && userRole.тип_пользователя == roleName)
                        outputResult = true;
                }
                return outputResult;
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}