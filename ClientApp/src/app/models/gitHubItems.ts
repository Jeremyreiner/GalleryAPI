export { GitHubData, GitHubItem, GitHubOwner };

class GitHubItem {
    full_name: string = '';
    owner: GitHubOwner = new GitHubOwner();
}

class GitHubData {
    items: GitHubItem[] = [];
}

class GitHubOwner {
    avatar_url: string = '';
}