import { Users } from '../../../../../models/Users';
import { Router } from '@angular/router';
import { ContactsService } from './../../services/contacts.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { UserSharedService } from '../../services/user-shared.service';

@Component({
  selector: 'app-create-contacts',
  templateUrl: './create-contacts.component.html',
  styleUrls: ['./create-contacts.component.scss']
})
export class CreateContactsComponent implements OnInit{

  formUsers!: FormGroup;
  users:Users[] = []


  constructor(
    private contactsService: ContactsService,
    private router: Router,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef,
    private userSharedService : UserSharedService
    ){}

  ngOnInit(): void {

    this.formUsers = this.formBuilder.group({
      Nome: ['', Validators.required],
      Email: ['', Validators.required,],
      Telefone: ['', Validators.required]
    });


    console.log(this.users)

  }

  sendForm(){

    const user: Users = this.formUsers.value;

      this.contactsService.postUsers(user).subscribe((result) => {

      console.log(result);
      this.cdr.detectChanges();
      alert("Contato adicionado");
      
      this.userSharedService.addUsers(user)
      this.router.navigate(['/contacts'])

    })

  }

}
