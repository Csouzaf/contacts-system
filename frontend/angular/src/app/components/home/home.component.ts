import { CookieService } from 'ngx-cookie-service';
import { UsersAuth } from 'models/UsersAuth';
import { HomeUserauthService } from './../services/home-userauth.service';
import * as jwt_decode from 'jwt-decode';
import { Component, OnInit } from '@angular/core';
import { Users } from 'models/Users';




@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [CookieService],

})
export class HomeComponent implements OnInit {
  users: UsersAuth[] = []

  constructor(private homeUserauthService : HomeUserauthService, private cookieService: CookieService){}

  ngOnInit(): void {
    this.getAll()
    // const token = this.cookieService.get("jwt")



    //   if(token !== null){
    //  const getUserAuthenticated = JSON.parse(window.atob(token.split('.')[1]))

    //     console.log("name", getUserAuthenticated.name)
    // if(token !== null){
    //   const user: UsersAuth = {
    //     id: decodedToken.id,
    //     name: decodedToken.name
    //   };
      // }
      // else{
      //   console.log("null")
      // }
    }


    getAll(){
      this.homeUserauthService.getAll().subscribe((result)=>{

        console.log(result)
      })
    }

    // error => {
    //   console.error(error)
    // })

    // this.current = this.homeUserauthService.getUser();

  }



