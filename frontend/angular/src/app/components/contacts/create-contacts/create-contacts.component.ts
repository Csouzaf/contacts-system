import { Users } from '../../../../../models/Users';
import { Router } from '@angular/router';
import { ContactsService } from './../../services/contacts.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { UserSharedService } from '../../services/user-shared.service';
import { Observable, Subject, map } from 'rxjs';

@Component({
  selector: 'app-create-contacts',
  templateUrl: './create-contacts.component.html',
  styleUrls: ['./create-contacts.component.scss']
})
export class CreateContactsComponent {

  usersForm: FormGroup;
  users: Users[] =[]

  constructor(
    private contactsService: ContactsService,
    private router: Router,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private userSharedService : UserSharedService
    )
    {
      this.usersForm = this.formBuilder.group({
        Nome: [''],
        Email: [''],
        Telefone: ['']
      });
    }



  // ngOnInit(): void {
  //  this.getAll()


  // }


  // getAll() {

  //   this.contactsService.getUsers().subscribe((result) => {

  //     this.users = result
  //     console.log(this.users)
  //     })


  // }

  sendForm(){

      this.contactsService.postUsers(this.usersForm.value).subscribe((result) => {
        console.log(result)
        console.log("produto criado");
        alert("Inserido")
          // this.users.push(result);
        // this.getAll()
        this.router.navigate(['/contacts'])

        // this.usersForm.setValue({
        //   Nome: [''],
        //   Email: [''],
        //   Telefone: ['']
        // })

    })

  }

}


