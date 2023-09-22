import { Component } from '@angular/core';
import { HomeUserauthService } from '../../services/home-userauth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {
  constructor(private homeUserauthService : HomeUserauthService){}
  logout(){
    this.homeUserauthService.signOut()
  
  }
}
