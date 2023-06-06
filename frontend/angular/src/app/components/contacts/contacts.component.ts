import { Router, ActivatedRoute } from '@angular/router';
import { Users } from '../../../../models/Users';
import { ContactsService } from './../services/contacts.service';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserSharedService } from '../services/user-shared.service';


@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.scss']
})

export class ContactsComponent implements OnInit{


  users: Users[] = []

  constructor( private cdr: ChangeDetectorRef, private router: Router, private activatedRoute: ActivatedRoute, private userSharedService : UserSharedService, private contactsService: ContactsService){}


  ngOnInit(): void {
    this.getAll()


   }

   getAll() {

     this.contactsService.getUsers().subscribe((result) => {

       this.users = result
       console.log(this.users)
       })

   }

  showConfirmation = false;

  // sendEditContact(){
  //   this.router.navigate(['/contacts/edit/id'])
  // }



  }





