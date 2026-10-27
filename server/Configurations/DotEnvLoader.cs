namespace CommBank.Configurations;

public static class DotEnvLoader
{
    public static void Load()
    {
        var candidates = new[] { ".env", Path.Combine("..", ".env") };
        foreach (var path in candidates)
        {
            if (File.Exists(path))
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var trimmed = line.Trim();
                    if (!string.IsNullOrWhiteSpace(trimmed) && !trimmed.StartsWith("#"))
                    {
                        var parts = trimmed.Split('=', 2);
                        if (parts.Length == 2)
                        {
                            Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
                        }
                    }
                }
                break;
            }
        }
    }
}
