// import {Injectable} from '@angular/core';
// import {RegisterModel, TokenModel, ApiService, StorageService, RouteNames, StateService, ToastService} from '../../index';
// import {Router} from '@angular/router';
// import {HttpResponse} from '@angular/common/http';
// import {catchError, take, tap} from 'rxjs/operators';
// import {of} from "rxjs";
// import {Location} from "@angular/common";

// export interface LoginModel {
//   email: string;
//   password: string;
// }

// @Injectable(
// )

// export class LoginService {
//   constructor(
//     private api: ApiService,
//     private storage: StorageService,
//     private state: StateService,
//     private location: Location,
//     private router: Router,
//     public toastService: ToastService,
//   ) {
//   }

//   async loginWithGoogle(credentials: string): Promise<boolean> {
//     const body = {idToken: credentials};
//     let user;
//     await this.api.post<TokenModel>('Authentication/LoginWithGoogle', body).then((res) => {
//       user = res;
//     });
//     if (!user) {
//       return false;
//     }
//     this.storage.initUserLogin(user.body);
//     return await this.handleLoginRoute(user.isTeacher, true);
//   }

//   refreshToken() {
//     const errorHandle = () => this.router.navigate([RouteNames.landing]);
//     const currentToken = this.storage.getToken();
//     if (!currentToken) { // user is not authenticated
//       errorHandle();
//       return of(null);
//     }
//     return this.api.postResult<TokenModel>('Authentication/RefreshToken', {token: currentToken})
//       .pipe(
//         tap((responseToken) => {
//             if (!responseToken.data) {
//               this.router.navigate([RouteNames.landing]);
//               return false;
//             }
//             this.storage.initUserLogin(responseToken.data);
//             return this.handleLoginRoute(responseToken.data.isTeacher, !this.location.path());
//           },
//           catchError(() => errorHandle())
//         ));
//   }

//   login(email: string, password: string) {
//     const bodyData: LoginModel = {email, password};

//     return this.api.postResult<TokenModel>('Authentication/Login', bodyData)
//       .pipe(
//         tap((message) => {
//           if(message.success) {
//             this.storage.initUserLogin(message.data);
//             if(message?.data?.user) {
//               this.handleLoginRoute(message.data.isTeacher, true);
//             }
//           }
//           return message;
//         })
//       );
//   }

//   handleLoginRoute(isTeacher: boolean, dynamicRoute = false): boolean {
//     this.api.initData();
//     if (dynamicRoute) {
//       this.router.navigate([RouteNames.dynamicRoute(isTeacher)]);
//     }
//     // note that navigate to route needs to happen before return
//     // so that there's no weird login glitch
//     return true;
//   }

//   registerUser(registerModel: RegisterModel) {
//     return this.api.postResult<TokenModel>('Authentication/Register', registerModel)
//       .pipe(
//         take(1),
//         tap((response) => {
//         if (response.data.token) {
//           this.storage.initUserLogin(response.data);
//           StorageService.firstTimeLogin = true;
//           this.handleLoginRoute(response.data.isTeacher, true);
//           return 'success';
//         } else {
//           return `User with email: ${registerModel.email} already exists`;
//         }
//       }));
//   }

//   async resetPassword(password: string, email: string, verificationCode: string): Promise<boolean> {
//     const bodyData: LoginModel = {
//       email,
//       password,
//     };
//     let success: boolean;
//     await this.api.post<HttpResponse<boolean>>(`Authentication/ResetPassword/${verificationCode}`, bodyData).then((res) => {
//       success = res.body;
//     });

//     if (success) {
//       this.login(email, password);
//     }
//     return success;
//   }

//   forgotPassword(email: string) {
//     return this.api.postResult<boolean>(`Authentication/ForgotPassword/${email}`, null);
//   }

//   async changePassword(password: string, newPassword: string): Promise<boolean> {
//     const bodyData = {email: this.state.user().email, password, newPassword};
//     let success;
//     await this.api.post<any>('Authentication/ChangePassword', bodyData).then((res) => {
//       success = res.body;
//     });
//     setTimeout(() => {
//     }, 0);
//     if(success) {
//       this.toastService.openToast('Your password has been updated.', true);
//     } else {
//       this.toastService.openToast('Password reset failed.', false);
//     }
//     return success;
//   }
// }
