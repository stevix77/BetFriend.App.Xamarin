namespace BetFriend.Domain.Bets.Usecases.SearchMembers
{
    public class SearchMembersQuery
    {
        public string Keyword { get; set; }

        public SearchMembersQuery(string keyword)
        {
            Keyword = keyword;
        }
    }
}
