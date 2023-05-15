import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {

  private contactsURL = "https://localhost:7299/api/contacts";
  

  constructor() { }


}
