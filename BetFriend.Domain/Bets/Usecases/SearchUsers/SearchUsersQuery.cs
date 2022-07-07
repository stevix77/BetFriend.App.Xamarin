namespace BetFriend.Domain.Bets.Usecases.SearchUsers
{
    public class SearchUsersQuery
    {
        public string Query { get; set; }

        public SearchUsersQuery(string query)
        {
            Query = query;
        }
    }
}
