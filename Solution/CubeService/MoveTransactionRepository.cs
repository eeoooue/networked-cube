using CubeService.Models;
using Dapper;
using LibNetCube;
using Microsoft.Data.Sqlite;


namespace CubeService
{
    public class MoveTransactionRepository
    {
        private const string ConnectionString = "Data Source=cubedatabase.db";

        public void InsertMove(CubeMove move)
        {
            using var conn = new SqliteConnection(ConnectionString);
            var sql = "INSERT INTO MoveTransactions (Timestamp, MovePerformed) VALUES (@Timestamp, @MovePerformed)";
            conn.Execute(sql, new
            {
                Timestamp = DateTime.UtcNow.ToString("o"),
                MovePerformed = move.ToString()
            });
        }

        public void InsertMoves(IEnumerable<CubeMove> moves)
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Open(); // required for transaction

            using var transaction = conn.BeginTransaction();
            var sql = "INSERT INTO MoveTransactions (Timestamp, MovePerformed) VALUES (@Timestamp, @MovePerformed)";

            foreach (var move in moves)
            {
                conn.Execute(sql, new
                {
                    Timestamp = DateTime.UtcNow.ToString("o"),
                    MovePerformed = move.ToString()
                }, transaction);
            }

            transaction.Commit();
        }


        public void ClearAllMoves()
        {
            using var conn = new SqliteConnection(ConnectionString);
            conn.Execute("DELETE FROM MoveTransactions");
        }

        public IEnumerable<MoveTransaction> GetAllMoves()
        {
            using var conn = new SqliteConnection(ConnectionString);
            return conn.Query<MoveTransaction>("SELECT * FROM MoveTransactions ORDER BY Timestamp");
        }
    }
}
