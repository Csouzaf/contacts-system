import { UserSharedService } from './../../services/user-shared.service';
import { ContactsService } from './../../services/contacts.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Users } from 'models/Users';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-contacts',
  templateUrl: './edit-contacts.component.html',
  styleUrls: ['./edit-contacts.component.scss']
})
export class EditContactsComponent implements OnInit {

  formEdit!: FormGroup
  users: Users[] = []

  constructor(private contactsService: ContactsService,
      private router: Router,
      private route: ActivatedRoute,
      private formBuilder: FormBuilder,
      private cdr: ChangeDetectorRef,
      private userSharedService: UserSharedService){}

  ngOnInit(): void {

    this.formEdit = this.formBuilder.group({
      Nome:[''],
      Email:[''],
      Telefone:['']
    })

    const id = this.route.snapshot.params['id']
    console.log(id)


    this.getUserById(id)
  }

  getUserById(id: number){
     this.contactsService.getUsersById(id).subscribe((contact)=>{
      this.formEdit.patchValue(contact)
      console.log(contact)
    })

  }

  sendUserEdited(){
    const id = this.route.snapshot.params['id']


    const editUser = this.formEdit.value
    this.contactsService.updateUsers(id, editUser).subscribe((result)=>{
      console.log(result)
      alert("Usuário editado")
      console.log("Enviado")
      this.router.navigate(['/contacts']);
    })



  }

}


