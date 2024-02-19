using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ToDuoAPI.Data;
using ToDuoAPI.Models;
using ToDuoAPI.Models.DataTransferObjects;

namespace ToDuoAPI.Service.StoredProcedures
{
    public class AdventuresStoredProcedures
    {
        private readonly ToDuoDbContext _context;
        public AdventuresStoredProcedures(ToDuoDbContext context)
        {
            _context = context;
        }

        public async Task<List<MatchUserDTO>> CheckAdventureMatches(int adventureId, int userId)
        {
            List<MatchUserDTO> matches = new List<MatchUserDTO>();
            var connection = _context.Database.GetDbConnection();

            try
            {
                if(connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.CheckAdventureMatches";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@UserId", userId));
                    command.Parameters.Add(new SqlParameter("@AdventureId", adventureId));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var matchUserDto = new MatchUserDTO
                            {
                                Id = reader.GetInt32("Id"),
                                FirstName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? "" : reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.IsDBNull(reader.GetOrdinal("FirstName")) ? "" : reader.GetString(reader.GetOrdinal("LastName"))
                            };
                            matches.Add(matchUserDto);
                        }
                    }
                }
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }

            return matches;
        }
    }
}
