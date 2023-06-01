import { Router } from '@angular/router';
import { Users } from '../../../../models/Users';
import { ContactsService } from './../services/contacts.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})

export class ContactsComponent implements OnInit{

  
  users:Users[] = []


  constructor(private contactsService : ContactsService, private router: Router){}

  showConfirmation = false;

  confirmRemove(){
    this.router.navigate(['/contacts/remove']);
  }


  getAll(){
    this.contactsService.getUsers().subscribe(result => {
      this.users = result;
      console.log(this.users)

    })


  }

  ngOnInit(): void {

    this.getAll()

  }

}
