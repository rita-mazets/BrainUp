using BrainUp.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace BrainUp.StoredProcedure
{
    public class PointAction
    {
        public static double GetAllPointForCource(int courceId, BrainUpBdContext context)
        {
            double point = 0;
            try
            {
                var parameters = new[] {
                new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = courceId
                    }
                };

                point = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[GetAllPointForCource] @courceId", parameters:
                  parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return point;
        }

        public static double GetAllPointsForUser (int userId, BrainUpBdContext context)
        {
            double point = 0;
            try
            {
                var parameters = new[] {
                new SqlParameter("@userId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = userId
                    },
                new SqlParameter("@point", SqlDbType.Float)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    },
                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[GetAllPointsForUser] @userId, @point out", parameters:
                  parameters);

                point = Convert.ToDouble(parameters[1].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return point;
        }

        public static double GetPercentOfMakedPoint (int courceId, int userId, BrainUpBdContext context)
        {
            double point = 0;
            try
            {
                var parameters = new[] {
                 new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = courceId
                    },
                new SqlParameter("@userId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = userId
                    },
                new SqlParameter("@percent", SqlDbType.Float)
                    {
                      Direction = ParameterDirection.Output,
                      Value = 0
                    },
                };

                context.Database.ExecuteSqlRaw(
                  "exec [dbo].[GetPercentOfMakedPoint] @courceId,@userId, @percent out", parameters:
                  parameters);

                point = Convert.ToDouble(parameters[2].Value);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return point;
        }

        public static double GetPointForCourceAndUser(int courceId, int userId, BrainUpBdContext context)
        {
            double point = 0;
            try
            {
                var parameters = new[] {
                 new SqlParameter("@courceId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = courceId
                    },
                new SqlParameter("@userId", SqlDbType.Int)
                    {
                      Direction = ParameterDirection.Input,
                      Value = userId
                    }
                };

                point = context.Database.ExecuteSqlRaw(
                  "exec [dbo].[GetPointForCourceAndUser] @courceId,@userId", parameters:
                  parameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return point;
        }
    }
}
