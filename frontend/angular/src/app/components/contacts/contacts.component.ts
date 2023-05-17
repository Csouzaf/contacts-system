import { Users } from '../Users';
import { ContactsService } from './../services/contacts.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})

export class ContactsComponent implements OnInit{

  users!: Users[];

  constructor(private contactsService : ContactsService){}

  ngOnInit(): void {

    this.contactsService.getUsers().subscribe(
      data => {
        this.users = data
        console.log(this.users)
      }
    )
  }

}
