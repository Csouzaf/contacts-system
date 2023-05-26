import { Users } from './../../Users';
import { Router } from '@angular/router';
import { ContactsService } from './../../services/contacts.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-create-contacts',
  templateUrl: './create-contacts.component.html',
  styleUrls: ['./create-contacts.component.scss']
})
export class CreateContactsComponent implements OnInit{

  formUsers: any;
  users!: Users[];

  constructor(private contactsService: ContactsService, private router: Router){}



  ngOnInit(): void {

    this.contactsService.getUsers().subscribe(response => {
      this.users = response;
    })
    
    this.formUsers = new FormGroup({

      Nome: new FormControl(),
      Email: new FormControl(),
      Telefone: new FormControl()
    });

  }


  showConfirmation = false;

  confirmRemove(){
    this.router.navigate(['/contacts/remove']);
  }



  // ngOnInit() {
  //   this.sendContacts();
  // }

  sendForm(): void{

    const contacts: Users = this.formUsers.value;

    this.contactsService.postUsers(contacts).subscribe((response) =>{
        alert("Adicionado")}
      // },

      // (error) =>{
      //   console.error("erro ao criar", error)
      // }
    )

  }
}
