// import {Injectable} from '@angular/core';
// import {MatDialog} from '@angular/material/dialog';
// import { OverlayContainer } from '@angular/cdk/overlay';
// import { StateService } from './state.service';
// import { TokenModel } from '../models/TokenModel';
// import { UserModel } from '../models/UserModel';

// @Injectable()
// export class StorageService {

//   static firstTimeLogin: boolean;
//   static readonly tokenKey = 'TOKEN';
//   rememberMe = true;

//   private storage: Storage = window.localStorage;

//   constructor(
//     public dialog: MatDialog,
//     public overlayContainer: OverlayContainer,
//     private state: StateService
//   ) {
//   }

//   initUserLogin(token: TokenModel) {
//     //console.log('SET USER ' + token.user.firstName );
//     // user only exists as variable in storage service, only token is stored in local storage
//     if(token.token) {
//       this.setToken(token.token);
//     }
//     console.log('USER IS INITIIIALIZED');

//     console.log('SET STATE USER', token.user);
//     this.state.user.set(new UserModel(token.user));
//   }

//   //endregion

//   //region TOKEN
//   setToken(token: string) {
//     if (token) {
//       this.setItem(StorageService.tokenKey, token);
//     }
//   }

//   getToken() {
//     return this.getItem(StorageService.tokenKey);
//   }


//   //endregion

//   //region STORAGE

//   logout() {
//     this.state.user.set(null);
//     this.storage.clear();
//   }

//   private getItem(key: string): string {
//     return this.storage.getItem(key);
//   }

//   private setItem(key: string, value: string) {
//     this.storage.setItem(key, value);
//   }



//   //endregion
// }
