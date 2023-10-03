using NLog;
public class MovieFile
{
  // public property
  public string filePath { get; set; }
  public List<Movie> Movies { get; set; }

  private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

  // when an instance of a class is created
  public MovieFile(string movieFilePath)
  {
    filePath = movieFilePath;
    // create instance of Logger
 
    Movies = new List<Movie>();

    // to populate the list with data, read from the data file
    try
    {
      StreamReader sr = new StreamReader(filePath);
      // first line contains column headers
      sr.ReadLine();
      while (!sr.EndOfStream)
      {
        // create instance of Movie class
        Movie movie = new Movie();
        string line = sr.ReadLine();
        // first look for quote(") in string
        // this indicates a comma(,) in movie title
        int idx = line.IndexOf('"');
        if (idx == -1)
        {
          // no quote = no comma in movie title
          // movie details are separated with comma(,)
          string[] movieDetails = line.Split(',');
          movie.movieId = UInt64.Parse(movieDetails[0]);
          movie.title = movieDetails[1];
          movie.genres = movieDetails[2].Split('|').ToList();
        }
        else
        {
          // quote = comma in movie title
          // extract the movieId
          movie.movieId = UInt64.Parse(line.Substring(0, idx - 1));
          // remove movieId and first quote from string
          line = line.Substring(idx + 1);
          // find the next quote
          idx = line.IndexOf('"');
          // extract the movieTitle
          movie.title = line.Substring(0, idx);
          // remove title and last comma from the string
          line = line.Substring(idx + 2);
          // replace the "|" with ", "
          movie.genres = line.Split('|').ToList();
        }
        Movies.Add(movie);
      }
      // close file when done
      sr.Close();
      logger.Info("Movies in file {Count}", Movies.Count);
    }
    catch (Exception ex)
    {
      logger.Error(ex.Message);
    }
  }
  // public method
  public bool isUniqueTitle(string title)
  {
    if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
    {
      logger.Info("Duplicate movie title {Title}", title);
      return false;
    }
    return true;
  }
}