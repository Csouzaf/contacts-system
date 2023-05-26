import { Router } from '@angular/router';
import { Users } from '../Users';
import { ContactsService } from './../services/contacts.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})

export class ContactsComponent implements OnInit{

  contacts: Users[] = [];


  constructor(private contactsService : ContactsService, private router: Router){}

  showConfirmation = false;

  confirmRemove(){
    this.router.navigate(['/contacts/remove']);
  }

  receiveNewContact(contact: Users) {
    this.contacts.push(contact);
  }

  ngOnInit(): void {

    this.contactsService.getUsers().subscribe(
      data => {
        this.contacts = data
        console.log(this.contacts)
      }
    )
  }

}
