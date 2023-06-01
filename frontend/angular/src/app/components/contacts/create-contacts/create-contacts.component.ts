import { Users } from '../../../../../models/Users';
import { Router } from '@angular/router';
import { ContactsService } from './../../services/contacts.service';
import { Component, Input, OnInit } from '@angular/core';
import { Form, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-create-contacts',
  templateUrl: './create-contacts.component.html',
  styleUrls: ['./create-contacts.component.scss']
})
export class CreateContactsComponent implements OnInit{

  formUsers!: FormGroup;
  // formUsers!: FormGroup;
  users: Users[] =[];
  contacts: Users ={
    Id: 0,
    Nome: '',
    Email: '' ,
    Telefone: ''
  }

  constructor(
    private contactsService: ContactsService,
    private router: Router,
    private formBuilder: FormBuilder,
    private cdr: ChangeDetectorRef
    ){}

  ngOnInit(): void {

    // this.formUsers = new FormGroup({

    //   Nome: new FormControl('', Validators.required),
    //   Email: new FormControl('', Validators.required),
    //   Telefone: new FormControl('', Validators.required)

    // });

    this.formUsers = this.formBuilder.group({
      Nome: ['', Validators.required],
      Email: ['', Validators.required,],
      Telefone: ['', Validators.required]
    });

    console.log(this.contacts)
    console.log(this.users)

  }




  getAll(){
    this.contactsService.getUsers().subscribe(result => {
      this.users = result;
      console.log(this.users)

    })


  }


  sendForm(){

    const user: Users = this.formUsers.value;

    this.contactsService.postUsers(user).subscribe((result) => {


      this.cdr.detectChanges();

      console.log(result);

      // alert("Adicionado");

      this.users.push(user);
      // this.router.navigate(['/contacts'])
    })

    // this.contactsService.postUsers(this.contacts).subscribe((resultPost) =>{
    //   console.log(resultPost)
    //   this.getAll()
    // })
  }

}
