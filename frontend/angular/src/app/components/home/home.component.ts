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
    //Get the user as name or id... from backend
      this.homeUserauthService.getAll().subscribe((result)=>{
        this.users = result
        console.log(result)
      })

    }


    }





