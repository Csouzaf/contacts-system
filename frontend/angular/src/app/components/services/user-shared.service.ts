import { Injectable } from '@angular/core';
import { Users } from 'models/Users';

@Injectable({
  providedIn: 'root'
})
export class UserSharedService {

  users:Users[] = []

  addUsers(user: Users){
    this.users.push(user)
  }

  getUsers(){
    return this.users
  }
}
