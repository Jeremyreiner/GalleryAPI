import { LoginModel } from "../authentication/login.service";

export class TokenModel {
  token: string = '';
  user: LoginModel;
  
  constructor(user: LoginModel) {
    this.user = user;
  }
}