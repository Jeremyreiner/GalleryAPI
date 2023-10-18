import { UserModel } from "./UserModel";

export class TokenModel {
  token: string = '';
  user: UserModel;
  
  constructor(user?: UserModel) {
    this.user = user;
  }
}