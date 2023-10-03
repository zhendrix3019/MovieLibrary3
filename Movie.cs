public class Movie
{
  public UInt64 movieId { get; set; }
  public string title { get; set; }
  public List<string> genres { get; set; }

  public string Display()
    {
      return $"Id: {movieId}\nTitle: {title}\nGenres: {string.Join(", ", genres)}\n";
    }
}
