class GitHubData {
    items: GitHubItem[] = [];
}

class GitHubItem {
    full_name: string = '';
    owner: GitHubOwner = new GitHubOwner();
}

class GitHubOwner {
    avatar_url: string = '';
}

export { GitHubData, GitHubItem, GitHubOwner };