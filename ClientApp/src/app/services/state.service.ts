// import {Injectable, signal} from '@angular/core';
// import { UserModel } from '../models/UserModel';
// import { GitHubItem } from '../models/gitHubItems';

// @Injectable({
//   providedIn: 'root',
// })
// export class StateService {

//   user = signal<UserModel>(null);
  
//   pushGitHubItem(value: GitHubItem) {
//     this.user().gitHubItems.push(value);
//   }

//   removeGitHubItem(value: GitHubItem) {
//     let index= this.user().gitHubItems.indexOf(value);
//     delete this.user().gitHubItems[index];
//   }
// }
