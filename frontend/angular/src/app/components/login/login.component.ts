import { Signup } from './../../../../models/Signup';
import { AuthRegisterService } from './../services/auth-register.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, RequiredValidator, EmailValidator } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthLoginService } from '../services/auth-login.service';
import { Login } from 'models/Login';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit{

  formLogin!: FormGroup;


  constructor(
    private router : Router,
    private formBuilder : FormBuilder,
    private authLoginService : AuthLoginService,

  ){}

  ngOnInit(): void {
    this.formLogin = this.formBuilder.group({
      email: "",
      password: ""
    })

  }

  sendLogin(){

      this.authLoginService.sendPostLogin(this.formLogin.value).subscribe((result)=>{
        console.log(result)

        this.router.navigate(['/home'])

      })

  }


}

