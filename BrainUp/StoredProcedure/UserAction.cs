using BrainUp.Data;
using BrainUp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public static class UserAction
    {
        public static int Add(User user, BrainUpBdContext context, string role = "Student")
        {
            int userId = -1;

            if (user == null || user.Email == null || user.PasswordHash == null)
            {
                return userId;
            }

            try
            {
                var parameters = new[] {
                new SqlParameter("@email", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = user.Email
                    },
                    new SqlParameter("@passwordHash", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = user.PasswordHash
                    },
                    new SqlParameter("@insertedId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    }
                };

                if (role == "Student")
                {
                    context.Database.ExecuteSqlRaw(
                    "exec [dbo].[CreateStudent] @email,@passwordHash,@insertedId out", parameters:
                    parameters);
                }

                if (role == "Teacher")
                {
                    context.Database.ExecuteSqlRaw(
                    "exec [dbo].[CreateTeacher] @email,@passwordHash,@insertedId out", parameters:
                    parameters);
                }
                if (role == "Admin")
                {
                    context.Database.ExecuteSqlRaw(
                    "exec [dbo].[CreateAdmin] @email,@passwordHash,@insertedId out", parameters:
                    parameters);
                }


                userId = Convert.ToInt32(parameters[2].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return userId;
        }

        public static int Fill(User user, BrainUpBdContext context)
        {
            int userId = -1;

            if (user == null)
            {
                return userId;
            }

            try
            {
                var parameters = new[] {
                new SqlParameter("@userId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.InputOutput,
                      Value = user.Email
                    },
                    new SqlParameter("@phoneNumber", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = user.PhoneNumber
                    },
                    new SqlParameter("@discription", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = user.Discription
                    },
                     new SqlParameter("@firstName", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = user.FirstName
                    },
                    new SqlParameter("@lastName", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = user.LastName
                    },
                    new SqlParameter("@image", SqlDbType.NVarChar)
                    {
                      Direction = ParameterDirection.Input,
                      Value = user.Image
                    },
                };

                context.Database.ExecuteSqlRaw(
                "exec [dbo].[FillUser] @userId out,@phoneNumber,@discription,@firstName,@lastName,@image", parameters:
                parameters);


                userId = Convert.ToInt32(parameters[0].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return userId;
        }

        public static User? Get(int userId, BrainUpBdContext context)
        {
            try
            {
                var parameters = new[] {
                    new SqlParameter("@userId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = userId
                    }
                };
                
                var result = context.Users.FromSqlRaw(
                  $"exec [dbo].[GetUser] @userId", parameters: parameters);

                return result.ToList().First();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null;
        }

        public static int GetIdByEmailPassword(string email, string password, BrainUpBdContext context)
        {
            int userId = -1;
            try
            {
                var parameters = new[] {
                    new SqlParameter("@email", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = email
                    },
                    new SqlParameter("@password", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = password
                    },
                    new SqlParameter("@userId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                        Value = 0
                    }
                };

                context.Database.ExecuteSqlRaw(
                  $"exec [dbo].[GetUserIdByEmailPassword] @email,@password, @userId out ", parameters: parameters);

                userId = Convert.ToInt32(parameters[2].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return userId;
        }
    }
}
