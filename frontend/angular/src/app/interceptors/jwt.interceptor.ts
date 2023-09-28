import { HomeUserauthService } from './../components/services/home-userauth.service';
import { HomeComponent } from './../components/home/home.component';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private homeUserauthService : HomeUserauthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const myToken = this.homeUserauthService.getToken()

    if(myToken){
      request = request.clone({
        setHeaders:{ Authorization :`Bearer ${myToken}` }
      })
///`jwtToken ${myToken}`
    }

    return next.handle(request);
  }
}
