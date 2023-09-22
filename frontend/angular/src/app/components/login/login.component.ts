import { JwtInterceptor } from './../../interceptors/jwt.interceptor';
import { HomeUserauthService } from './../services/home-userauth.service';
import { HomeComponent } from './../home/home.component';
import { Signup } from './../../../../models/Signup';
import { AuthRegisterService } from './../services/auth-register.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, RequiredValidator, EmailValidator } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthLoginService } from '../services/auth-login.service';
import { Login } from 'models/Login';
import { HttpClient } from '@angular/common/http';
// import from 'auth0/angular-jwt';

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
    private homeUserauthService : HomeUserauthService

  ){}

  ngOnInit(): void {
    this.formLogin = this.formBuilder.group({
      email: "",
      password: "",
      jwt: ""
    })

  }


  sendLogin(){

      this.authLoginService.sendPostLogin(this.formLogin.value).subscribe({
        next:(result)=>{
        console.log(result)
        //send the jwt to localstorage
        localStorage.setItem("jwt", result.jwt)
        console.log( result.jwt)

        this.router.navigate(['/home'])

      }
    })

  }


}

