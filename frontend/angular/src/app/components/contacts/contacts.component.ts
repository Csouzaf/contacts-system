import { Router } from '@angular/router';
import { Users } from '../../../../models/Users';
import { ContactsService } from './../services/contacts.service';
import { Component, Input, OnInit } from '@angular/core';
import { UserSharedService } from '../services/user-shared.service';

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})

export class ContactsComponent implements OnInit{


  users:Users[] = []

  constructor(private router: Router, private userSharedService : UserSharedService){}

  ngOnInit(): void {

    this.getAll()

  }

  getAll(){
    this.users = this.userSharedService.getUsers()
    console.log(this.users)
  }
  showConfirmation = false;

  confirmRemove(){
    this.router.navigate(['/contacts/remove']);
  }





  }




