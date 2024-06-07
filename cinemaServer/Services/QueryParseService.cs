namespace cinemaServer.Services
{
    public static class QueryParseService
    {
        public static List<int> ParseTheaterFilterList(string theaterFilterString) 
        {
            if (string.IsNullOrEmpty(theaterFilterString)) 
            {
                return new List<int>();
            }

            return theaterFilterString
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }
}
