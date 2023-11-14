import { Component } from '@angular/core';
import { HomeUserauthService } from '../../services/home-userauth.service';
import { UsersAuth } from 'models/UsersAuth';
import { CookieService } from 'ngx-cookie-service';

declare function test():any
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})


export class NavComponent {
  users!: UsersAuth

  constructor(private homeUserauthService : HomeUserauthService, private cookieService: CookieService){}

  ngOnInit(): void {
    //Get the user as name or id... from backend
      this.homeUserauthService.getUserAuthenticated().subscribe((result)=>{
        this.users = result
        console.log(result)
      })
      test()
    }


  logout(){
    this.homeUserauthService.signOut()

  }




}
