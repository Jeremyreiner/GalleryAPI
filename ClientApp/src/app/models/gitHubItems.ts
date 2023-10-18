export { GitHubData, GitHubItem, GitHubOwner };

class GitHubItem {
    full_name: string = '';
    owner: GitHubOwner = new GitHubOwner();
}

interface GitHubData {
    items: GitHubItem[];
}

class GitHubOwner {
    avatar_url: string = '';
}