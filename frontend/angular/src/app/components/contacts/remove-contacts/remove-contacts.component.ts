import { ActivatedRoute, Router } from '@angular/router';
import { ContactsService } from './../../services/contacts.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-remove-contacts',
  templateUrl: './remove-contacts.component.html',
  styleUrls: ['./remove-contacts.component.scss']
})
export class RemoveContactsComponent implements OnInit {


  constructor(private contactsService: ContactsService, private activatedRoute: ActivatedRoute, private router: Router){}


  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.params['id']
    console.log(id)
    this.getUserById(id)
  }

  getUserById(id: number){

    this.contactsService.getUsersById(id).subscribe((result)=>{
      console.log(result)
    })
  }


  removeUser(){
    const removeId = this.activatedRoute.snapshot.params['id']
    this.contactsService.deleteUsers(removeId).subscribe((result)=>{
      console.log(result)
      alert("Usuário removido")
      this.router.navigate(['/contacts'])

    })
  }
}
