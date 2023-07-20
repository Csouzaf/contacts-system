import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthRegisterService } from '../services/auth-register.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit{

  register!: FormGroup;

  constructor(
      private formBuilder : FormBuilder,
      private router : Router,
      private authRegisterService: AuthRegisterService
      ){}


  ngOnInit(): void {
    this.register = this.formBuilder.group({

      name: '',
      email: '',
      telefone: '',
      password:''

    })


  }

  verifyEmail(){

  }
  sendUserRegistered():void{

    // console.log(this.register.getRawValue())
     this.authRegisterService.postRegister(this.register.value).subscribe((result) => {


      console.log(result)

      this.router.navigate(['/login'])

    })

  }





}
