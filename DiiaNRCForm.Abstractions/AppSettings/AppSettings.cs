namespace DiiaNRCForm.Abstractions.AppSettings;

public class AppSettings
{
    public string PostgresNRCFromDb { get; set; }
    public string OriginUrl { get; set; }
    public DiiaSettings DiiaSettings { get; set; }
    public KoboToolboxSettings KoboToolboxSettings { get; set; }
}