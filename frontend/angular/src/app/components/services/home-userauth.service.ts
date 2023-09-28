import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UsersAuth } from 'models/UsersAuth';
import { Observable } from 'rxjs';
import * as jwt_decode from 'jwt-decode';
import { CookieService } from 'ngx-cookie-service';
import { Route, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class HomeUserauthService {

  private getUserAuth = "https://localhost:7087/api/contacts/auth";
  private getAllUser = "https://localhost:7087/api/auth/list";

  constructor(private http : HttpClient, private cookieService: CookieService, private router : Router) { }

   HttpOptions ={
    headers: new HttpHeaders( {
      'Contenty-Type': 'application/json',
      'Accept':'application/json',

    }), withCredentials: true

   }

   getAll(): Observable<any>{

    return this.http.get<any>(this.getAllUser)

   }

   getUserAuthenticated(): Observable<UsersAuth>{

    // if(localStorage.getItem('jwt') != null){

      return this.http.get<UsersAuth>(this.getUserAuth)
    // }

    // return <any> null

  }

  //  getUserInfoFromToken(token: string): { id: number; username: string } | null {
  //   try {
  //     const decodedToken: any = jwt_decode(token);

  //     // Extract user ID and username from the decoded token
  //     const userId = decodedToken.id;
  //     const username = decodedToken.username;

  //     return { id: userId, username: username };
  //   } catch (error) {
  //     // Handle any errors that may occur during decoding
  //     console.error('Error decoding JWT token:', error);
  //     return null;
  //   }
  // }

   getUserAuths(): Observable<any>
   {
    const token = this.cookieService.get("jwt")
    if(token !== null){
     return JSON.parse(window.atob(token.split('.')[1]))
    }
    return this.http.get<any>(this.getUserAuth)    // return this.http.get<UsersAuth>(JSON.parse(user));

   }


  // public getUserAuthenticate(): any
  // {
  //   const token = this.cookieService.get("jwt")
  //   if(token !== null){
  //     return JSON.parse(window.atob(token.split('.')[1]))
  //    }
  //   // return this.http.get<any>(this.getUserLogin)
  //   return {};
  // }

  storeToken(){
     //send the jwt to localstorage from cookies
    const sendToken = this.cookieService.get('jwt')

    localStorage.setItem('jwt', sendToken)
  }

  getToken(){
    return localStorage.getItem('jwt')
  }

  isLoggedIn():boolean{
    return !!localStorage.getItem('jwt')
  }

  signOut(){
    localStorage.clear()
    localStorage.removeItem('jwt')
    this.router.navigate(["login"])
  }
}
