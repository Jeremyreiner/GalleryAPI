namespace Gallery.Shared.Entities;

public class GitHubItem
{
    public string full_name { get; set; }
    public GitHubOwner owner { get; set; }
}