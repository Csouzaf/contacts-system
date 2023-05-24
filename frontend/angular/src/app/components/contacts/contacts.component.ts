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

  users!: Users[];

  constructor(private contactsService : ContactsService, private router: Router){}

  showConfirmation = false;

  confirmRemove(){
    this.router.navigate(['/contacts/remove']);
  }

  ngOnInit(): void {

    this.contactsService.getUsers().subscribe(
      data => {
        this.users = data
        console.log(this.users)
      }
    )
  }

}
