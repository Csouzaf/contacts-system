import { ContactsService } from './contacts.service';
import { Injectable } from '@angular/core';
import { Users } from 'models/Users';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserSharedService {

  users:Users[] = []
  private readonly storageKey = 'users';

  constructor() { }

  usersUpdated: BehaviorSubject<Users[]> = new BehaviorSubject<Users[]>([])

  addUsers(user: Users){
    this.users.push(user)
    this.usersUpdated.next([...this.users])
  }

  saveUsers(users: Users[]): void {
    localStorage.setItem(this.storageKey, JSON.stringify(users));
  }

  loadUsers(){

  }

  getUsers(){
     const usersJson = localStorage.getItem(this.storageKey);
    return usersJson ? JSON.parse(usersJson) : [];

  }

  getByService(){

  }

  // editUsers(user: Users){
  //   const index = this.users.findIndex(u=> u.Id === user.Id)
  //   if(index !== -1){
  //     this.users[index] = user
  //     this.users.push(user)
  //   }
  // }
}
