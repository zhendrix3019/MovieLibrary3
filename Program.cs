using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();
logger.Info("Program started");
string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.csv";

MovieFile movieFile = new MovieFile(movieFilePath);


logger.Info("Program ended");