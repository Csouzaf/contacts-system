import { HomeUserauthService } from './../components/services/home-userauth.service';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { NgbToast } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { NgToastService} from 'ng-angular-popup';

@Injectable({
  providedIn: 'root'
})

export class AuthGuard {

  constructor(private homeUserauthService : HomeUserauthService, private toast : NgToastService, private router : Router){}

  canActivate(): boolean{
    if(this.homeUserauthService.isLoggedIn()){
      return true
    }
    else{
      this.toast.error({detail: "ERROR", summary: "Login firstly"})
      this.router.navigate(['/login'])
      return false;
    }

  }

}
