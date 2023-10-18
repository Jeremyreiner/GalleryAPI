import { GitHubItem } from "./gitHubItems";

export class UserModel {
    name: string = '';
    gitHubItems: GitHubItem[] = [];

    constructor(fromStorage?) {
        this.gitHubItems = fromStorage?.gitHubItems;
    }
}
export class UserProperties extends UserModel {
    getPropertyNames() {
        return Object.keys(new UserProperties());
    }

    get(property: keyof UserProperties) {
        return property;
    }
}
